using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxScript : MonoBehaviour
{
    public Image OxBar;
    public GameObject OxBG;

    public float startOx = 100;
    private float Ox;




    // Start is called before the first frame update
    void Start()
    {
        Ox = startOx;


    }
    public void LoseOx(float amount)
    {

        OxBar.fillAmount = Ox / startOx;

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
