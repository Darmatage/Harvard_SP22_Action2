using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaveDeadBody : MonoBehaviour
{
	public GameObject deadBody;
	// publix OxBarScript oxBarScript;
	public GameHandler gameHandler;
	public float currentHealth = 100f;
	public bool oxActivated = false;
	GameObject deadBodNow;
	public OxygenActivator OxAct;
	
	public bool iAmDying = false;
	
	
    // Start is called before the first frame update
    void Start()
    {
        // OxBG = GameObject.Find("OxBG");
    }

    // Update is called once per frame
    void Update()
    {
		// if (OxBG.activeSelf)
		// {
			// oxActivated = true;
		// }
		if(OxAct.isActivated)
		{
			currentHealth = gameHandler.CurrentHealthNotStatic;
			if(currentHealth <= 0 && iAmDying == false)
			{
				currentHealth = 100;
				iAmDying = true;
				// StartCoroutine(delayBod());
			  deadBodNow = Instantiate(deadBody, transform.position, Quaternion.identity);
			  iAmDying = false;
			  deadBodNow.SetActive(true);
				
				
			}
			if(currentHealth == 100)
			{
				iAmDying = false;
			}
		}
    }
	
	IEnumerator delayBod()
	{
	  yield return new WaitForSeconds(2.5f);
	  deadBodNow = Instantiate(deadBody, transform.position, Quaternion.identity);
	  iAmDying = false;
	  yield return new WaitForSeconds(1f);
	  deadBodNow.SetActive(true);
	}
}
