using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter2D(Collider2D col)
    {
		print("I am hit");
	   AudioHandler.PlaySound("door_warn");
       if(col.tag == "player")
	   {

	   }
    }
	void OnTriggerExit2D(Collider2D col)
    {
		print("I have been exited!");	
       if(col.tag == "player")
	   {
		   

	   }
    }
}
