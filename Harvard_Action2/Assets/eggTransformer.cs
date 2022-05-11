using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class eggTransformer : MonoBehaviour
{
	public Camera mainCamera;
	public GameObject AlienShipCamera;
	public GameObject PlayerEgg; 
	public AudioHandlerObj AHO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "Player"){
							
                         print("player has entered the egg!");
						 other.gameObject.SetActive(false);
						 PlayerEgg.SetActive(true);

						 AlienShipCamera.SetActive(true);
						 mainCamera.enabled = false;
						 
						 // TURN OFF ALL SOUNDS FROM PREV PLAYER
						 AHO.PlaySoundLoop("ox", false); // this would be only sound still on
              }
       }
	   
	   // one potential issue, everytime we pass a checkpoint will reset thinking....
	   public void OnTriggerExit2D(Collider2D other) {
              // if (other.gameObject.tag == "Checkpoint"){
							// Debug.Log("I moved passed a checkpoint");
							// gameHandler.newCheckPointTouched = false;
              // }
       }

}
