using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class eggTransformer : MonoBehaviour
{
	public Camera mainCamera;
	public AudioListener mainCamAudioListener;
	
	
	public GameObject AlienShipCamera;
	public GameObject PlayerEgg; 
	public AudioHandlerObj AHO;
	public bool deactivateEgg = false;
	public GameObject w1;
	public GameObject w2;
	public GameObject w3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void OnTriggerEnter2D(Collider2D other) {
		if(!deactivateEgg){
              if (other.gameObject.tag == "Player"){
							
                         print("player has entered the egg!");
						 other.gameObject.SetActive(false);
						 PlayerEgg.SetActive(true);

						 AlienShipCamera.SetActive(true);
						 mainCamera.enabled = false;
						 mainCamAudioListener.enabled = false;
						 
						 // TURN OFF ALL SOUNDS FROM PREV PLAYER
						 AHO.PlaySoundLoop("ox", false); // this would be only sound still on
						 StartCoroutine(changeColorWall(w1));
						 // StartCoroutine(changeColorWall(w2));
						 // StartCoroutine(changeColorWall(w3));
              }
		}
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
		       if (other.gameObject.tag == "Player"){
              deactivateEgg = true;
			  // Collider col = gameObject.GetComponent<Collider>();
			  // col.isTrigger = false;
			   }
       }
	   
	   
	    IEnumerator changeColorWall(GameObject wall){
			
				// print("CHANGING COLOR! ");
				
			  Renderer checkRend = wall.GetComponentInChildren<Renderer>();
              checkRend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			  SpriteRenderer checkRend2 = wall.GetComponentInChildren<SpriteRenderer>();
			  checkRend2.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              yield return new WaitForSeconds(0.5f);
              checkRend.material.color = Color.red;
			  checkRend2.color = Color.red;
			  
			  // remove if checkpoint img changed
			 
			  // checkRend.color = Color.green;
       }

}
