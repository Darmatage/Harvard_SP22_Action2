using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// public bool isPlatform = false;
// public bool isRight = false;
// public bool isLeft = false;
// public bool isUpsideDown = false;


// this checks the TYPE of platform and passes this info on to the movement scripts
public class checkGrounded : MonoBehaviour
{
	public bool objectCollision = false;
	public String typeOfPlatform;
    // Start is called before the first frame update
    void Start()
    {
        typeOfPlatform = "";
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
		print("colliders.Length " + colliders.Length);
		
		foreach (Collider2D other in colliders) 
				{
				   if(other.tag == "platform" || other.tag == "platformLeft" || other.tag == "platformRight" || other.tag == "platformUpsidedown" )
					{
						objectCollision = true;
						typeOfPlatform = other.tag;
						
					}
					else
					{
						typeOfPlatform = "did not find platform";
					}
				}
    }
	
}
