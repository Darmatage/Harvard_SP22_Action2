using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// camera needs to be in orthographic
public class Oxygen_thruster : MonoBehaviour
{
	
	
public float speed = 5f;
public ParticleSystem particleEffect;
// public Aim2_static aimer
private Quaternion armRotation;
private float OxygenTime;

	void start()
	{

	}


    // Update is called once per frame
    public  void Update()
    {
		

			Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
			armRotation = Quaternion.AngleAxis(angle, Vector3.forward); 
			// transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
			// print("transform.rotation = Quaternion " + transform.rotation);
			// armRotation = aimer.Update();
			startOxygen(); 
			
			// GET PARTICLE ROTATION OR DIRECTION AND THEN THRUS IN OPPOSITE WAY OF THIS
			// armRotation = transform.rotation;
			
    }
	
	public float startOxygen() 
	{
		 if (Input.GetKeyDown(KeyCode.E))
		 {
			 print("oxygen is on particle!");
             particleEffect.Play();
			 
			 // track timer
			 OxygenTime += Time.deltaTime;
			 print("the oxygen use is " + OxygenTime);
			 // MOVE PARENT IN OPPOSITE OF TOTATION - get player tag and move player opposite
			 
			
		
		 }
		 if (Input.GetKeyUp(KeyCode.E))
		 {
			 particleEffect.Stop();
			 OxygenTime = 0;
			 
		 }
		 
		  return OxygenTime;
	}
	
	// public Quaternion getArmRotation()
	// {
		// return armRotation;
	// }
}
