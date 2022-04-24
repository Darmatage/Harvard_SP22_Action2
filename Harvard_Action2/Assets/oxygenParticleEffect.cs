using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygenParticleEffect : MonoBehaviour
{
	
// public float speed = 5f;
// public GameObject oxygenParticleHolder;
public ParticleSystem po;

	void Start()
	{
		// po = oxygenParticleHolder.GetComponent<ParticleSystem>();
		po.Stop();
		// oxygenParticleHolder.SetActive(false);
		
	}


    // Update is called once per frame
    public  void Update()
    {
		
			startOxygen(); 
			
    }
	
	public void startOxygen(){
		
		
		 if (Input.GetKeyDown(KeyCode.E))
		 {
			
			// oxygenParticleHolder.SetActive(true);
			if(po.isPlaying)
			{
				po.Play();
			}
		
		 }
		 if (Input.GetKeyUp(KeyCode.E))
		 {
			// oxygenParticleHolder.SetActive(false);
			po.Stop();
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
