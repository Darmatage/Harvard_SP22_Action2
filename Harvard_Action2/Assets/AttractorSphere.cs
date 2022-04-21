using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorSphere : MonoBehaviour
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
	
	// should i add feet so that object stands up?
	GameObject feet;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		com = rb.centerOfMass;
    }
	 void Update()
    {
      moveDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;  
		// if (Input.GetKeyDown(KeyCode.Space))
		// {
			   
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
		 if(colliders.Length > 1)
		 {
			 Vector2 dir;
			 RaycastHit2D hit1;
			 Vector2 normalSurface;
			   
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
					   normalSurface = hit1.normal;
					   rb.AddForce((dir * BootGravPower), ForceMode2D.Force);
					   print(" the hit point is " + hit1.point + " the dir is " + dir + " the origin is " + origin);
					   Debug.DrawRay(origin, dir, Color.blue, 5);
					   break;
				   }
				}
				
				
		 }
		 if (isGrounded) 
		 {
			Vector3 transPos = transform.TransformDirection(moveDir);
			Vector2 transPos2 =  new Vector2(transPos.x, transPos.y);
			rb.MovePosition(rb.position + transPos2 * moveSpeed * Time.deltaTime);
			 
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