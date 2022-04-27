using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
       private GameHandler gameHandler;
	   public GameObject oxygenParticles;
	   // public Transform shoulder;
	   public PlayerBottomRespawn pSpawnScript;
       public Transform pSpawn;       // current player spawn point
	   // public Transform suithole;
	   public GameObject suitholeparent;
	   private Transform suithole;
	   
	   
	   GameObject particlesTemp;

       void Start() {
              gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
			  suithole = suitholeparent.transform;
			  suitholeparent.SetActive(false);
       }

       void Update() {
				damage();
              if (pSpawn != null){
                     if (GameHandler.CurrentHealth <= 0f && GameHandler.Deaths < GameHandler.MaxDeaths){
                            //comment out lines from GameHandler about EndLose screen
                            Debug.Log("I am going back to the last spawn point");
                            Vector3 pSpn2 = new Vector3(pSpawn.position.x, pSpawn.position.y, transform.position.z);
                            gameObject.transform.position = pSpn2;
							gameObject.transform.rotation = Quaternion.identity;
							pSpawnScript.respawn(); // call a respawn
							// GameHandler.CurrentHealth = 1;
							gameHandler.replenishHealth();
							GameHandler.Deaths ++;
                     }
              }
       }
	   
	   public void damage(){
		   if (GameHandler.CurrentHealth <= 20f){
			   print("particles should start!");
			   suitholeparent.SetActive(true);
			 // particlesTemp = Instantiate(oxygenParticles, suithole.position, Quaternion.identity);
			 // particlesTemp.transform.SetParent(suithole);
			 // particlesTemp.transform.LookAt(particlesTemp.transform.position - ( shoulder.position - particlesTemp.transform.position));
		   }
		   else{
			    suitholeparent.SetActive(false);
		   }
		   
	   }
		// get checkpoint name >> send this to GameHandler >> GameHandler records current pos >> sends to canvas SceneScript >> displays appropriate message based on input from gameHandler
       public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "Checkpoint"){
							
                            pSpawn = other.gameObject.transform;
							Debug.Log("I touched a checkpoint " + pSpawn);
                            GameObject thisCheckpoint = other.gameObject;
							// Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
							// checkRend.material.color = Color.white;
                            StopCoroutine(changeColor(thisCheckpoint));
                            StartCoroutine(changeColor(thisCheckpoint));
							
							// update gameHandler with current checkpoint name
							var currentCheckpoint = other.gameObject.name;
							gameHandler.currCheckpointName = currentCheckpoint;
							gameHandler.newCheckPointTouched = true;
							gameHandler.updateUIMissionThoughtUI();
              }
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "Checkpoint"){
							Debug.Log("I moved passed a checkpoint");
							gameHandler.newCheckPointTouched = false;
              }
       }

       IEnumerator changeColor(GameObject thisCheckpoint){
              Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
              checkRend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			  SpriteRenderer checkRend2 = thisCheckpoint.GetComponentInChildren<SpriteRenderer>();
			  checkRend2.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              yield return new WaitForSeconds(0.5f);
              checkRend.material.color = Color.white;
			  checkRend2.color = Color.white;
			  
			  // remove if checkpoint img changed
			 
			  // checkRend.color = Color.green;
       }
}