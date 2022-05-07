using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen_thruster3_KaiFix : MonoBehaviour
{
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

// DO NOT DELETE -- THIS IS MAKING OX PARTICLES WORK!
GameObject particlesTemp;

	void start()
	{

	}


    // Update is called once per frame
    public  void Update()
    {
		
			startOxygen(); 

			
    }
	
	public void startOxygen(){
		
		
		 if (Input.GetMouseButtonDown(1) || (Input.GetKeyDown(KeyCode.E)))
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
		 if(Input.GetMouseButtonUp(1) || (Input.GetKeyDown(KeyCode.E)))
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
