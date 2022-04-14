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
	public float walkForce = 10f;
	private Vector3 hMove; 
	
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
		//NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
		hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
		
		if ((inContact)&&(hMove != null)&&(currentAttractor !=null)){
			//transform.position = transform.position + hMove * walkSpeed * Time.deltaTime;
			
			rb2D.AddForce (transform.forward + hMove * walkForce * Time.deltaTime);
			Debug.Log("horizontal movement = " + hMove + ", move force vector = " + (transform.right + hMove * walkForce * Time.deltaTime));
			
			// always draw a 5-unit colored line from the origin
			Color color = new Color(2, 2, 1.0f);
			//Debug.DrawLine(Vector3.zero, new Vector3(0, 5, 0), color);
			Debug.DrawLine(transform.position, Vector3.Cross(transform.position, currentAttractor.position), color);
			
			// make this the vector3.cross!
			// PlayerInContactVector = transform.position - currentAttractor.position;
            // transform.right = Vector3.Cross(PlayerInContactVector, Vector3.forward);
			// rb2D.AddForce (transform.right + hMove * walkForce * Time.deltaTime);
			// Debug.Log("horizontal movement = " + hMove + ", move force vector = " + (transform.right + hMove * walkForce * Time.deltaTime));
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
		// NOTE: if input is moving the Player right and Player faces left, turn, and vice-versa
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