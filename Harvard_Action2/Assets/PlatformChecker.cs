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
	public float miniPlatformPullerOther = -0.4f;
	
	// non upright platforms
		public bool isGroundedOther = false;
		public bool isLeftRight = false; // for left and right platforms 
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
				   if (c.tag == "platformOther")
				   {
					   
					   isGroundedOther = true;
					   
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
                         
						   reorientToGround();
						   isGrounded = true;
              }
			   if (other.gameObject.tag == "platformLeft"){
						   AudioHandler.PlaySound ("land");
						   reorientLeft();
						   isGroundedOther = true;
						   isLeftRight = true;

              }
			   if (other.gameObject.tag == "platformRight"){
						   AudioHandler.PlaySound ("land");
						   reorientRight();
						   isGroundedOther = true;
						   isLeftRight = true;
              }
			   if (other.gameObject.tag == "platformUpsidedown"){
						   AudioHandler.PlaySound ("land");
						   print(" I am platformUpsidedown ");
						   reorientUpsideDown();
						   isGroundedOther = true;
              }
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "platform"){
						isGrounded = false;
              }
			  if (other.gameObject.tag == "platformLeft"){
							StartCoroutine(delay());
						   isGroundedOther = false;
						   isLeftRight = false;
              }
			  if (other.gameObject.tag == "platformRight"){
							StartCoroutine(delay());
						   isGroundedOther = false;
						   isLeftRight = false;
              }
			  if (other.gameObject.tag == "platformUpsidedown"){
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
	   
	      public void reorientUpsideDown()
	   {
		   Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		   var tr = player.transform;
		   
		   // tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.identity, 100f * Time.deltaTime);
		   tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.identity, 1f);
		   // tr.Rotate(0, Time.deltaTime * 30, 0, Space.Self);
		      Vector3 rot = tr.rotation.eulerAngles;
			 rot = new Vector3(rot.x,rot.y,rot.z+180);
			 tr.rotation = Quaternion.Euler(rot);
		   // rb.AddForce(Vector2.down*10f, ForceMode2D.Impulse);
		   tr.position = tr.position + new Vector3(0,-miniPlatformPullerOther,0);
		
		   
	   }
	    public void reorientRight()
	   {
		   Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		   var tr = player.transform;
		   
		   // tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.identity, 100f * Time.deltaTime);
		   tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.identity, 1f);
		   // tr.rotation *= Quaternion.Euler(0, -90, 0);
		     Vector3 rot = tr.rotation.eulerAngles;
			 rot = new Vector3(rot.x,rot.y,rot.z+90);
			 tr.rotation = Quaternion.Euler(rot);
		   // rb.AddForce(Vector2.down*10f, ForceMode2D.Impulse);
		   // tr.position = tr.position + new Vector3(miniPlatformPullerOther,0,0);
		
		   
	   }
	    public void reorientLeft()
	   {
		   Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		   var tr = player.transform;
		   
		   // tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.identity, 100f * Time.deltaTime);
		   tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.identity, 1f);
		      Vector3 rot = tr.rotation.eulerAngles;
			 rot = new Vector3(rot.x,rot.y,rot.z-90);
			 tr.rotation = Quaternion.Euler(rot);
		   // rb.AddForce(Vector2.down*10f, ForceMode2D.Impulse);
		   // tr.position = tr.position + new Vector3(-miniPlatformPullerOther,0,0);
		
		   
	   }
	   
	   // add a slight delay so player can stick a little longer
	   IEnumerator delay()  
		{
			print("i'm in delay land");
			yield return new WaitForSeconds(0.4f);
			print("i'm in delay AFTER land");
		}
}
