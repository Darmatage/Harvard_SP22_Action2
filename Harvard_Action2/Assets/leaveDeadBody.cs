using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaveDeadBody : MonoBehaviour
{
	public GameObject deadBody;
	// publix OxBarScript oxBarScript;
	public GameHandler gameHandler;
	public float currentHealth = 100f;
	GameObject deadBodNow;
	
	public bool iAmDying = false;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = gameHandler.CurrentHealthNotStatic;
		if(currentHealth <= 0 && iAmDying == false)
		{
			iAmDying = true;
			StartCoroutine(delayBod());
			
		}
		if(currentHealth == 100)
		{
			iAmDying = false;
		}
    }
	
	IEnumerator delayBod()
	{
	  

	  yield return new WaitForSeconds(2.5f);
	  deadBodNow = Instantiate(deadBody, transform.position, Quaternion.identity);
	  deadBodNow.SetActive(true);
	 
	}
}
