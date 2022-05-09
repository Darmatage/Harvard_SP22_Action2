using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePodMovement3 : MonoBehaviour
{
	public float GravityRadius = 10;
	public float BootGravPower = 30;
	public float speed = 5;
	public float jumpPower = 0.3f;
	public bool isGrounded;
	public bool isJumping = false;
	
    // CharacterController r;
	Rigidbody2D rb;
	Vector2 com;
	Vector2 hitpoint;
	Vector2 origin;
	Vector2 moveDir;
	Vector2 dir;
	Vector2 normalSurface;
	private float horizontalSpeed;
	float v;
	float h;
	
	// should i add feet so that object stands up?
	GameObject feet;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		com = rb.centerOfMass;
		horizontalSpeed = speed;
    }
	 void Update()
    {
      moveDir = new Vector2(Input.GetAxisRaw("Horizontal"),0).normalized;  
	  
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
			
			// bind space to jump
				if(Input.GetKey(KeyCode.Space))
				{
					v = 1f;
				}
			
			  // if(h != 0 && isGrounded)
			  // {
				  
				  // rb.velocity = Vector2.Perpendicular(normalSurface)*h*moveSpeed;
				  
				  // print("move dir is negative " + h + Vector2.Perpendicular(normalSurface));
			  // }
	  
	  
	  // if(v > 0) 
			 // { 
					// Vector2 jumpVect =  new Vector2(0,Input.GetAxisRaw("Vertical")).normalized;
					// Vector3 transPos = transform.TransformDirection(jumpVect);
					// Vector2 transPos2 =  new Vector2(transPos.x, transPos.y);
			// rb.MovePosition(rb.position + transPos2 * moveSpeed * Time.deltaTime);
					// isJumping = true;
					
			 // }
	  // if (v <= 0)
			 // {
					// isJumping = false;
			 // }
	}

    void FixedUpdate()
    {
        // RaycastHit hit;
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        origin = pos2D + com;
		// RaycastHit2D hit = (Physics2D.CircleCast(origin, GravityRadius, transform.forward, 0));

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        // if (Physics2D.CircleCast(p1, rb.height / 2, transform.forward, out hit, GravityRadius))
			
		// create a circle radius around player
		 Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
		 RaycastHit2D hit1;
		 if(colliders.Length > 1)
		 {
			 
			 
			 
			   
			   // should loop and find first one with tgis tag
				foreach (Collider2D c in colliders) 
				{
				   if (c.tag == "platform")
				   {
					   Vector2 closestPoint = c.ClosestPoint(origin);
					   var heading = origin - closestPoint;
					   var distance = heading.magnitude;
					   dir = -heading / distance;
					   // dir = dir.normalized;
					   hit1 =  Physics2D.Raycast(transform.position, dir, GravityRadius);
					   hitpoint = hit1.point;
					   normalSurface = -hit1.normal;
					   if(!isJumping)
					   {
							rb.AddForce((dir * BootGravPower), ForceMode2D.Force);
					   }
					   else
					   {
						   rb.AddForce((-dir ), ForceMode2D.Impulse);
						   
					   }
					   
					   Debug.DrawRay(origin, dir, Color.blue, 5);
					   break;
				   }
				}
				
				
		 }
		 if (isGrounded) 
		 {
			 
			 Vector3 force3d = new Vector3 (0,-1,0);
			
			 
				Vector3 surfaceNorm3d = new Vector3(normalSurface.x, normalSurface.y, 0);
					
					// Project our force direction to be parallel to the floor
					var force = Vector3.ProjectOnPlane(force3d, surfaceNorm3d);
					
					 float angle = Mathf.Atan2(normalSurface.x, normalSurface.y)*Mathf.Rad2Deg; //get angle
					 Debug.Log( " THE ANGLE IS " + angle);
				// This means the force we're adding is now following the slopes angle
				Vector2 force2D = new Vector2(force.x, force.y);
				// Vector2 nonZeroForce;
				
				// if (force2D != Vector2.zero) nonZeroForce = force2D;
				if(h != 0 && v == 0)
					{
						  rb.velocity = Vector2.Perpendicular(normalSurface)*h*speed;
				  
							print("move dir is negative " + h + Vector2.Perpendicular(normalSurface));

					}
				if (h == 0 && v == 0)
					{
						rb.velocity = Vector2.zero;
					}
				if (v != 0)
				{
					isJumping = true;
					 Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
					// maybe adding to it will be with mouse
					// rigidbody2d.AddForce(Vector2.up, ForceMode2D.Impulse);
					rb.AddForce(direction*jumpPower, ForceMode2D.Impulse);
					isGrounded = false;
					 // StartCoroutine(jump());
				}
				else isJumping = false;
			 
		 }
		
    }
	
		void OnCollisionEnter2D(Collision2D collision)
	{
			if (collision.gameObject.tag == "platform")
			{
				print("collision with platform");
				isGrounded = true;
				rb.velocity =  rb.velocity/(rb.velocity);// * 1.09f);
				print("rb.velocity/(rb.velocity "  + rb.velocity/(rb.velocity));

			}
	}
	
	void OnCollisionExit2D(Collision2D collision)
    {
			if( collision.gameObject.tag == "platform") 
			{
				isGrounded = false;
			}
	}
}
