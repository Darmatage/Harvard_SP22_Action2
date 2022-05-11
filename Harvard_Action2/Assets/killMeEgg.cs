using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killMeEgg : MonoBehaviour
{
	
	public OxBarScript dragCanvasHereOxyHealth;
		public bool isFiltering = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
		
		if (collision.gameObject.tag == "platform")
		{
			// isGrounded = true;
		}	
		if (collision.gameObject.tag == "virus")
		{
			AudioHandler.PlaySound ("spike");
			// print("object is spike or fire! NAME: " + collision.gameObject.name);
			// print("the tag of obj is " + collision.gameObject.tag);
			dragCanvasHereOxyHealth.Die();
			
		}
		if (collision.gameObject.tag == "grabbable")
		{
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // player.collider);
		}	
		if (collision.gameObject.tag == "OxRefill")
        {
			dragCanvasHereOxyHealth.isFiltering = true;
		}

					
    }
	void OnCollisionExit2D(Collision2D collision)
    {
			if( collision.gameObject.tag == "OxRefill") 
			{

				// isGrounded = false;
				dragCanvasHereOxyHealth.isFiltering = false;
				// AudioHandler.PlaySound ("ox_refill");
				// rigidbody2d.velocity = h * velocityNow*speed;
			}
	}		
}
