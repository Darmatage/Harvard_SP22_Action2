using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetOnPlatforms : MonoBehaviour
{
	Quaternion startRotation;
	float time;
	
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
		// print("entered a collision");//  + collision.gameObject.tag == "platform");
		if (collision.gameObject.tag == "platform")
		{
			transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
			time += Time.deltaTime;
			

		}
	}
}
