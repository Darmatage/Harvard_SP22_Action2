using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovementSounds : MonoBehaviour
{
	public PlatformChecker PC;
	public bool isGrounded;
	// public bool isWalking;
	// public bool isFlying;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		isGrounded = PC.isGrounded;
		// walking, probably should check if grounded too!
		if (isGrounded &&  Input.GetAxis("Horizontal") != 0)   //((Input.GetKeyDown("A") || Input.GetKeyDown("D") || Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.RightArrow))))
		{
			print("in thy Play audio walk!!");
			AudioHandler.PlaySoundLoop ("walk", true);
			// isWalking = true;
		}
		else
		{
			// isWalking = false;
			 AudioHandler.PlaySoundLoop ("walk", false);
		}
		// if (isGrounded && ((Input.GetKeyUp("A") || Input.GetKeyUp("D") || Input.GetKeyUp(KeyCode.RightArrow)) || (Input.GetKeyUp(KeyCode.RightArrow))))
		// {
			// AudioHandler.PlaySoundLoop ("walk", false);
		// }
			
		
		// jump
		if (isGrounded &&  Input.GetAxis("Horizontal") != 0) // ((Input.GetKeyDown("space") || (Input.GetKeyDown("W")) || (Input.GetKeyDown(KeyCode.UpArrow)))))
		{
			// jump sound
			AudioHandler.PlaySound ("jump");
		}
        
    }
}
