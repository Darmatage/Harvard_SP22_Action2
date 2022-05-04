using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateFromPlayer : MonoBehaviour
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
				animator.SetTrigger("CP");	
				// animator.SetBool("CP", true);
				// animator.Play("CP");
			  }
							
							
              }
			  
			  public void OnTriggerExit2D(Collider2D other) {
              if (other.gameObject.tag == "playerCheckpoint"){
							
				// animator.SetBool("CP", false);	
				animator.speed = 0f;
			  }
							
							
              }
}
