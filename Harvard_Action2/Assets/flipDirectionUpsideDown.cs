using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipDirectionUpsideDown : MonoBehaviour
{
	public allPlatformChecker apc;
	public Animator animTorso;
	Vector3 origAnimRot;
	public bool flipped = false;
	public bool flippedNorm = false;
	
    // Start is called before the first frame update
    void Start()
    {
        origAnimRot = animTorso.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
		print("this is flipper checker");
		var h = Input.GetAxis("Horizontal");
		print("apc.typeOfPlatform  " + apc.typeOfPlatform  + " " + h);
        if(apc.typeOfPlatform == "platformUpsidedown" && flipped == false) // && h >= 0)
		{	
			flipped = true;
			print("i am flipping! " );
			animTorso.transform.Rotate(0, 180, 0);
			
		}
		else if(apc.typeOfPlatform != "platformUpsidedown" && flipped == true)
		{
			flipped = false;
			print("i am flippingAgain! " + origAnimRot );
			// animTorso.transform.Rotate(origAnimRot);
			animTorso.transform.Rotate(0, 180, 0);
		}
		
		if (apc.typeOfPlatform == "platform")
		{
			print("i am flippingAgainNorm! " + origAnimRot );
			animTorso.transform.Rotate(origAnimRot);
		}
    }
}
