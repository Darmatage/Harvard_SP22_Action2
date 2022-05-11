using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check to see if player is grounded on platform
public class PlatformChecker : MonoBehaviour
{
	GameObject player;
    // Start is called before the first frame update
	public bool isGrounded = false;
	public float miniPlatformPuller = -0.2f;
	
    void Start()
    {
        player =  this.transform.parent.gameObject;
		
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
					   
					   // use a slower roation if collideers hit
					   var tr = player.transform;
						tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.identity, 100f * Time.deltaTime);
				   }
				}
		 }
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "platform"){
						   AudioHandler.PlaySound ("land");
                           isGrounded = true;
						   reorientToGround();
              }
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "platform"){
						isGrounded = false;
              }
       }
	   
	   public void reorientToGround()
	   {
		   Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		   var tr = player.transform;
		   // tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.identity, 100f * Time.deltaTime);
		   tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.identity, 1f);
		   // rb.AddForce(Vector2.down*10f, ForceMode2D.Impulse);
		   tr.position = tr.position + new Vector3(0,miniPlatformPuller,0);
		
		   
	   }
}
