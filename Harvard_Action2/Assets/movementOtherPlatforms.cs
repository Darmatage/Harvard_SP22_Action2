using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementOtherPlatforms : MonoBehaviour
{
   	public Animator animator;
	public float speed = 10f;
	public float jumpPower=0.3f;
	// private Vector2 velocityNow;
	public OxBarScript dragCanvasHereOxyHealth;
	private float h=0;
	private float v=0; // jump
	private float horizontalSpeed;
	// private float c;
	Quaternion startRotation;
	float time;
	// public GameObject underFeet;
	public PlatformChecker underMyFeet;
	// public GameObject myFeet;
	RaycastHit2D hit;
	// private bool isGrabbable;


// stick to floor
// Vector3 forceDirection = new Vector3(0, 0, 1);

  Rigidbody2D rigidbody2d;

    [Header("Bools")]
    [SerializeField] bool isGroundedOther = false;
	[SerializeField] bool isGrounded = false;
	public bool isFiltering = false;
	public bool isJumping = false;
	public bool isLeftRight = false;
	


    // Start is called before the first frame update
    void Start()
    {
		h = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
		startRotation = transform.rotation;
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
		
		if (collision.gameObject.tag == "platform")
		{
			// isGroundedOther = true;
		}	
		if (collision.gameObject.tag == "kill")
		{
			AudioHandler.PlaySound ("spike");
			// print("object is spike or fire! NAME: " + collision.gameObject.name);
			// print("the tag of obj is " + collision.gameObject.tag);
			dragCanvasHereOxyHealth.Die();
			
		}
		if (collision.gameObject.tag == "grabbable")
		{
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // player.collider);
		}	
		if (collision.gameObject.tag == "OxRefill")
        {
			isFiltering = true;
		}

					
    }
	void OnCollisionExit2D(Collision2D collision)
    {
			if( collision.gameObject.tag == "platform") 
			{

				// isGroundedOther = false;
				// rigidbody2d.velocity = h * velocityNow*speed;
			}
	}			
		      
    

    // Update is called once per frame
    void Update()
    {
		
		if(isGroundedOther && (Input.GetKeyDown("space") || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))))
		{
			// isGroundedOther = false;
			isJumping = true;
		}
		
		if(!isJumping)
		{
			isGroundedOther = underMyFeet.isGroundedOther;
			isGrounded = underMyFeet.isGrounded;
			isLeftRight = underMyFeet.isLeftRight;
		}
		
		// turn off isGroundedOther if OX
		if((Input.GetMouseButton(1) || (Input.GetKeyDown(KeyCode.E))))
		{
			isGroundedOther = false;
			// isGrounded = false;
		}
		
		// for jumping
		
		
			if (isGroundedOther)
			{

				h = Input.GetAxisRaw("Horizontal");
				v = Input.GetAxis("Vertical");
				// if(isLeftRight) // we are on right/left platform
				// {
					// v = Input.GetAxisRaw("Horizontal");
					// if(Input.GetKeyDown(KeyCode.LeftArrow))
					// {
					// h = new Vector2(0, 1)
					// }
					// if(Input.GetKeyDown(KeyCode.RightArrow))
					// {
					// h = new Vector2(0, -1)
					// }
				// }
				
				if(Input.GetKey(KeyCode.Space))
				{
					v = 1f;
				}

				rigidbody2d.freezeRotation = true;
				
				//NEW STUFF BY DANIEL - activates on platform animation
				animator.SetBool("Float", false);
				animator.SetBool("Idle", true);
			
				// animator.SetBool("", true);
				animator.SetFloat("Walk", h);
				animator.SetFloat("WalkB", -h);
				
					if (v != 0)
	{
		isGroundedOther = false;
		// isGrounded = false;
		 Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		 var angle2 = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg; //get angle
		 print("the angle of jump is " + angle2 + "the force will be "+ direction*jumpPower);
		// maybe adding to it will be with mouse
		// rigidbody2d.AddForce(Vector2.up, ForceMode2D.Impulse);
		rigidbody2d.velocity = Vector2.zero;
		rigidbody2d.AddForce(direction*jumpPower, ForceMode2D.Impulse);
		

		 StartCoroutine(delay());
		 // isJumping = false;
	}

			}
			else 
			{
				//resume the Rigidbody  rotating
				rigidbody2d.freezeRotation = false;
				
				if(!isGrounded) // so it does not interfere with horizontal platforms
				{
				//NEW STUFF BY DANIEL - activates off platform animation
				animator.SetBool("Idle", false);
				animator.SetBool("Float", true);
				}
				// animator.SetBool("Light", true);
				
			}
			
		
    }

void FixedUpdate()
{
	
	

    // isGroundedOther = underMyFeet.isGroundedOther;
	Vector3 force3d = new Vector3 (0,-1,0);
	Vector3 force = force3d;
	Vector2 upDir = new Vector2(1,0);
   // Cast a ray straight down.
   RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
   
   if (hit.collider != null && isGroundedOther == true)
    {
		
		Vector3 surfaceNorm3d = new Vector3(hit.normal.x, hit.normal.y, 0);
		
        // Project our force direction to be parallel to the floor
        force = Vector3.ProjectOnPlane(force3d, surfaceNorm3d);
		
		 float angle = Mathf.Atan2(hit.normal.x, hit.normal.y)*Mathf.Rad2Deg; //get angle
         Debug.Log( " THE ANGLE IS " + angle + " the force.x " + force.x + " " + force.y);
		// This means the force we're adding is now following the slopes angle
		Vector2 force2D = new Vector2(force.x, force.y);
		// Vector2 nonZeroForce;
	
	// if (force2D != Vector2.zero) nonZeroForce = force2D;
	if(h != 0 && v == 0)
		{

			if (force2D == Vector2.zero)
			{
				print("zero");
				if(!isLeftRight)
				{
				force2D = new Vector2(1,0);
				horizontalSpeed = speed;
				rigidbody2d.velocity = (force2D*h*horizontalSpeed);
				print("upsideDown");
				}
				else{
					force2D = new Vector2(0,1);
					horizontalSpeed = speed;
					rigidbody2d.velocity = (force2D*h*horizontalSpeed);
				}
			}
			else // hills
			{
				// if (angle > 0) rigidbody2d.AddForce(force2D*h*speed*1.5f, ForceMode2D.Force);
				// if (angle < 0) rigidbody2d.AddForce(-force2D*h*speed*1.5f, ForceMode2D.Force);
				force2D = new Vector2(0,1);

				horizontalSpeed = speed;
				print("I am in lr " + force2D*h*horizontalSpeed);
				rigidbody2d.velocity = (force2D*h*horizontalSpeed);
			}

		}
	if (h == 0 && v == 0)
		{
			rigidbody2d.velocity = Vector2.zero;
		}

	}

}

		IEnumerator delay()  
		{
			print("i'm in delay jump");
			yield return new WaitForSeconds(1f);
			isJumping = false;
			print("i'm in delay AFTER jump");
		}


}
