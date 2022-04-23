using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpFlip : MonoBehaviour
{
	Rigidbody2D rb;
	public float tapSpeed = 0.5f;
	public float jumpPower = 10f;
	float lastTapTime = 0;
   

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		// if (Input.GetKeyDown(KeyCode.Space))
		// {
			// print("jump is pushed");
			// rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			// if ((Time.time - lastTapTime) < tapSpeed)
			// {
				// rb.AddTorque(10 * Time.deltaTime);
			// }
		// }
	
	}
		
}
