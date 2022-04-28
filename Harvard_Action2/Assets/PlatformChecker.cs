using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check to see if player is grounded on platform
public class PlatformChecker : MonoBehaviour
{
    // Start is called before the first frame update
	public bool isGrounded = false;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "platform"){
							
                           isGrounded = true;
              }
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "platform"){
						isGrounded = false;
              }
       }
}
