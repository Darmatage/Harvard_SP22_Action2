using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement_attractor : MonoBehaviour
{
	
	// TO DO ORIGIN BEGINS AT PLAAYERS CENTER NEED TO MAKE BEGIN AT FEET CENTER AND POINT DOWN
	// HOW TO MAKE RAYCAST DOWN ALWAYS ATTRACKS THE FORCE
	public OxBarScript dragCanvasHereOxyHealth;
	public float GravityRadius = 10;
	public float BootGravPower = 30;
	public float moveSpeed = 15;
	public bool isGrounded;
	private float gravity = -10;

	Rigidbody2D rb;
	GameObject platform;
	GameObject feet;
	Vector2 com;
	Vector2 hitpoint;
	Vector2 origin;
	Vector2 moveDir;
	Vector2 velocityNow;
	Vector2 dir;
	Vector2 normalSurface;
	const float G = 0.1f; //10.4f;

	private float h;
	private float c;
	Quaternion startRotation;
	float time;

	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		com = rb.centerOfMass;
		startRotation = transform.rotation;
		dir = new Vector2(0,0);
		feet = GameObject.FindWithTag("feet");
    }
	 void Update()
    {
      
		if(isGrounded)
		{
				
			// RaycastHit hit;
			// Transform raycastPoint;
			// Rotate to align with terrain
			// Physics.Raycast(raycastPoint.position, Vector3.down, out hit);
			// if (hit.collider.tag == "platform")
			// {
				// isGrounded = true;
				// transform.up -= (transform.up - hit.normal) * 0.1f;
			// }
		   
		}
	}

    void FixedUpdate()
    {
        // RaycastHit hit;
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        origin = pos2D + com;
		
		// create a circle radius around player
		 Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
		 if(colliders.Length > 1)
		 {
			 Vector2 dir;
			 RaycastHit2D hit1;
			 // RaycastHit hit;
			 
			   
			   // should loop and find first one with tgis tag
				foreach (Collider2D c in colliders) 
				{
				   if (c.tag == "platform")
				   {
					   platform = c.gameObject;
					   
					   Vector2 closestPoint = c.ClosestPoint(origin);
					   var heading = origin - closestPoint;
					   var distance = heading.magnitude;
					   dir = -heading / distance;
					 
					   hit1 =  Physics2D.Raycast(origin, dir, GravityRadius);
					   hitpoint = hit1.point;
					   normalSurface = hit1.normal;
					   try
					   {
					   rb.AddForce((dir * BootGravPower), ForceMode2D.Force);
					   }
					   catch
					   {
						   rb.AddForce((new Vector2(0,-1)), ForceMode2D.Force);
						   print("force error " );
					   }
					   print(" the hit point is " + hit1.point + " the dir is " + dir + " the origin is " + origin);
					   Debug.DrawRay(origin, dir, Color.blue, 5);
					   Debug.DrawLine(origin, hitpoint, Color.red, 5);
					   
					   
					   	var slopeRotation = Quaternion.FromToRotation(transform.up, normalSurface);
					   // break;
					   
					   			if (rb.velocity.sqrMagnitude < 2) 
			{
			transform.rotation = Quaternion.Slerp(transform.rotation,slopeRotation * transform.rotation,10*Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation,slopeRotation,10*Time.deltaTime);
			}
			// else
			// {
				// var movementRotation = Quaternion.LookRotation (new Vector3(Input.GetAxis ("Horizontal"),0, Input.GetAxis("Vertical")));
				// transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation*movementRotation, 10 * Time.deltaTime);
			// }
				   }
				}
				
				
		 }
		 if (isGrounded) 
		 {
			// Vector3 transPos = transform.TransformDirection(moveDir);
			// Vector2 transPos2 =  new Vector2(transPos.x, transPos.y);
			// print("transPos2 " + transPos2);
			// rb.MovePosition(rb.position + transPos2 * moveSpeed * Time.deltaTime);
		
			// Vector2 transPos2 =  new Vector2(transPos.x, transPos.y);
			// rb.MovePosition(rb.position + transPos2 * moveSpeed * Time.deltaTime);
			float h = Input.GetAxisRaw("Horizontal");
			velocityNow = new Vector2 (1 + (h * moveSpeed)*Time.deltaTime, 0.1f);
			transform.position = new Vector2 (transform.position.x + (h * moveSpeed)*Time.deltaTime, transform.position.y);
					
			
			// Vector2 gravityup = dir;// (transform.position - platform.transform.position).normalized;
			// Vector2 bodyup = -dir; //transform.up;
				
				// rb.rigidbody.AddForce(gravityUp * gravity);
				
			// Quaternion targetRotation = Quaternion.FromToRotation(bodyup,gravityup) * transform.rotation;
			// transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time);
			// transform.rotation = Quaternion.Slerp(startRotation, dir3D, time);
			// time += Time.deltaTime;
			
			
			// var heading = origin - feet.transform.position;
			// var distance = heading.magnitude;
			// Vector2 dirFeet = -heading / distance;

			
			// if player is barely moving let's align them to obj this way

		 }
		
    }
	
		void OnCollisionEnter2D(Collision2D collision)
	{
			if (collision.gameObject.tag == "platform")
			{
				print("collision with platform");
				isGrounded = true;
				
				// rotate upright
				// transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
				// time += Time.deltaTime;

			}
			if (collision.gameObject.tag == "grabbable")
			{
			// print("entered GRABBABLE collision and tag is " + collision.gameObject.name);
			// print("the tag of obj is " + collision.gameObject.tag);
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // player.collider);
			}	
			if (collision.gameObject.tag == "kill")
			{
				// print("object is spike or fire! NAME: " + collision.gameObject.name);
				// print("the tag of obj is " + collision.gameObject.tag);
				dragCanvasHereOxyHealth.Die();
				
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