using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_arrows_in_space: MonoBehaviour {
    
	public float speed;
	private Vector2 velocityNow;
	private bool isGrabbable;


  Rigidbody2D rigidbody2d;

    [Header("Bools")]
    [SerializeField] bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
		print("entered a collision and tag is ");//  + collision.gameObject.tag == "platform");
		if (collision.gameObject.tag == "platform")
		{
        isGrounded = true;
		}
		else if (collision.gameObject.tag != "platform")  //(collision.gameObject.tag != "platform")
		{
			isGrounded = false;
		}
		if (collision.gameObject.tag == "grabbable")
		{
        isGrabbable = true;
		}
		else if (collision.gameObject.tag != "platform")  //(collision.gameObject.tag != "platform")
		{
			isGrabbable = false;
		}
					
    }
	void OnCollisionExit2D(Collision2D collision)
    {
			print("EXITED a collision and tag is " );// + collision.gameObject.tag == "platform");
			
			if( collision.gameObject.tag == "platform") 
			{
			isGrounded = false;
			}			
    }

    // Update is called once per frame
    void Update()
    {
			
			// not ground and not grabbable then do this (move at constant speed)
			if (!isGrounded && !isGrabbable) 
			{
					print("velocityNow " + velocityNow);
					rigidbody2d.velocity = -velocityNow/3;
					// gameObject.transform.position = velocityNow;
			}
			else 
			{

			float h = Input.GetAxisRaw("Horizontal");
			// velocityNow = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y);
			gameObject.transform.position = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y);
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