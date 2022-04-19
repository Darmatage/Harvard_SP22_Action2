using UnityEngine;
using System.Collections;

public class pickup_throw_obj2 : MonoBehaviour {

	public bool grabbed;
	RaycastHit2D hit;
	public float distance=2f;
	public Transform holdpoint;
	public LayerMask notgrabbed;
	
	// public Camera mainCamera;

	public aim2_static pickupPoint;
	
	// for shooting
	public Transform firePoint; // this is where object fires from
	Vector2 lookDirection;
    float lookAngle;
	public float bulletSpeed = 10f;
	
	// player 
	private Rigidbody2D PersonRB;
	private GameObject ThisPlayer;
	private Vector3 originalOrientation;
	
	// for aiming
	private Vector3 direction;
	
	// to pass along
	// private Quaternion oldRot;
	// private Vector2 objVelocity;
	public Transform redTargeter;
	
	//ox bar connector
	// public OxBarScript oxBar;
	

	
	
	
	
	// Use this for initialization
	void Start () 
	{
	   ThisPlayer = GameObject.FindGameObjectWithTag("Player");
	   originalOrientation = ThisPlayer.transform.position;
		// print("the pos of this player is " + ThisPlayer.transform.position);
		PersonRB = ThisPlayer.GetComponent<Rigidbody2D>();
		grabbed = false;
		
		
		// hit = Physics2D.Raycast(holdpoint.position,Vector2.right*transform.localScale.x,distance);
	}
	
	// Update is called once per frame
	void Update () {
		
		// Gets a vector that points from the player's position to the target's.
		// https://docs.unity3d.com/2018.3/Documentation/Manual/DirectionDistanceFromOneObjectToAnother.html
		var heading = holdpoint.position - redTargeter.position;
		var distance = heading.magnitude;
		direction = heading / distance; // This is now the normalized direction.

		 if (Input.GetKeyDown(KeyCode.R))
		{
			// Debug.DrawRay(hit.transform.position, transform.TransformDirection(Vector2.right)*50, Color.blue, 2, false);
			if(!grabbed)
			{
				Physics2D.queriesStartInColliders=false;
				
				float DistOfVect = Vector3.Distance (holdpoint.position, redTargeter.position);
				hit = Physics2D.Raycast(holdpoint.position,-direction,DistOfVect);
				
				// Debug.DrawRay(hit.transform.position, directionOfTarget*DistOfVect, Color.yellow, 2, false);

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
					// PersonRB.velocity =  -lookDirection * bulletSpeed;
					PersonRB.velocity =  direction * bulletSpeed;

					// launch object
					// obj.GetComponent<Rigidbody2D>().velocity = lookDirection * bulletSpeed;
					obj.GetComponent<Rigidbody2D>().velocity = -direction * bulletSpeed;
					 
					
					
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
					PersonRB.velocity =  direction * 3f; // bulletSpeed;
					
					
					// roation reducer;
					// float smooth = Time.deltaTime;
					// oxBar.TakeDamage(smooth);
					// ThisPlayer.transform.rotation = Quaternion.identity*smooth;
					// ThisPlayer.transform.Rotate(originalOrientation * smooth);
					
					
		}
	}
	
	
	// public Quaternion getObjRotation()
	// {
		// return oldRot;
	// }
	
	// public Vector2 getObjVelcity()
	// {
		// return objVelocity;
	// }

	// draw raycast to show distance for picking up
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		
		Gizmos.DrawLine(holdpoint.position, redTargeter.position);
		// print("the holdpoint and the world " + holdpoint.position + " "+  worldMousePosition);
		
		
		// Vector3 directionOfTarget = redTargeter.position.normalized;
		// Gets a vector that points from the player's position to the target's.
		// https://docs.unity3d.com/2018.3/Documentation/Manual/DirectionDistanceFromOneObjectToAnother.html
		var heading = holdpoint.position - redTargeter.position;
		var distance = heading.magnitude;
		var direction2 = heading / distance; // This is now the normalized direction.

		
		Gizmos.color = Color.red;
		Gizmos.DrawRay(holdpoint.position,-direction2);
		
		
	}
}