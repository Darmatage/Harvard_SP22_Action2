using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedFromFeet : MonoBehaviour
{
public bool isGrounded = false;
	
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
