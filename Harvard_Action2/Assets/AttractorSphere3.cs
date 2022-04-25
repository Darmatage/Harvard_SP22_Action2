using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorSphere3 : MonoBehaviour
{
	public float GravityRadius = 10;
	public float BootGravPower = 30;
	public float moveSpeed = 15;
	public bool isGrounded;
	public IsGroundedFromFeet IsGroundedFromFeet;
	
    // CharacterController r;
	Rigidbody2D rb;
	Vector2 com;
	Vector2 comFeet;
	Vector2 hitpoint;
	Vector2 origin;

	Vector2 normalSurface;
	GameObject feet;
	Vector2 dir;
	
	// for movement left or right
	Vector2 moveDir;
	bool isMovingHorizontally = false;
	
	
	
	// for jumping
	Vector2 jumpVect;
	Vector2 jumpDir;
	public bool isJumping;
	public float jumpPower = 20;
	public float tapSpeed = 0.5f;
	float lastTapTime = 0;
	

	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		com = rb.centerOfMass;
		feet = GameObject.FindWithTag("feet");
		comFeet = feet.GetComponent<Rigidbody2D>().centerOfMass;
		jumpDir = Vector2.up;
		dir = -Vector2.up;
		isJumping = false;
    }
	 void Update()
    {
		// check if on platform
	  // isGrounded = IsGroundedFromFeet.isGrounded;
	  
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
	  
	}

    void FixedUpdate()
    {
        // RaycastHit hit;
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        // origin = pos2D + com;
		origin = feet.transform.position;
		
		// maybe to use later
		Vector2 originFeet  = new Vector2(feet.transform.position.x, feet.transform.position.y) + comFeet;
		// RaycastHit2D hit = (Physics2D.CircleCast(origin, GravityRadius, transform.forward, 0));

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        // if (Physics2D.CircleCast(p1, rb.height / 2, transform.forward, out hit, GravityRadius))
			
		// create a circle radius around player
		 Collider2D [] colliders = Physics2D.OverlapCircleAll(feet.transform.position, 10f);
		 if(colliders.Length > 1)
		 {
			 RaycastHit2D hit1;
			 RaycastHit2D hit2;
			
			   
			   // should loop and find first one with tgis tag
				foreach (Collider2D c in colliders) 
				{
				   if (c.tag == "platform")
				   {
					   
					   Vector2 closestPoint = c.ClosestPoint(origin);
					   var heading = origin - closestPoint;
					   var distance = heading.magnitude;
					   
					   print("distance is " + distance);
					   // if (distance <= 0.75f )
					   // {
						   // print("now grounded!!!" );
						   // isGrounded = true;
					   // }
					   // else
					   // {
						   // isGrounded = false;
					   // }
					   dir = -heading / distance;
					   jumpDir = -dir;
					   // dir = dir.normalized;
					   hit1 =  Physics2D.Raycast(origin, dir, GravityRadius);
					   hitpoint = hit1.point;
				
					   // rotate player to srface normal
					   
					   if(!isGrounded)
					   {

						   normalSurface = hit1.normal;
						   Vector3 normalSurface3D = new Vector3(normalSurface.x, normalSurface.y, 0);
						   transform.up -= (transform.up - normalSurface3D)*0.4f;
					   }
					   
					   // force calculator
					   // Vector2 closestPointToFeet = hitpoint.ClosestPoint(origin);
					   var new_heading = origin - hitpoint;
					   var new_distance = new_heading.magnitude;
					   
					 
					
					   if (new_distance <= 0.75f )
					   {
						   print("now grounded!!!" );
						   isGrounded = true;
					   }
					   else
					   {
						   isGrounded = false;
					   }
					   var new_dir = -new_heading / new_distance;
					   Debug.DrawRay(origin, new_dir, Color.green, 1);
					   print("new_dir " + new_dir);
					   
					    if (Physics2D.Raycast(originFeet, new_dir, GravityRadius)) 
						{
					
						   hit2 =  Physics2D.Raycast(originFeet, new_dir, GravityRadius);
						   var hitpoint2 = hit2.point;
						   
						   var new_heading2 = originFeet - hitpoint2;
						   var new_distance2 = new_heading2.magnitude;
						   var new_dir2 = -new_heading2 / new_distance2;
						   
						   Debug.DrawLine(originFeet, hitpoint2, Color.yellow, 2);
						   
						   if(isGrounded)
						   {
						   var normalSurface2 = hit2.normal;
						   Vector3 normalSurface3D_2 = new Vector3(normalSurface2.x, normalSurface2.y, 0);
						   transform.up -= (transform.up - normalSurface3D_2)*1f;
						   }
						   
						   // once player is oriented we need to create another raycast to calculate force pull
						   if(!isJumping)
						   {
								rb.AddForce((new_dir2 * BootGravPower), ForceMode2D.Force);
						   }
						   // Debug.DrawRay(origin, dir, Color.blue, 5);
						}
					   Debug.DrawLine(origin, hitpoint, Color.red, 5);
					   // break;
					   
					   // rb.angularVelocity = 0;
					   // rb.inertia = 0;
					   

				   }
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
	
		// void OnCollisionEnter2D(Collision2D collision)
	// {
			// if (collision.gameObject.tag == "platform")
			// {
				// print("collision with platform");
				// isGrounded = true;

			// }
	// }
	
	// void OnCollisionExit2D(Collision2D collision)
    // {
			// if( collision.gameObject.tag == "platform") 
			// {
				// isGrounded = false;
			// }
	// }
}


/* 
notes for KAI:
work on directions.... right is along surface normal, left is negative of surface normal?
*/