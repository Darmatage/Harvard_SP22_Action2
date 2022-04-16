using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script ensures that nothing 'grabbable' can interfere with a players movement
public class collisionBlock: MonoBehaviour {
  
  private Collision2D player;


    // Start is called before the first frame update
    void Start()
    {
        // rigidbody2d = GetComponent<Rigidbody2D>();
		player = GetComponent<Collision2D>();
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
		print("entered a collision and tag is ");//  + collision.gameObject.tag == "platform");
		if (collision.gameObject.tag == "grabbable");
		{
			print("entered a collision and tag is grabbable");
         Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // player.collider);
		}
					
    }

   
	}