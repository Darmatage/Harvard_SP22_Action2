using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_arrows_4 : MonoBehaviour
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
    [SerializeField] bool isGrounded = false;
	public bool isFiltering = false;
	public bool isJumping = false;
	


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
			// isGrounded = true;
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

				// isGrounded = false;
				// rigidbody2d.velocity = h * velocityNow*speed;
			}
	}			
		      
    

    // Update is called once per frame
    void Update()
    {
		
		if(isGrounded && (Input.GetKeyDown("space") || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))))
		{
			// isGrounded = false;
			isJumping = true;
		}
		
		if(!isJumping)
		{
			isGrounded = underMyFeet.isGrounded;
		}
		
		// turn off isGrounded if OX
		if((Input.GetMouseButton(1) || (Input.GetKeyDown(KeyCode.E))))
		{
			isGrounded = false;
		}
		
		// for jumping
		
		
			if (isGrounded)
			{
				// transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
				// time += Time.deltaTime;
				h = Input.GetAxisRaw("Horizontal");
				v = Input.GetAxis("Vertical");
				
				if(Input.GetKey(KeyCode.Space))
				{
					v = 1f;
				}

				rigidbody2d.freezeRotation = true;
				
				//NEW STUFF BY DANIEL - activates on platform animation
				animator.SetBool("Idle", true);
				animator.SetBool("Float", false);
				// animator.SetBool("", true);
				animator.SetFloat("Walk", h);
				animator.SetFloat("WalkB", -h);
				
					if (v != 0)
	{
		isGrounded = false;
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
				
				//NEW STUFF BY DANIEL - activates off platform animation
				animator.SetBool("Idle", false);
				animator.SetBool("Float", true);
				// animator.SetBool("Light", true);
				
			}
			
		
    }

void FixedUpdate()
{
	
	

    // isGrounded = underMyFeet.isGrounded;
	Vector3 force3d = new Vector3 (0,-1,0);
	Vector3 force = force3d;
	Vector2 upDir = new Vector2(1,0);
   // Cast a ray straight down.
   RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
   
   if (hit.collider != null && isGrounded == true)
    {
		// transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
		// time += Time.deltaTime;
		
		// reorient player to platform
		// transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 100f * Time.deltaTime);
		
		// pull player down to platform sice they rotate and can disconnect
		// rigidbody2d.AddForce(Vector2.down * 10f);
		// rigidbody2d.AddForce(Vector2.down*10f, ForceMode2D.Impulse);
		
		Vector3 surfaceNorm3d = new Vector3(hit.normal.x, hit.normal.y, 0);
		
        // Project our force direction to be parallel to the floor
        force = Vector3.ProjectOnPlane(force3d, surfaceNorm3d);
		
		 float angle = Mathf.Atan2(hit.normal.x, hit.normal.y)*Mathf.Rad2Deg; //get angle
         Debug.Log( " THE ANGLE IS " + angle);
    // This means the force we're adding is now following the slopes angle
	Vector2 force2D = new Vector2(force.x, force.y);
	// Vector2 nonZeroForce;
	
	// if (force2D != Vector2.zero) nonZeroForce = force2D;
	if(h != 0 && v == 0)
		{
			// AudioHandler.PlaySoundLoop ("walk", true);
			// AudioHandler.PlaySoundLoop ("ox", false);
			// walking on metal
			// AudioHandler.PlaySound ("walk");
			// print("move horiz " + force2D*h*speed + " h " + h + " force2d " + force2D + " speed " + speed + "vel " + rigidbody2d.velocity);
			
						// if we're on a horizontal surface
			if (force2D == Vector2.zero)
			{
				force2D = new Vector2(1,0);
				horizontalSpeed = speed;
				rigidbody2d.velocity = (force2D*h*horizontalSpeed);
			}
			else // hills
			{
				if (angle > 0) rigidbody2d.AddForce(force2D*h*speed*1.5f, ForceMode2D.Force);
				if (angle < 0) rigidbody2d.AddForce(-force2D*h*speed*1.5f, ForceMode2D.Force);
			}

		}
	if (h == 0 && v == 0)
		{
			rigidbody2d.velocity = Vector2.zero;
		}
	// if (v != 0)
	// {
		// isGrounded = false;
		 // Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		 // var angle2 = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg; //get angle
		 // print("the angle of jump is " + angle2 + "the force will be "+ direction*jumpPower);
		// maybe adding to it will be with mouse
		// rigidbody2d.AddForce(Vector2.up, ForceMode2D.Impulse);
		// rigidbody2d.velocity = Vector2.zero;
		// rigidbody2d.AddForce(direction*jumpPower, ForceMode2D.Impulse);

		 // StartCoroutine(delay());
		 // isJumping = false;
	// }
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
