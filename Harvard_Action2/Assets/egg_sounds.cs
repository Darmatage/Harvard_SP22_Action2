using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg_sounds : MonoBehaviour
{
	public EscapePodMovement3 EPM;
	public bool isGrounded;
	// public OxygenMovement2 oxCheck;
	public AudioHandlerObj AHO;
	public Grab_throw_3_egg GT;
	public bool grabbed = false;
	public string platformName = "";
	// GameObject gameHandler;
	// public bool isWalking;
	// public bool isFlying;
	
	
    // Start is called before the first frame update
    void Start()
    {
        // gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
		isGrounded = EPM.isGrounded;
		
		// walking, probably should check if grounded too!
		if (Input.GetAxis("Horizontal") != 0)   //((Input.GetKeyDown("A") || Input.GetKeyDown("D") || Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.RightArrow))))
		{
			AHO.PlaySoundLoop ("egg_walk", true);
			// isWalking = true;
		}
		else
		{
			// isWalking = false;
			 AHO.PlaySoundLoop ("egg_walk", false);
		}
		if(Input.GetMouseButtonDown(1) || (Input.GetKeyDown(KeyCode.E)))
		// if(oxCheck.OxygenOn)
		{
			print("playing the ox from EGG");
			AHO.PlaySoundLoop ("ox", true);
		}
		if(Input.GetMouseButtonUp(1) || (Input.GetKeyUp(KeyCode.E)))
		{
			AHO.PlaySoundLoop ("ox", false);
		}
		// if (isGrounded && ((Input.GetKeyUp("A") || Input.GetKeyUp("D") || Input.GetKeyUp(KeyCode.RightArrow)) || (Input.GetKeyUp(KeyCode.RightArrow))))
		// {
			// AudioHandler.PlaySoundLoop ("walk", false);
		// }
			
		
		// jump
		if (isGrounded && ((Input.GetKeyDown("space") || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))))) // Input.GetAxis("Vertical") != 0)
		{
			// jump sound
			AudioHandler.PlaySound ("jump");
			
		}
		if(grabbed && Input.GetMouseButtonDown(0))
		{
			print("I am throwing!");
			AudioHandler.PlaySound ("throw_debris");
		}
		if(GameHandler.CurrentHealth <= 20f)
		{
			AHO.PlaySoundLoop ("low_ox", true);
		}
		else{
			AHO.PlaySoundLoop ("low_ox", false);
		}
		
		grabbed = GT.grabbed;
        
    }
	
	 void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
           
		   
		   if(collision.gameObject.name != platformName)
		   {
		   AudioHandler.PlaySound ("egg_jump");
		   }
		   
		   platformName = collision.gameObject.name;
		   
        }
    }
}
