using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
       private GameHandler gameHandler;
	   public PlayerBottomRespawn pSpawnScript;
       public Transform pSpawn;       // current player spawn point

       void Start() {
              gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
       }

       void Update() {
              if (pSpawn != null){
                     if (GameHandler.CurrentHealth <= 0f && GameHandler.Deaths < GameHandler.MaxDeaths){
                            //comment out lines from GameHandler about EndLose screen
                            Debug.Log("I am going back to the last spawn point");
                            Vector3 pSpn2 = new Vector3(pSpawn.position.x, pSpawn.position.y, transform.position.z);
                            gameObject.transform.position = pSpn2;
							pSpawnScript.respawn(); // call a respawn
							// GameHandler.CurrentHealth = 1;
							gameHandler.replenishHealth();
							GameHandler.Deaths ++;
                     }
              }
       }

       public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "Checkpoint"){
							Debug.Log("I touched a checkpoint");
                            pSpawn = other.gameObject.transform;
                            GameObject thisCheckpoint = other.gameObject;
							// Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
							// checkRend.material.color = Color.white;
                            StopCoroutine(changeColor(thisCheckpoint));
                            StartCoroutine(changeColor(thisCheckpoint));
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