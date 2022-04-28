using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_arrows2 : MonoBehaviour
{
   	public float speed;
	private Vector2 velocityNow;
	public OxBarScript dragCanvasHereOxyHealth;
	private float h;
	private float c;
	Quaternion startRotation;
	float time;
	// public GameObject underFeet;
	public PlatformChecker underMyFeet;
	RaycastHit2D hit;
	// private bool isGrabbable;


  Rigidbody2D rigidbody2d;

    [Header("Bools")]
    [SerializeField] bool isGrounded = false;
	


    // Start is called before the first frame update
    void Start()
    {
		h = 1;
        rigidbody2d = GetComponent<Rigidbody2D>();
		startRotation = transform.rotation;
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
		
		if (collision.gameObject.tag == "platform")
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
		if (collision.gameObject.tag == "grabbable")
		{
			// print("entered GRABBABLE collision and tag is " + collision.gameObject.name);
			// print("the tag of obj is " + collision.gameObject.tag);
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // player.collider);
		}	

					
    }
	void OnCollisionExit2D(Collision2D collision)
    {
		
		h = Input.GetAxisRaw("Horizontal");
			// print("EXITED a collision and tag is " + collision.gameObject.tag + " h on exit " + h);// + collision.gameObject.tag == "platform");
			if( collision.gameObject.tag == "platform") 
			{

				// isGrounded = false;
				rigidbody2d.velocity = h * velocityNow*speed;
			}
	}			
		      
    

    // Update is called once per frame
    void Update()
    {
				isGrounded = underMyFeet.isGrounded;
		
		
					   // Vector2 feetPos = new Vector2(underFeet.transform.position.x, underFeet.transform.position.y);
					   // Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
					   // Vector2 com = rigidbody2d.centerOfMass;
					   // playerPos = playerPos + com;
					   // var heading = playerPos - feetPos;
					   // var distance = heading.magnitude;
					   // print("distance to feet is " + playerPos);
					   // Vector2 dir = -heading / distance;

					   // hit =  Physics2D.Raycast(playerPos, dir, 100);
					   // Debug.DrawRay(playerPos, dir, Color.green, 1);
					   // if(hit.collider.tag == "platform")
					   // {
						   // isGrounded = true;
						   	// transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
							// time += Time.deltaTime;
					   // }
					   // else
					   // {
						   // isGrounded = false;
					   // }
			
			// not ground and not grabbable then do this (move at constant speed)
			if (isGrounded)
			{

				h = Input.GetAxisRaw("Horizontal");
				// print("the horiz direction is " + h);
				// velocityNow = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y);
				transform.position = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y);
					// print("his velocity is " + velocityNow + " transform.position.x " + transform.position.x + " h " + h);

						velocityNow = new Vector2 (1 + (h * speed)*Time.deltaTime, 0.1f);
			
					print("his velocity is " + velocityNow);
					
				//Stop the Rigidbody from rotating
				rigidbody2d.freezeRotation = true;
			}
			else 
			{
				//resume the Rigidbody  rotating
				rigidbody2d.freezeRotation = false;
			}
			
		
    }

	public Vector2 getVelocity() {
		return velocityNow;
	}
   
	}