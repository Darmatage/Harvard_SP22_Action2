using UnityEngine;
using System.Collections;

public class pickup_throw_obj2 : MonoBehaviour {

	public bool grabbed;
	RaycastHit2D hit;
	public float distance=2f;
	public Transform holdpoint;
	public LayerMask notgrabbed;

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
	
	
	
	
	
	// Use this for initialization
	void Start () 
	{
		GameObject ThisPlayer = GameObject.FindGameObjectWithTag("Player");
		// print("the pos of this player is " + ThisPlayer.transform.position);
		PersonRB = ThisPlayer.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
		 if (Input.GetMouseButtonDown(0))
		{
			
			print("first mouse click 0");
			if(!grabbed)
			{
				Physics2D.queriesStartInColliders=false;
				
				print("not grabbed! " + transform.position + " " + Vector2.right*transform.localScale.x + " " + distance);

				hit = Physics2D.Raycast(transform.position,Vector2.right*transform.localScale.x,distance);
				
				// hit collider rotate
				hit.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
				Debug.DrawRay(hit.transform.position, transform.TransformDirection(Vector3.forward)*hit.distance, Color.yellow);
				
				bool madeContact = hit.collider.tag=="grabbable";
				bool isNull =  hit.collider!=null ;
				print("hit collider made contact true? " + isNull + " is grabbable? " + madeContact);
				
				if(hit.collider!=null && hit.collider.tag=="grabbable")
				{
					grabbed=true;

				}


				//grab aka if grabbed = true 
			}else if(!Physics2D.OverlapPoint(holdpoint.position,notgrabbed))
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

		Gizmos.DrawLine(transform.position,transform.position+Vector3.right*transform.localScale.x*distance);
		
	}
}