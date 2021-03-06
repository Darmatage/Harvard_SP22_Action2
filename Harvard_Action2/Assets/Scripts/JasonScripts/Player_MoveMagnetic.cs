using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveMagnetic : MonoBehaviour{
	
    //public Animator anim;
	//public AudioSource WalkSFX;
	public Rigidbody2D rb2D;
	public Transform feet;
	public LayerMask walkableLayer;

	//Attraction variables:
	//public bool isMagnetic = false;
	public float magnetRange = 2f;
	public float groundedRange = 0.4f;
	private Vector3 PlayerInContactVector;
	public Transform currentAttractor;
	public bool attractMe = false;

	public float magnetism = -10f;
	private float maxMagnetism;
	private float minMagnetism = -2f;
	public float distToAttractor;

	//Jumping variables:
	public bool inContact = false;
	public float jumpForce = 13f;
	public bool isJumping = false;
	public float jumpTime = 2f;

	//Movement variables:
	private bool FaceRight = false; // determine which way player is facing.
	private Vector3 hMove; 
	public float walkSpeed = 2f;
	public bool isWalking = false;
	
	//Polygon Collider Variables:
	public PolygonCollider2D myCollider;
	public int totalPoints; // length of the array
	public Vector2[] shapePoints; 
	public Vector2 currentPoint;
	public int currentIndex = 0;
	public int startIndex; 
	
    void Start(){
        //animator = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
		rb2D.gravityScale = 0;
		PlayerInContactVector = Vector3.zero;
		maxMagnetism = magnetism;
    }

    void Update(){
		//Attraction work:
		findAttractor();
		IsGroundedCheck();
		
		if (attractMe){
			distToAttractor = Vector2.Distance(transform.position, currentAttractor.position);

			Vector3 magneticUp = (transform.position - currentAttractor.position).normalized;
			rb2D.AddForce(magneticUp * magnetism);
			
			Vector3 difference = currentAttractor.position - transform.position;
			float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 110f);
					
			if ((isJumping)||((distToAttractor > magnetRange)&&(distToAttractor < magnetRange + 1f))){
				endAttractor();
				//playerMag.currentWalkable = null;
			}
		}
		
		//Reduce magnetism when touching Attractor
		if (inContact){ magnetism = minMagnetism;} 
		else {magnetism = maxMagnetism;}

		//Movement work:
		//NOTE: Horizontal axis: [a] / left arrow = -1, [d] / right arrow = 1
		hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
		
		if ((inContact)&&(hMove != null)&&(currentAttractor !=null)){
						
			//move the player:
			if (hMove.x > 0){movePlayerRight();}
			if (hMove.x < 0){movePlayerLeft();}			
		}

		//Walk animation:
		// if (Input.GetAxis("Horizontal") != 0){
		//       anim.SetBool ("Walk", true);
		//       if (!WalkSFX.isPlaying){
		//             WalkSFX.Play();
		//       }
		// } else {
		//      anim.SetBool ("Walk", false);
		//      WalkSFX.Stop();
		// }

		//Player flip:
		if ((hMove.x <0 && !FaceRight) || (hMove.x >0 && FaceRight)){
			playerTurn();
		}
		
		//Jump input listener:
		if ((Input.GetButtonDown("Jump")) && (IsGroundedCheck())) {
			Jump();
			isJumping = true;
			StartCoroutine(NoAttractorsWhileJumping());
			// anim.SetTrigger("Jump");
			// JumpSFX.Play();
		}
    }
	
	void FixedUpdate(){
		//slow down on hills / stops sliding from velocity
		if (hMove.x == 0){
			rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y) ;
		}
		
		//Lerp player around debris
		// if (isWalking){
			// Vector2 pos = Vector2.Lerp ((Vector2)transform.position, currentPoint, walkSpeed * Time.fixedDeltaTime);
			// transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
			// if ((isJumping) ||((Vector2)transform.position == currentPoint)){isWalking=false;}
		// }
	}
	
	public void movePlayerRight(){
		currentIndex++; 
		if (currentIndex >= totalPoints){currentIndex=0;} 
		currentPoint.x = shapePoints[currentIndex].x + transform.position.x;
		currentPoint.y = shapePoints[currentIndex].y + transform.position.y;
		transform.position = currentPoint; // change to a LERP
		//isWalking = true;
		Debug.Log("current point = " + currentPoint);
	}
			
	public void movePlayerLeft(){
		currentIndex--; 
		if (currentIndex < 0){currentIndex=(totalPoints -1);} 
		currentPoint.x = shapePoints[currentIndex].x + transform.position.x;
		currentPoint.y = shapePoints[currentIndex].y + transform.position.y;
		transform.position = currentPoint; // change to a LERP
		//isWalking = true;
		Debug.Log("current point = " + currentPoint);
	}
	
	
	private void playerTurn(){
		// NOTE: Switch player facing label
		FaceRight = !FaceRight;

		// NOTE: Multiply player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void Jump() {
		//rb2D.velocity = PlayerWalkableVector * jumpForce;
		rb2D.velocity = transform.up * jumpForce;
		
		//Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
		//rb.velocity = movement;
	}

	public bool IsGroundedCheck() {
		Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, groundedRange, walkableLayer);
		//Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
		
		if (groundCheck != null) {
			//currentWalkable = groundCheck[0];
			inContact = true;
			
			//load polygon collider values
			myCollider = currentAttractor.gameObject.GetComponent<PolygonCollider2D>();
			totalPoints = myCollider.GetTotalPointCount();		
			shapePoints = myCollider.points; // How to get the points to recalculate when object is rotated?
			
			return true;
			//Debug.Log("I can jump now!");
		}
		inContact = false;
		//currentWalkable = null;
		return false;
	}
	
	void findAttractor(){
		//animator.SetTrigger ("Melee");
		Collider2D[] nearAttractors = Physics2D.OverlapCircleAll(feet.position, magnetRange, walkableLayer);
           
		if (nearAttractors == null){attractMe = false;}
		else{
			foreach(Collider2D attractor in nearAttractors){
			Debug.Log("I am near " + attractor.name);
			currentAttractor = attractor.gameObject.transform;
			attractMe = true;
			//attractor.GetComponent<AttractorBody>().pullPlayer(); // use a separate script for attraction
			}
		}
	}

	public void endAttractor(){
		currentAttractor = null;
		attractMe = false;
	}


	IEnumerator NoAttractorsWhileJumping(){
		yield return new WaitForSeconds(jumpTime);
		isJumping = false;
	}

	void OnDrawGizmos(){
		if (feet == null) {return;}
		Gizmos.DrawWireSphere(feet.position, magnetRange);
		Gizmos.DrawWireSphere(feet.position, groundedRange);
      }

}


//Jason drafted this script 4/12/22
	//check for nearby object in a "Walkable" LayerMask (like DebrisMajor) using dynamic collider
	//Check if distance is within magnetic boot threhold. If it is, activate manetic boots functionality
	//magnetism = pull player towards debris center. polygon collider on xebris klep player on surface. 
	//change walk inputs to vector 3.cross?
	//add jump: instantly override magnetism	