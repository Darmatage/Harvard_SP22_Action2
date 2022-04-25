using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorSpjere4_escapePod : MonoBehaviour
{
	public float GravityRadius = 10;
	public float BootGravPower = 30;
	public float moveSpeed = 15;
	public bool isGrounded;
	// public IsGroundedFromFeet IsGroundedFromFeet;
	
    // CharacterController r;
	Rigidbody2D rb;
	Vector2 com;
	// Vector2 comFeet;
	Vector2 hitpoint;
	Vector2 origin;

	Vector2 normalSurface;
	// GameObject feet;
	Vector2 dir;
	
	// for movement left or right
	Vector2 moveDir;
	bool isMovingHorizontally = false;
	
	
	
	// for jumping
	Vector2 jumpVect;
	Vector2 jumpDir;
	public bool isJumping;
	public float jumpPower = 20;
	

	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		com = rb.centerOfMass;
		jumpDir = Vector2.up;
		dir = -Vector2.up;
		isJumping = false;
    }
	 void Update()
    {
	  
	  if(isGrounded)
	  {
		  var h = Input.GetAxis("Horizontal");
		  if(h != 0)
		  {
			  isMovingHorizontally = true;
			  
			  rb.velocity = Vector2.Perpendicular(normalSurface)*h;
			  
			  print("move dir is negative " + h + Vector2.Perpendicular(normalSurface));
		  }
		  else
		  {
			  isMovingHorizontally = false;
		  }
		  // if (moveDir > 0)
		  // {
			  // print("move dir is positive " + moveDir);
		  // }
		  
		  // moveDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;  
		  if (Input.GetKeyDown(KeyCode.Space)) 
		  { 
				// jumpVect = Input.GetAxisRaw("Vertical").normalized
				isJumping = true;
		  }
		  if (Input.GetKeyUp(KeyCode.Space))
		  {
				isJumping = false;
		  }
		  Debug.DrawRay(origin, Vector2.Perpendicular(normalSurface), Color.yellow, 5);
	  }
	  
	}

    void FixedUpdate()
    {
        // RaycastHit hit;
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        origin = pos2D + com;
		
		// create a circle radius around player
		 Collider2D [] colliders = Physics2D.OverlapCircleAll(origin, 10f);
		 if(colliders.Length > 1)
		 {
			 RaycastHit2D hit1;
			
			   
			   // should loop and find first one with tgis tag
				foreach (Collider2D c in colliders) 
				{
				   if (c.tag == "platform")
				   {
					   
					   Vector2 closestPoint = c.ClosestPoint(origin);
					   var heading = origin - closestPoint;
					   var distance = heading.magnitude;
					   
					   dir = -heading / distance;
					   jumpDir = -dir;
					   hit1 =  Physics2D.Raycast(origin, dir, GravityRadius);
					   hitpoint = hit1.point;
				
						   normalSurface = hit1.normal;
						   Vector3 normalSurface3D = new Vector3(normalSurface.x, normalSurface.y, 0);
					
						   // once player is oriented we need to create another raycast to calculate force pull
						   if(!isJumping)
						   {
								rb.AddForce((dir * BootGravPower), ForceMode2D.Force);
						   }
						   // Debug.DrawRay(origin, dir, Color.blue, 5);
						}
					   Debug.DrawLine(origin, hitpoint, Color.red, 5);
				   }
				}
				
		 if (isGrounded) 
		 {
			// rb.AddForce((dir * BootGravPower*Time.deltaTime), ForceMode2D.Force);
			// Vector3 transPos = transform.TransformDirection(moveDir);
			// Vector2 transPos2 =  new Vector2(transPos.x, transPos.y);
			// rb.MovePosition(rb.position + transPos2 * moveSpeed * Time.deltaTime);
			// Vector3 normalSurface3D = new Vector3(normalSurface.x, normalSurface.y, 0);
			if (isJumping)
			{
				rb.MovePosition(rb.position + jumpDir*jumpPower*(Time.deltaTime));
				// send player continuing to float in that direction...
				
			}
			if (!isMovingHorizontally)
			{
				// ZERO velocity
				rb.velocity = Vector2.zero;
				rb.angularVelocity = 0;
				// rb.inertia = 0;
			}
			// transform.rotation = Quaternion.FromToRotation(transform.position, normalSurface);
		 }
		
    }
	
		void OnCollisionEnter2D(Collision2D collision)
	{
			if (collision.gameObject.tag == "platform")
			{
				print("collision with platform");
				isGrounded = true;

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


/* 
notes for KAI:
work on directions.... right is along surface normal, left is negative of surface normal?
*/