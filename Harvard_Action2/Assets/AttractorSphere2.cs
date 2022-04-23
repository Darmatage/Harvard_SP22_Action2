using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorSphere2 : MonoBehaviour
{
	public float GravityRadius = 10;
	public float BootGravPower = 30;
	public float moveSpeed = 15;
	public bool isGrounded;
	
    // CharacterController r;
	Rigidbody2D rb;
	Vector2 com;
	Vector2 hitpoint;
	Vector2 origin;
	Vector2 moveDir;
	Vector2 normalSurface;
	GameObject feet;
	Vector2 dir;
	
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
		jumpDir = Vector2.up;
		dir = -Vector2.up;
		isJumping = false;
    }
	 void Update()
    {
      moveDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;  
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
		// RaycastHit2D hit = (Physics2D.CircleCast(origin, GravityRadius, transform.forward, 0));

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        // if (Physics2D.CircleCast(p1, rb.height / 2, transform.forward, out hit, GravityRadius))
			
		// create a circle radius around player
		 Collider2D [] colliders = Physics2D.OverlapCircleAll(feet.transform.position, 10f);
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
					   // dir = dir.normalized;
					   hit1 =  Physics2D.Raycast(origin, dir, GravityRadius);
					   hitpoint = hit1.point;
					   // Rigidbody2D rbFeet = feet.GetComponent<Rigidbody2D>().AddForce((dir * BootGravPower), ForceMode2D.Force);
					   normalSurface = hit1.normal;
					   
					   if(!isJumping)
					   {
							rb.AddForce((dir * BootGravPower), ForceMode2D.Force);
					   }
					   Debug.DrawRay(origin, dir, Color.blue, 5);
					   Debug.DrawLine(origin, hitpoint, Color.red, 5);
					   // break;
					   
					   Vector3 normalSurface3D = new Vector3(normalSurface.x, normalSurface.y, 0);
					   transform.up -= (transform.up - normalSurface3D)*0.1f;
				   }
				}
				
				
		 }
		 if (isGrounded) 
		 {
			// rb.AddForce((dir * BootGravPower*Time.deltaTime), ForceMode2D.Force);
			Vector3 transPos = transform.TransformDirection(moveDir);
			Vector2 transPos2 =  new Vector2(transPos.x, transPos.y);
			rb.MovePosition(rb.position + transPos2 * moveSpeed * Time.deltaTime);
			// Vector3 normalSurface3D = new Vector3(normalSurface.x, normalSurface.y, 0);
			if (isJumping)
			{
				rb.MovePosition(rb.position + jumpDir*jumpPower*(Time.deltaTime));
				var m = rb.position;
				print("time "  + (Time.time - lastTapTime) + " " + tapSpeed);
				if ((Time.time - lastTapTime) < tapSpeed)
				{
					print(" added torque jumpdir " + jumpDir + " " + jumpDir*jumpPower*(Time.deltaTime)) ;
					// rb.AddForce(jumpDir * jumpPower, ForceMode2D.Impulse);
					rb.AddTorque(10 * Time.deltaTime);
					rb.velocity= jumpDir*jumpPower*(Time.deltaTime);
				}
				lastTapTime = Time.time;
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