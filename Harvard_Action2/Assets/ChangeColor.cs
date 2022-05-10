using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
	public MissionHandler MS;
	public GameObject innerCircle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	
	 public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "Player"){
							
				    print("EGG ENTERED!!");
                    StartCoroutine(EggChangeColor(innerCircle));
					MS.makeBoxesVanish = false;
					MS.makeBoxesFade = false;
			  }
							
							
              }
	
	 IEnumerator EggChangeColor(GameObject thisCheckpoint){
			
			  print("CHANGING COLOR! ");
				
			  Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
              checkRend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			  SpriteRenderer checkRend2 = thisCheckpoint.GetComponentInChildren<SpriteRenderer>();
			  checkRend2.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              yield return new WaitForSeconds(0.5f);
              checkRend.material.color = Color.red;
			  checkRend2.color = Color.red;
			  
			  // remove if checkpoint img changed
			 
			  // checkRend.color = Color.green;
       }
}
