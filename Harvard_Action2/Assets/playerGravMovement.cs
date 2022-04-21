using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGravMovement : MonoBehaviour
{

	public float G = 1.4f;
	public float jumpAmount = 10f;

	private  Rigidbody2D rb;

	void Start() {
	
	rb = gameObject.GetComponent<Rigidbody2D>();

	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
		}
		float h = Input.GetAxis("Horizontal");
		// if (h > .18f) h = 0.18f;
		// if (h>0) 
		// {
			// float rightForce = Mathf.Sqrt(1 * -2 * (Physics2D.gravity.y * G));
			// rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		// }
		// if (h<0) 
		// {
			print("the h is " + h);
			
			float horizontalForce = h*Mathf.Sqrt(1 * -2 * (-0.1f * G));
			// .05446258
			print("the horiz force is  " + horizontalForce);
			rb.AddForce(new Vector2(0, horizontalForce), ForceMode2D.Impulse);
		// }
	}

}