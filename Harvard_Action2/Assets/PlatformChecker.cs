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
	
	// non upright platforms
		public bool isGroundedOther = false;
		public GameObject gravHelper;
		RaycastHit hit;
		
	
    void Start()
    {
        player =  this.transform.parent.gameObject;
		
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - gravHelper.transform.position;
		dir = dir.normalized;
		Ray ray = new Ray(transform.position, dir);
		Physics.Raycast(ray, out hit);
		Debug.DrawLine(transform.position, gravHelper.transform.position, Color.blue);
    }
	
	void FixedUpdate()
	{
		Vector3 dir = transform.position - gravHelper.transform.position;
		dir = dir.normalized;
		Ray ray = new Ray(transform.position, dir);
		Physics.Raycast(ray, out hit);
		// Debug.DrawRay(transform.position, dir*5, Color.red, 2.0f);

        // Vector3 p1 = transform.position;
        float distanceToObstacle = 0;

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
			   if (other.gameObject.tag == "platformOther"){
						   AudioHandler.PlaySound ("land");
                           isGroundedOther = true;
						   reorientToGround2();
              }
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "platform"){
						isGrounded = false;
              }
				   if (other.gameObject.tag == "platformOther"){
				   isGroundedOther = false;
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
	   
	      public void reorientToGround2()
	   {
		   Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		   var tr = player.transform;
		   
		   // tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.identity, 100f * Time.deltaTime);
		   tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.identity, 1f);
		   // rb.AddForce(Vector2.down*10f, ForceMode2D.Impulse);
		   tr.position = tr.position + new Vector3(0,miniPlatformPuller,0);
		
		   
	   }
}
