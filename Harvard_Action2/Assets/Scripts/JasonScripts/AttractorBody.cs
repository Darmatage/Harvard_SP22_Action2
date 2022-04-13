using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorBody : MonoBehaviour{
	
	public GameObject thePlayer; 
	public Player_MoveMagnetic playerMag;
	
	public bool pullThePlayer = false;
	
	public float magnetism = -10f;
	private float maxMagnetism;
	private float minMagnetism = 1f;
	public float distToPlayer;
	public float maxDistance;
	
	
    void Start(){
		thePlayer = GameObject.FindWithTag("Player");
		playerMag = thePlayer.GetComponent<Player_MoveMagnetic>();
		maxDistance = playerMag.magnetRange;
		
		maxMagnetism = magnetism;
    }

	//UPDATE FUNCTION CURRENTY NOT IN USE (all handled in player)
    void disabledUpdate(){
		distToPlayer = Vector2.Distance(transform.position, thePlayer.transform.position);
		
        if (pullThePlayer){	
			Vector3 magneticUp = (thePlayer.transform.position - transform.position).normalized;
			thePlayer.transform.GetComponent<Rigidbody2D>().AddForce(magneticUp * magnetism);
			
			//thePlayer.transform.LookAt(gameObject.transform);
			//thePlayer.transform.LookAt(new Vector3 (0, 0, gameObject.transform.position.z));
			
			Vector3 difference = transform.position - thePlayer.transform.position;
			float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			thePlayer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 110f);
			
			//playerMag.currentWalkable = gameObject.transform;
		}
		
		if ((playerMag.isJumping)||((distToPlayer > maxDistance)&&(distToPlayer < maxDistance + 0.5f))){
			pullThePlayer = false;
			//playerMag.currentWalkable = null;
		}
		
		if (playerMag.inContact){
			//magnetism= minMagnetism;
		}else {magnetism = maxMagnetism;}
    }

	// public void pullPlayer(){
		// pullThePlayer = true;
	// }
	
	
	//STILL IN USE
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, thePlayer.GetComponent<Player_MoveMagnetic>().magnetRange);
      }

}



//attraction system scripts reference this video: https://youtu.be/gHeQ8Hr92P4