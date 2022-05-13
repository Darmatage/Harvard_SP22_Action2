using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// this will make the player enter into the regular game with full OX loss
public class OxygenActivator : MonoBehaviour
{
	GameObject OxBG;
	public GameObject OxActivateWarning;
	public bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        OxBG = GameObject.Find("OxBG");
		// OxActivateWarning = GameObject.Find("OxActivateWarning");
		OxBG.SetActive(false);
		OxActivateWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter2D(Collider2D col)
    { 
	  // if(!isActivated)
	  // {
		// if (col.gameObject.tag == "Player")
		// {
              // Debug.Log("I have been collided with");
			  // OxBG.SetActive(true);
			  // OxActivateWarning.SetActive(true);
			  // Text OxActivateWarningText = OxActivateWarning.GetComponent<Text>(); //.text = "WARNING: Oxygen Depleting";
			  
              // StartCoroutine(TypeText(OxActivateWarningText, "WARNING: Oxygen Depleting"));
			  // isActivated = true;
		// }
	  // }
        
    }
	
	void OnTriggerExit2D(Collider2D col)
    {
	  if(!isActivated)
	  {
		if (col.gameObject.tag == "Player")
		{
              Debug.Log("I have been collided with");
			  OxBG.SetActive(true);
			  OxActivateWarning.SetActive(true);
			  Text OxActivateWarningText = OxActivateWarning.GetComponentInChildren<Text>(); //.text = "WARNING: Oxygen Depleting";
			  
              StartCoroutine(TypeText(OxActivateWarningText, "WARNING: Oxygen Depleting "));
			  isActivated = true;
			  AudioHandler.PlaySound ("oxActivated");
		}
	  }
        
    }
	
	IEnumerator delay()
	{

	  yield return new WaitForSeconds(10f);
	  OxActivateWarning.SetActive(false);

	}
	
	IEnumerator TypeText(Text target, string fullText){
		Debug.Log("I have been TypeText Effect");
			float delay = 0.04f;
			for (int i = 0; i < fullText.Length; i++){
					string currentText = fullText.Substring(0,i);
					target.text = currentText;
					yield return new WaitForSeconds(delay);
			}
			yield return new WaitForSeconds(7f);
			target.text = "";
			yield return new WaitForSeconds(1f);
			target.text = fullText;
			AudioHandler.PlaySound ("oxActivated");
			yield return new WaitForSeconds(3f);
			target.text = "";
			yield return new WaitForSeconds(1f);
			target.text = fullText;
			AudioHandler.PlaySound ("oxActivated");
			yield return new WaitForSeconds(3f);
			OxActivateWarning.SetActive(false);
			
	}
}
