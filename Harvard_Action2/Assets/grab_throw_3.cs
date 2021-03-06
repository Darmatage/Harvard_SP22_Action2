using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab_throw_3 : MonoBehaviour
{

	public bool grabbed;
	RaycastHit2D hit;
	public float distance = 2f;
	public Transform grabpoint;
	public Transform handpoint;
	public LayerMask notgrabbed;

	// public Camera mainCamera;

	public aim2_static pickupPoint;

	// for shooting
	public Transform firePoint; // this is where object fires from
	Vector2 lookDirection;
	float lookAngle;
	public float bulletSpeed = 10f;
	public float thrusterSpeed = 3f;

	// player 
	private Rigidbody2D PersonRB;
	private GameObject ThisPlayer;
	private Vector3 originalOrientation;

	// for aiming
	private Vector3 direction;

	// to pass along

	public Transform redTargeter;
	
	
	public PlatformChecker platformChecker;
	public bool isGrounded;
	public GameObject feet;

	






	// Use this for initialization
	void Start()
	{
		
		
		// platformChecker = GameObject.FindWithTag("feet").GetComponent<PlatformChecker>();
		ThisPlayer = GameObject.FindGameObjectWithTag("Player");
		originalOrientation = ThisPlayer.transform.position;
		// print("the pos of this player is " + ThisPlayer.transform.position);
		PersonRB = ThisPlayer.GetComponent<Rigidbody2D>();
		grabbed = false;


	}

	// Update is called once per frame
	void Update()
	{
		// feet.SetActive(true);
		isGrounded = platformChecker.isGrounded;
		// Gets a vector that points from the player's position to the target's.
		// https://docs.unity3d.com/2018.3/Documentation/Manual/DirectionDistanceFromOneObjectToAnother.html
		var heading = grabpoint.position - redTargeter.position;
		var distance = heading.magnitude;
		direction = heading / distance; // This is now the normalized direction.

		if (Input.GetMouseButtonDown(0))
		{
			// Debug.DrawRay(hit.transform.position, transform.TransformDirection(Vector2.right)*50, Color.blue, 2, false);
			if (!grabbed)
			{
				Physics2D.queriesStartInColliders = false;

				float DistOfVect = Vector3.Distance(grabpoint.position, redTargeter.position);
				hit = Physics2D.Raycast(grabpoint.position, -direction, DistOfVect);

				// Debug.DrawRay(hit.transform.position, directionOfTarget*DistOfVect, Color.yellow, 2, false);

				// bool madeContact = hit.collider.tag == "grabbable";
				// bool isNull = hit.collider != null;
				// print("hit collider made contact true? " + isNull + " is grabbable? " + madeContact);

				if (hit.collider != null && hit.collider.tag == "grabbable")
				{
					grabbed = true;

				}


				//grab aka if grabbed = true 
			}
			else if (!Physics2D.OverlapPoint(grabpoint.position, notgrabbed))
			{
				grabbed = false;

				if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) // throw
				{
					
					// mouse directions for shooting
					lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
					firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

					// get obj we throw 
					GameObject obj = hit.collider.gameObject;
					obj.transform.position = firePoint.position;
					obj.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
					
					// this aims to allow us to launch player even when isGrounded
					platformChecker.isGrounded = false;
					feet.SetActive(false);
					//launch player
					// PersonRB.velocity =  -lookDirection * bulletSpeed;
					
					//Kai 5-14
					var objRb = obj.GetComponent<Rigidbody2D>();
					var objMass = objRb.mass;
					if(objMass < 1) objMass = objMass + 1;
					else objMass = objMass-1;
					
					// change
					// PersonRB.velocity = direction * bulletSpeed;
					
					PersonRB.AddForce(direction * bulletSpeed * objMass, ForceMode2D.Impulse);
					// AudioHandler.PlaySound ("throw_debris");
					// launch object
					// obj.GetComponent<Rigidbody2D>().velocity = lookDirection * bulletSpeed;
					// obj.GetComponent<Rigidbody2D>().velocity = -direction * bulletSpeed;
					if(objMass > 1) objMass = (objMass/3);
					obj.GetComponent<Rigidbody2D>().AddForce(-direction * bulletSpeed * objMass, ForceMode2D.Impulse);
					StartCoroutine(delay());


				}

			}


		}

		if (grabbed) // this holds the object in place //
		{

			hit.collider.gameObject.transform.position = handpoint.position;
			hit.collider.gameObject.transform.rotation = pickupPoint.Update();
			hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = PersonRB.velocity;

		}

		// Oxygen thruster
		if (!grabbed && (Input.GetKeyDown(KeyCode.Q)))
		{
			lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
			firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
			PersonRB.velocity = direction * thrusterSpeed; // bulletSpeed;


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

		Gizmos.DrawLine(grabpoint.position, redTargeter.position);
		// print("the grabpoint and the world " + grabpoint.position + " "+  worldMousePosition);


		// Vector3 directionOfTarget = redTargeter.position.normalized;
		// Gets a vector that points from the player's position to the target's.
		// https://docs.unity3d.com/2018.3/Documentation/Manual/DirectionDistanceFromOneObjectToAnother.html
		var heading = grabpoint.position - redTargeter.position;
		var distance = heading.magnitude;
		var direction2 = heading / distance; // This is now the normalized direction.


		Gizmos.color = Color.red;
		Gizmos.DrawRay(grabpoint.position, -direction2);


	}
	
	IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
		feet.SetActive(true);
    }
}