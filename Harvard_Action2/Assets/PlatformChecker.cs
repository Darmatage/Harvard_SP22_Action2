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
	
	void FixedUpdate()
	{
		RaycastHit hit;

        Vector3 p1 = transform.position;
        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        // if (Physics.SphereCast(p1, 2, transform.forward, out hit, 10))
        // {
			// print("I am in spherecast " + hit.collider.tag);
            // distanceToObstacle = hit.distance;
			// if(hit.collider.tag == "platform")
			// {
				// isGrounded = true;
			// }
        // }
		 Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
		 if(colliders.Length > 1)
		 {
			 
			   // should loop and find first one with tgis tag
				foreach (Collider2D c in colliders) 
				{
				   if (c.tag == "platform")
				   {
					   
					   isGrounded = true;
				   }
				}
		 }
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
