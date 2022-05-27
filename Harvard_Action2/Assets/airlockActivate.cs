using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airlockActivate : MonoBehaviour
{
	public Animator animator;
	public GameObject firstDoor;
	public GameObject secondDoor;
    // Start is called before the first frame update
    void Start()
    {
        firstDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
	   	   AudioHandler.PlaySound("door");
       if(col.tag == "player")
	   {
	
		   // AudioHandler.PlaySound("airlock");
		   // firstDoor.SetActive(true);
		   animator.SetBool("Airlock", true);
		   secondDoor.SetActive(false);
	   }
    }
	void OnTriggerExit2D(Collider2D col)
    {
	  

	  
		StartCoroutine(delay());
       if(col.tag == "player")
	   {

		   	
		   // secondDoor.SetActive(false);
	   }
	   
	    firstDoor.SetActive(true);
    }
	
	IEnumerator delay()  
		{
			print("i'm in delay");
			yield return new WaitForSeconds(3f);
			AudioHandler.PlaySound("door");
		}
}
