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
			isGrounded = underMyFeet.isGrounded;
		
		// turn off isGrounded if OX
		if((Input.GetMouseButton(1) || (Input.GetKeyDown(KeyCode.E))))
		{
			isGrounded = false;
		}
		
			if (isGrounded)
			{
				transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
				time += Time.deltaTime;
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
				animator.SetFloat("Walk", h);
				animator.SetFloat("WalkB", -h);

			}
			else 
			{
				//resume the Rigidbody  rotating
				rigidbody2d.freezeRotation = false;
				
				//NEW STUFF BY DANIEL - activates off platform animation
				animator.SetBool("Idle", false);
				animator.SetBool("Float", true);
				
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
		
		
		Vector3 surfaceNorm3d = new Vector3(hit.normal.x, hit.normal.y, 0);
		
        // Project our force direction to be parallel to the floor
        force = Vector3.ProjectOnPlane(force3d, surfaceNorm3d);
		
		 float angle = Mathf.Atan2(hit.normal.x, hit.normal.y)*Mathf.Rad2Deg; //get angle
         Debug.Log( " THE ANGLE IS " + angle);
    // This means the force we're adding is now following the slopes angle
	Vector2 force2D = new Vector2(force.x, force.y);
	if(h != 0 && v == 0)
		{
			print("move horiz " + force2D*h*speed + " h " + h + " force2d " + force2D + " speed " + speed);
			
						// if we're on a horizontal surface
			if (force2D == Vector2.zero)
			{
				force2D = new Vector2(1,0);
				horizontalSpeed = speed;
				rigidbody2d.velocity = (force2D*h*horizontalSpeed);
			}
			else // hills
			{
				if (angle > 0) rigidbody2d.AddForce(force2D*h*speed, ForceMode2D.Force);
				if (angle < 0) rigidbody2d.AddForce(-force2D*h*speed, ForceMode2D.Force);
			}

		}
	if (h == 0 && v == 0)
		{
			rigidbody2d.velocity = Vector2.zero;
		}
	if (v != 0)
	{
		 Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		// maybe adding to it will be with mouse
		// rigidbody2d.AddForce(Vector2.up, ForceMode2D.Impulse);
		rigidbody2d.AddForce(direction*jumpPower, ForceMode2D.Impulse);
		isGrounded = false;
		 // StartCoroutine(jump());
	}
	}

}


}
