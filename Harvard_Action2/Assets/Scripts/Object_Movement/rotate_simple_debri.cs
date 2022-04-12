using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_simple_debri : MonoBehaviour
{
	
	 public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

	void Update()
	{
		// spins a 2D physics object clockwise at 10 degrees per second
		rb.AddTorque(10 * Time.deltaTime);
	}
}
