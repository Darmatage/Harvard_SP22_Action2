using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxBarScript : MonoBehaviour
{

    public float startOx = 100;
    private float Ox;
    //public GameObject deathEffect;
    public Image OxBar;
    public Color OxyColor = new Color(0.3f, 0.8f, 0.3f);
    public Color unOxyColor = new Color(0.8f, 0.3f, 0.3f);

    //temporary time variables:
    public float timeToDamage = 5f;
    private float theTimer;
    public float damageAmt = 10f;
	
	
	// connect to oxygenThruster:
	public Oxygen_thruster OxyThrust;

    private void Start()
    {
        Ox = startOx;
        theTimer = timeToDamage;
    }

    void FixedUpdate()
    {
		// if (Input.GetKeyDown(KeyCode.E))
		// {
			 // timeToDamage = .05f;
		// }
		// if (Input.GetKeyUp(KeyCode.E))
		// {
			// timeToDamage = 5f;
		// }
		// theTimer = timeToDamage;
        theTimer -= Time.deltaTime;
		
        if (theTimer <= 0)
        {
            TakeDamage(damageAmt);
            theTimer = timeToDamage;
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

    public void FilterOverTime (int filterAmount, int duration)
    {
        StartCoroutine(FilterOverTimeCoroutine(filterAmount, duration));
    }

    public void SpendOverTime(int spendAmount, int duration)
    {
        StartCoroutine(SpendOverTimeCoroutine(spendAmount, duration));
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

    IEnumerator FilterOverTimeCoroutine(float filterAmount, float duration)
    {
        float amountFiltered = 0;
        float filterPerLoop = filterAmount / duration;
        while (amountFiltered < filterAmount)
        {
            Ox += filterPerLoop;
            amountFiltered += filterPerLoop;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpendOverTimeCoroutine(float spendAmount, float duration)
    {
        float amountSpent = 0;
        float spendPerLoop = spendAmount / duration;
        while (amountSpent < spendAmount)
        {
            Ox += spendPerLoop;
            Debug.Log(Ox.ToString());
            amountSpent += spendPerLoop;
            yield return new WaitForSeconds(1f);
        }
    }


}


