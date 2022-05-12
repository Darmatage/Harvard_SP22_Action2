using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_stats_viewer : MonoBehaviour
{
	public OxBarScript Drag_Canvas_Here_OxygenTracker;
	public Text OxygenPercentTextBox;
	private GameHandler gameHandler;
	public Text RespawnsLeft;
	public Text GoalsMet;
	
    // Start is called before the first frame update
    void Start()
    {
           gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
		// UpdateOxygenPercentTextBox();
		UpdateDeathTracker();
		UpdateAchievements();
        
    }
	       public void UpdateOxygenPercentTextBox(){
              // Text OxygenPercentTextBoxText = OxygenPercentTextBox.GetComponent<Text>();
			  float OxPercent = GameHandler.CurrentHealth;
			  
			  
			  // took out below due to error in build
					  float OxygenPercent = OxPercent;
					  OxygenPercentTextBox.text = "Ox: " + OxygenPercent + " %";
					  print("the text of OXBar " + OxygenPercentTextBox.text );
       }
	   
	   public void UpdateDeathTracker()
	   {
		   
		   // TAKEN OUT DUE TO BUILD
		  var respawnsLeft = GameHandler.MaxDeaths - GameHandler.Deaths;
		  RespawnsLeft.text =  " Respawns " + respawnsLeft/2;
		
	   }
	   
	   public void UpdateAchievements(){
		   
		   //TAKEN OUT DUE TO BUILD
		   GoalsMet.text = GameHandler.pointsScoredForUI + "/3 Goals Met";
	   }
}
