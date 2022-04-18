using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_arrows_in_space: MonoBehaviour {
    
	public float speed;
	private Vector2 velocityNow;
	public OxBarScript dragCanvasHereOxyHealth;
	private float h;
	// private bool isGrabbable;


  Rigidbody2D rigidbody2d;

    [Header("Bools")]
    [SerializeField] bool isGrounded = false;
	


    // Start is called before the first frame update
    void Start()
    {
		h = 1;
        rigidbody2d = GetComponent<Rigidbody2D>();
		print("starting script!");
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
		print("entered a collision");//  + collision.gameObject.tag == "platform");
		if (collision.gameObject.tag == "platform")
		{
			print("object is PLATFORM! NAME: " + collision.gameObject.name);
			print("the tag of obj is " + collision.gameObject.tag);
			isGrounded = true;
		}
		if (collision.gameObject.tag == "grabbable")
		{
			print("entered GRABBABLE collision and tag is " + collision.gameObject.name);
			print("the tag of obj is " + collision.gameObject.tag);
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // player.collider);
		}	
		if (collision.gameObject.tag == "kill")
		{
			print("object is spike or fire! NAME: " + collision.gameObject.name);
			print("the tag of obj is " + collision.gameObject.tag);
			dragCanvasHereOxyHealth.Die();
			
		}

					
    }
	void OnCollisionExit2D(Collision2D collision)
    {
			print("EXITED a collision and tag is " + collision.gameObject.tag);// + collision.gameObject.tag == "platform");
			if( collision.gameObject.tag == "platform") 
			{

				isGrounded = false;
				if (h == 1)
				{
				print("right " + h);
				rigidbody2d.velocity = -velocityNow/3;
				}
				if (h == -1)
				{
					print("left " +h);
					rigidbody2d.velocity = velocityNow/3;
				}
			}	
		
    }

    // Update is called once per frame
    void Update()
    {
			
			// not ground and not grabbable then do this (move at constant speed)
			if (isGrounded)
			{

				float h = Input.GetAxisRaw("Horizontal");
				print("the horiz direction is " + h);
				// velocityNow = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y);
				transform.position = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y);
					if(h != 0) 
					{
						velocityNow = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, 0.1f);
					}
			}
			
		
    }

	public Vector2 getVelocity() {
		return velocityNow;
	}
   
	}