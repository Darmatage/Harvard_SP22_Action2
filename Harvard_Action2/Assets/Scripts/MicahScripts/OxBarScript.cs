using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxBarScript : MonoBehaviour
{

    public float startOx = 100;
    public float Ox;
    //public GameObject deathEffect;
    public Image OxBar;
    public Color OxyColor = new Color(0.3f, 0.8f, 0.3f);
    public Color unOxyColor = new Color(0.8f, 0.3f, 0.3f);

    //temporary time variables:
    public float timeToDamage = 5f;
    public float timeToRefill = .0001f;
    private float theTimer;
    public float damageAmt = 10f;
    public float refillAmt = 10f;
    public bool isFiltering = false;
	
	// screenflash
	public GameObject ScreenFlash;
	public float screenFlashDelay = 0.3f;
	public GameObject warning;
	
	// connect to oxygenThruster:
	// public Oxygen_thruster OxyThrust;

    private void Start()
    {
        Ox = startOx;
        theTimer = timeToDamage;
		ScreenFlash.SetActive(false);
		warning.SetActive(false);

       
    }

    public void adjustOx(int amount)
    {
        Ox += amount;
    }

    void FixedUpdate()
    {
	
        theTimer -= Time.deltaTime;
        

        if (isFiltering == false)
        {
            if (theTimer <= 0)
            {
                TakeDamage(damageAmt);
                theTimer = timeToDamage;
            }
        }
        else if (isFiltering == true)
        {
            if (theTimer <= 0)
            {
                RefillOx(refillAmt);
                theTimer = timeToRefill;
            }
        }
		
    }

    
   
    public void SetColor(Color newColor)
    {
        OxBar.GetComponent<Image>().color = newColor;
    }

    public void TakeDamage(float amount)
    {
        Ox -= amount;
        OxBar.fillAmount = Ox / startOx;
      
    }
    public void RefillOx(float amount)
    {
        if (Ox <= 100)
        {
            Ox += amount;
            OxBar.fillAmount = Ox / startOx;
        }
        else if (Ox >= 100)
        {
            isFiltering = false;
        }
    }
 

    
    public void Update()
    {
		if(Ox <= 20f)
		{
		 warning.SetActive(true);
		 StartCoroutine(ShowAndHide(ScreenFlash, screenFlashDelay));
		}
		else
		{
			warning.SetActive(false);
			ScreenFlash.SetActive(false);
		}
    }

    public void Die()
    {
		Ox = 0;
        Debug.Log("You Died");
      
    }
	
	// connected to GameHandler to update level on total oxygen
	public float getOxLevel()
	{
		return Ox;
	}
	
	// reset
	public void setOxLevel100()
	{
		print("OXYGEN levels replenished!");
		Ox = startOx;
		// return Ox;
	}
	IEnumerator ShowAndHide(GameObject ScreenFlash, float delay)
	   {
		   ScreenFlash.SetActive(true);
		   yield return new WaitForSeconds(delay);
		   ScreenFlash.SetActive(false);
	   }

    
	

}


