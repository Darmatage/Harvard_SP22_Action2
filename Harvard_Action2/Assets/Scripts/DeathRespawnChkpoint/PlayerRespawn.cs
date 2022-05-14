using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
       public Animator animator;
	   
	   // public Animator animatorCheckPoint;
	   
	   
	   private GameHandler gameHandler;
	   public GameObject oxygenParticles;
	   // public Transform shoulder;
	   public PlayerBottomRespawn pSpawnScript;
       public Transform pSpawn;       // current player spawn point
	   // public Transform suithole;
	   public GameObject suitholeparent;
	   private Transform suithole;
	   private bool respawning = false;
	   public float playerHealth = 100f;
	   
	   
	   
	   
	   GameObject particlesTemp;
	   
	   // timer
	   private float timer = 0.0f;
	   public float waitTime = 2.0f;
	   
	   
	   // I'm pulling in oxbar to make it disappear when player is dying
	   public GameObject oxBar;
	   public GameObject OxActivateWarning;
	   public GameObject DeathOverlay;

       void Start() {
              gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
			  suithole = suitholeparent.transform;
			  suitholeparent.SetActive(false);
       }

       void Update() {
		   playerHealth = gameHandler.CurrentHealthNotStatic;
				damage();
              if (respawning == false && pSpawn != null){
                     if (playerHealth <= 0f){ //&&  GameHandler.Deaths < GameHandler.MaxDeaths){
                            respawning = true; // cannot respawn or die again
							
							OxActivateWarning.SetActive(true);
							oxBar.SetActive(false);
							DeathOverlay.SetActive(true);
							Text OxActivateWarningText = OxActivateWarning.GetComponentInChildren<Text>();
							OxActivateWarningText.text = "DEATH IMMINENT";
							
									// playerHealth = 100;
							playerHealth = 100;
							var currDeaths = GameHandler.Deaths;
							gameHandler.replenishHealth();
							StartCoroutine(DelayDeath());		
									// Vector3 pSpn2 = new Vector3(pSpawn.position.x, pSpawn.position.y, transform.position.z);
					
									// gameObject.transform.position = pSpn2;
									// gameObject.transform.rotation = Quaternion.identity;
									// gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // turn off RB
									// pSpawnScript.respawn(); // call a respawn
							
									// gameHandler.replenishHealth();
							// GameHandler.Deaths ++;
							
							
							 // timeDelay();
							
							// GameHandler.Deaths ++;
							GameHandler.Deaths = currDeaths + 1;
							
							respawning = false;
							
                     }
              }
       }
	   
	   public void damage(){
		   if (GameHandler.CurrentHealth <= 20f){
			   // suitholeparent.SetActive(true);
			   // animator.SetBool("Death", true);
			 // particlesTemp = Instantiate(oxygenParticles, suithole.position, Quaternion.identity);
			 // particlesTemp.transform.SetParent(suithole);
			 // particlesTemp.transform.LookAt(particlesTemp.transform.position - ( shoulder.position - particlesTemp.transform.position));
		   }
		   else{
			    // suitholeparent.SetActive(false);
				// animator.SetBool("CP", false);
			
		   }
		   
	   }
		// get checkpoint name >> send this to GameHandler >> GameHandler records current pos >> sends to canvas SceneScript >> displays appropriate message based on input from gameHandler
       public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "Checkpoint"){
							
							// animatorCheckPoint.SetTrigger("CP");
							AudioHandler.PlaySound ("checkpoint");
							
                            pSpawn = other.gameObject.transform;
							// Debug.Log("I touched a checkpoint " + pSpawn);
							// print("checkpoint has been hit! ");
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
			  
			  if (other.gameObject.tag == "Thought"){
				  var currentThoughtpoint = other.gameObject.name;
				  gameHandler.currCheckpointName = currentThoughtpoint;
				  gameHandler.newCheckPointTouched = true;
				  // print("the current checkpoint name is " + other.gameObject.name);
				  
			  }
			  
			   if (other.gameObject.tag == "LevelGateway"){
							
                            pSpawn = other.gameObject.transform;
							Debug.Log("I touched a LevelGateway " + pSpawn);
							print("LevelGateway has been hit! ");
                            GameObject thisLevelGateway = other.gameObject;
							// Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
							// checkRend.material.color = Color.white;
                            StopCoroutine(changeColorGatway(thisLevelGateway));
                            StartCoroutine(changeColorGatway(thisLevelGateway));
							
							// update gameHandler with current checkpoint name
							var currentLevelGateway = other.gameObject.name;
							gameHandler.currentLevel = currentLevelGateway; // update name of level in gamehandler
							gameHandler.updateUIMissionUI(); // will trigger player UI
              }
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "Checkpoint" || other.gameObject.tag == "Thought"){
							Debug.Log("I moved passed a checkpoint");
							gameHandler.newCheckPointTouched = false;
              }
       }

       IEnumerator changeColor(GameObject thisCheckpoint){
			
			animator.SetBool("CP", true);	
				
			  //Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
              //checkRend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			  //SpriteRenderer checkRend2 = thisCheckpoint.GetComponentInChildren<SpriteRenderer>();
			  //checkRend2.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              yield return new WaitForSeconds(0.5f);
              //checkRend.material.color = Color.white;
			  //checkRend2.color = Color.white;
			  
			  // remove if checkpoint img changed
			 
			  // checkRend.color = Color.green;
       }
	   
	   IEnumerator DelayDeath()  //  <-  its a standalone method
		{
			print("dying in 3 seconds");
			
			animator.SetTrigger("Death");
			AudioHandler.PlaySound ("no_air");
			
			
			yield return new WaitForSeconds(4.5f);
			Vector3 pSpn2 = new Vector3(pSpawn.position.x, pSpawn.position.y, transform.position.z);
							
							
			  gameObject.transform.position = pSpn2;
							gameObject.transform.rotation = Quaternion.identity;
							Rigidbody2D rb = GetComponent<Rigidbody2D>();
							rb.velocity = Vector2.zero; 
							rb.angularVelocity  = 0f;
							
							pSpawnScript.respawn(); // call a respawn
					
							// gameHandler.replenishHealth();
							// GameHandler.Deaths ++;
							
							animator.SetBool("Death", false);
							DeathOverlay.SetActive(false);
							 animator.Rebind();
							animator.Update(0f);
							OxActivateWarning.SetActive(false);
							oxBar.SetActive(true);
			// print("ByeBye");
			respawning = false;
		}
			
		public void timeDelay()
		{
			// respawning = true;
			animator.SetTrigger("Death");
			
			 timer += Time.deltaTime;
			 // animator.SetBool("Death", false);	
			 GameHandler.CurrentHealth = 100f;
			
        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
        if (timer > waitTime)
			{
				 // animator.SetBool("Death", false);	
			Vector3 pSpn2 = new Vector3(pSpawn.position.x, pSpawn.position.y, transform.position.z);
							
						
			  gameObject.transform.position = pSpn2;
							gameObject.transform.rotation = Quaternion.identity;
							
							pSpawnScript.respawn(); // call a respawn
					
							gameHandler.replenishHealth();
							// print("animate the death!" + gameHandler.CurrentHealthNotStatic);
			animator.SetBool("Death", false);
			animator.SetBool("idle", true);
				// Remove the recorded 2 seconds.
				timer = timer - waitTime;
				
			}
			respawning = false;
		}
		
		
		// temp!
		 IEnumerator changeColorGatway(GameObject thisCheckpoint){
			
				// print("CHANGING COLOR! ");
				
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