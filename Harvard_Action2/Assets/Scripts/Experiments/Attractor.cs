using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	const float G = 10.4f;
	public static List<Attractor> Attractors;
	public Rigidbody2D rb;
	public bool isGrounded;
	
	void FixedUpdate ()
	{
		foreach (Attractor attractor in Attractors)
		{
			if (attractor != this)
				Attract(attractor);
		}
	}

	void OnEnable ()
	{
		if (Attractors == null)
			Attractors = new List<Attractor>();

		Attractors.Add(this);
	}

	void OnDisable ()
	{
		Attractors.Remove(this);
	}

	void Attract (Attractor objToAttract)
	{
		Rigidbody2D rbToAttract = objToAttract.rb;

		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;
		print("distance " + distance);

		if (distance == 0f)
			return;
		
		if (distance <= 10f) 
		{
			if ( isGrounded ) {
				distance = 1;
			}
				float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
				Vector3 force = direction.normalized * forceMagnitude;

			// rbToAttract.AddForce(force);
			
			// force 2
			// get the size of this object
			var renderer = gameObject.GetComponent<Renderer>();
			float width = renderer.bounds.size.x;
			print("my width is " + width);
			float forceMagnitudeF2dg = -G * (rb.mass * rbToAttract.mass) / (width/2);
			Vector3 forceF2dg = direction.normalized * forceMagnitude;
			print("my force vector is " + forceF2dg);
			rbToAttract.AddForce(forceF2dg);
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
			if (collision.gameObject.tag == "platform")
			{
				print("collision with platform");
				isGrounded = true;

			}
	}
	
	void OnCollisionExit2D(Collision2D collision)
    {
			if( collision.gameObject.tag == "platform") 
			{
				isGrounded = false;
			}
	}
	

}