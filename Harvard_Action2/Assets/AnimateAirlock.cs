using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAirlock : MonoBehaviour
{
	public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "playerCheckpoint"){
				// print("player has entered ");
				// animator.SetTrigger("Airlock");	
				 animator.SetBool("Airlock", true);
				// animator.Play("Airlock");
			  }
							
							
              }
			  
			  public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "playerCheckpoint"){
							
				// animator.SetBool("Airlock", false);	
				//animator.speed = 0f;
			  }
							
							
              }
}
