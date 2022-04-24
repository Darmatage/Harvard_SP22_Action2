using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// camera needs to be in orthographic
public class Oxygen_thruster2 : MonoBehaviour{
	
public float speed = 5f;
public GameObject oxygenParticles;
public Transform handEnd;
public Transform shoulder;
public float OxygenDepletion = .05f;

public OxBarScript oxBar;
public bool OxygenOn;
//public ParticleSystem particleEffect;
// public Aim2_static aimer
private Quaternion armRotation;
// private float OxygenTime;


GameObject particlesTemp;

	void start()
	{

	}


    // Update is called once per frame
    public  void Update()
    {
		

			// Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
			// float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
			// armRotation = Quaternion.AngleAxis(angle, Vector3.forward); 
			// transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
			// print("transform.rotation = Quaternion " + transform.rotation);
			// armRotation = aimer.Update();
			startOxygen(); 
			
			// GET PARTICLE ROTATION OR DIRECTION AND THEN THRUS IN OPPOSITE WAY OF THIS
			// armRotation = transform.rotation;
			
    }
	
	public void startOxygen(){
		
		
		 if (Input.GetMouseButtonDown(1))
		 {
			 OxygenOn = true;
			 // oxBar.timeToDamage = OxygenDepletion;
			 print("the ox level in THRUSTER is " + oxBar.timeToDamage);
			 // print("oxygen is on particle!");
			 particlesTemp = Instantiate(oxygenParticles, handEnd.position, Quaternion.identity);
			 particlesTemp.transform.SetParent(handEnd);
			 particlesTemp.transform.LookAt(particlesTemp.transform.position - ( shoulder.position - particlesTemp.transform.position));
			 //LookAt( position - ( target - position))
;             //particleEffect.Play();
			 
			// for oxygenBlaster
			 // OxygenTime += 1f * Time.deltaTime;
			 // oxDamage();
	
			 // oxBar.TakeDamage(OxygenTime);
			 // oxDamage();
			 
			
		
		 }
		 if(Input.GetMouseButtonUp(1))
		 {
			 OxygenOn = false;
			 //particleEffect.Stop() ;
			 // float newOxygenTime = OxygenTime + 100f * Time.deltaTime;
			 // OxygenTime = newOxygenTime - OxygenTime;
			 Destroy(particlesTemp);
			 // oxBar.timeToDamage = 5f;
			 
			 // oxBar.TakeDamage(OxygenTime);
			 

			 // return OxygenTime;
			 
		 }
		  
		  // OxygenTime = 0;
		  // return OxygenTime;
	}
	
	// IEnumerator oxDamage()
	// float oxTime = 0f;
// {
    // while (Input.GetKeyDown(KeyCode.E))
    // {
        // oxBar.TakeDamage(40);
        // yield return null;
    // }
// }
	// public Quaternion getArmRotation()
	// {
		// return armRotation;
	// }
}
