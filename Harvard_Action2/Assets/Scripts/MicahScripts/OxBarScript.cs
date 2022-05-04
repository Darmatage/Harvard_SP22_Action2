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
    public float timeToRefill = 5f;
    private float theTimer;
    public float damageAmt = 10f;
    public float refillAmt = 10f;
    public bool isFiltering = false;
	
	// connect to oxygenThruster:
	public Oxygen_thruster OxyThrust;

    private void Start()
    {
        Ox = startOx;
        theTimer = timeToDamage;

       
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
        else if (Ox > 100)
        {
            isFiltering = false;
        }
    }
 

    
    public void Update()
    {
        if (GetComponent<Movement_arrows_4>().isFiltering == false)
        {
            isFiltering = false;
        }
        else if (GetComponent<Movement_arrows_4>().isFiltering == true)
        {
            isFiltering = true;
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

    


}


