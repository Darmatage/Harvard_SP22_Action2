using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_arrows_in_space: MonoBehaviour {
    
	public Animator animator;
	public float speed;
	private Vector2 velocityNow;
	public OxBarScript dragCanvasHereOxyHealth;
	private float h;
	private float c;
	Quaternion startRotation;
	float time;
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
		// print("entered a collision");//  + collision.gameObject.tag == "platform");
		if (collision.gameObject.tag == "platform")
		{
			isGrounded = true;
			transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
			time += Time.deltaTime;
			

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
		
		h = Input.GetAxisRaw("Horizontal");
			// print("EXITED a collision and tag is " + collision.gameObject.tag + " h on exit " + h);// + collision.gameObject.tag == "platform");
			if( collision.gameObject.tag == "platform") 
			{

				isGrounded = false;
				rigidbody2d.velocity = h * velocityNow*speed;
			}
	}			
		
    

    // Update is called once per frame
    void Update()
    {
			
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

	public Vector2 getVelocity() {
		return velocityNow;
	}
   
	}