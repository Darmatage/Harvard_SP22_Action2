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
      moveDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;  
		if(isGrounded)
		{
			// 	rotate smoothly back upright
				// transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
				// time += Time.deltaTime;
				// rb.freezeRotation = true;
				
				// Vector2 gravityup = (transform.position - platform.transform.position).normalized;
				// Vector2 bodyup = transform.up;
				
				// rb.rigidbody.AddForce(gravityUp * gravity);
				
				// Quaternion targetRotation = Quaternion.FromToRotation(bodyup,gravityup) * transform.rotation;
				// transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
		}
		else 
		{
				//resume the Rigidbody  rotating
				// rb.freezeRotation = false;
			}
		
	}

    void FixedUpdate()
    {
        // RaycastHit hit;
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        origin = pos2D + com;
		// origin = feet.transform.position;
		// RaycastHit2D hit = (Physics2D.CircleCast(origin, GravityRadius, transform.forward, 0));

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        // if (Physics2D.CircleCast(p1, rb.height / 2, transform.forward, out hit, GravityRadius))
			
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
					   
					   
					   // -------
					   // Rigidbody2D rbToAttract  = platform.GetComponent<Rigidbody2D>();
					   // Vector3 direction = rb.position - rbToAttract.position;
					   // float distance = direction.magnitude;
					   // print("distance " + distance);

						// if (distance == 0f)
							// return;
						
						// if (distance <= 10f) 
						// {
							// if ( isGrounded ) {
								// distance = 1;
							// }
								// float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
								// Vector3 force = direction.normalized * forceMagnitude;

			// rbToAttract.AddForce(force);
			
			// force 2
			// get the size of this object
			// var renderer = gameObject.GetComponent<Renderer>();
			// float width = 3f; // renderer.bounds.size.x;
			// print("my width is " + width);
			// float forceMagnitudeF2dg = -G * (rb.mass * rbToAttract.mass) / (width/2);
			// Vector3 forceF2dg = direction.normalized * forceMagnitude;
			// print("my force vector is " + forceF2dg);
			// rb.AddForce(-forceF2dg);
					   
					   // --------
					   
					   
					   Vector2 closestPoint = c.ClosestPoint(origin);
					   var heading = origin - closestPoint;
					   var distance = heading.magnitude;
					   dir = -heading / distance;
					 
					   hit1 =  Physics2D.Raycast(transform.position, dir, GravityRadius);
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
					   // break;
				   }
				}
				
				
		 }
		 if (isGrounded) 
		 {
			Vector3 transPos = transform.TransformDirection(moveDir);
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
			var slopeRotation = Quaternion.FromToRotation(transform.up, normalSurface);
			// transform.rotation = Quaternion.Slerp(transform.rotation,slopeRotation * transform.rotation,10*Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation,slopeRotation,10*Time.deltaTime);
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