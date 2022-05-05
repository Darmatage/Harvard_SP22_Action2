using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygenReplenish : MonoBehaviour
{
	public OxBarScript OxBar;
	public float SpeedOfReplishmentSmallIsFaster = .00001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
		public void OnTriggerEnter2D(Collider2D other) 
		{
              if (other.gameObject.tag == "playerCheckpoint"){
				  print("player got replenishment " );
				  Renderer checkRend = GetComponentInChildren<Renderer>();
				  checkRend.material.color = new Color(0, 1, 0, 1);
				// OxBar.isFiltering = true;
				
				OxBar.timeToDamage = SpeedOfReplishmentSmallIsFaster;
				OxBar.damageAmt = -10f;
			  }
							
							
              }
			  
			  public void OnTriggerExit2D(Collider2D other) 
			  {
              if (other.gameObject.tag == "playerCheckpoint"){
							
				// OxBar.isFiltering = false;
				Renderer checkRend = GetComponentInChildren<Renderer>();
				 // checkRend.material.color = new Color(2.4f, 1.9f, 0.9f, 1.5f);
				  checkRend.material.color = new Color(0f, 0, 1, 1);
				  // oxBar.timeToDamage = .0001f;
				OxBar.damageAmt = .3f;
				OxBar.timeToDamage = 5f;
			  }
							
							
              }
			  
			   // IEnumerator changeColor(GameObject thisCheckpoint){
			//currently not working, not sure why... the color changes but the animation doesnt play
			// animator.SetBool("CP", true);	
				
			  // Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
              // checkRend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			  // SpriteRenderer checkRend2 = thisCheckpoint.GetComponentInChildren<SpriteRenderer>();
			  // checkRend2.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              // yield return new WaitForSeconds(0.5f);
              // checkRend.material.color = Color.white;
			  // checkRend2.color = Color.white;
			  
			  // remove if checkpoint img changed
			 
			  // checkRend.color = Color.green;
       // }
	   
}
