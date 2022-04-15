using UnityEngine;
using System.Collections;

public class pickup_throw_obj2 : MonoBehaviour {

	public bool grabbed;
	RaycastHit2D hit;
	public float distance=2f;
	public Transform holdpoint;
	public LayerMask notgrabbed;
	
	public Camera mainCamera;

	public aim2_static pickupPoint;
	
	// for shooting
	public Transform firePoint; // this is where object fires from
	Vector2 lookDirection;
    float lookAngle;
	public float bulletSpeed = 10f;
	private Rigidbody2D PersonRB;
	
	
	// to pass along
	private Quaternion oldRot;
	private Vector2 objVelocity;
	public Transform redTargeter;

	
	
	
	
	// Use this for initialization
	void Start () 
	{
		GameObject ThisPlayer = GameObject.FindGameObjectWithTag("Player");
		// print("the pos of this player is " + ThisPlayer.transform.position);
		PersonRB = ThisPlayer.GetComponent<Rigidbody2D>();
		
		// hit = Physics2D.Raycast(holdpoint.position,Vector2.right*transform.localScale.x,distance);
	}
	
	// Update is called once per frame
	void Update () {
		
		// get the mouse screen pos
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		 
		 // convert it -- dont thibk i need ti do this in 2d
		Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
	
		 if (Input.GetKeyDown(KeyCode.R))
		{
			// Debug.DrawRay(hit.transform.position, transform.TransformDirection(Vector2.right)*50, Color.blue, 2, false);
			if(!grabbed)
			{
				Physics2D.queriesStartInColliders=false;
				
				float DistOfVect = Vector3.Distance (holdpoint.position, redTargeter.position);

				// issue here is the thing that is rotating is not the 'grabber' but the rotator...so let's connect raycast to pickupPoint, aka holdpoint
				Vector3 directionOfTarget = redTargeter.position.normalized;
				hit = Physics2D.Raycast(holdpoint.position,redTargeter.position,DistOfVect);
				
				// hit collider rotate
				// hit.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

				
				Debug.DrawRay(hit.transform.position, redTargeter.position*DistOfVect, Color.yellow, 2, false);
				print("raycast ! " + hit.transform.position + " " + transform.TransformDirection(Vector3.forward)*hit.distance);
				
				bool madeContact = hit.collider.tag=="grabbable";
				bool isNull =  hit.collider!=null ;
				print("hit collider made contact true? " + isNull + " is grabbable? " + madeContact);
				
				if(hit.collider!=null && hit.collider.tag=="grabbable")
				{
					grabbed=true;

				}


				//grab aka if grabbed = true 
			}
			else if(!Physics2D.OverlapPoint(holdpoint.position,notgrabbed))
			{
				grabbed=false;

				if(hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null) // throw
				{
					
					// mouse directions for shooting
					lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
					firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
					
					// get obj we throw 
					GameObject obj = hit.collider.gameObject;
					obj.transform.position = firePoint.position;
					obj.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
					
					//launch player
					PersonRB.velocity =  -lookDirection * bulletSpeed;

					// launch object
					obj.GetComponent<Rigidbody2D>().velocity = lookDirection * bulletSpeed;
					 
					
					
				}

			}


		}

		if (grabbed) // this holds the object in place //
		{

						hit.collider.gameObject.transform.position = holdpoint.position;
						hit.collider.gameObject.transform.rotation =  pickupPoint.Update();
						hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = PersonRB.velocity;
						
		}
		
		// Oxygen thruster
		if(!grabbed && (Input.GetKeyDown(KeyCode.E)))
		{
					lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
					firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
					PersonRB.velocity =  -lookDirection * bulletSpeed;
					
					
		}
	}
	
	
	public Quaternion getObjRotation()
	{
		return oldRot;
	}
	
	public Vector2 getObjVelcity()
	{
		return objVelocity;
	}

	// draw raycast to show distance for picking up
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		
		 // get the mouse screen pos
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		 
		 // convert it -- dont thibk i need ti do this in 2d
		Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

		
		// Ray ray = new Ray(holdpoint.position );
		// Debug.DrawRay(holdpoint.position, mousePosition);
		Vector2 rightHoldPos = new Vector2(holdpoint.position.x, 0);
		Gizmos.DrawLine(holdpoint.position, redTargeter.position);
		print("the holdpoint and the world " + holdpoint.position + " "+  worldMousePosition);

		// Gizmos.DrawRay(holdpoint.position,transform.TransformDirection(Vector2.right));
		
		
	}
}