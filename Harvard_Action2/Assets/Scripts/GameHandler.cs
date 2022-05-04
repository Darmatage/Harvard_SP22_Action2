using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour{

        public static int playerStat;
        public static bool GameisPaused = false;
        public GameObject pauseMenuUI;
        // public AudioMixer mixer;  
        public static float volumeLevel = 0.5f;
        private Slider sliderVolumeCtrl;
		
		
		
		
		// Oxygen
		private static float OxygenLevel = 100f;
		public OxBarScript Drag_Canvas_Here_OxygenTracker;
		// public Text OxygenPercentTextBox;
		// public Text RespawnsLeft;
		// public Text GoalsMet;

	    //Deaths
		public static int Deaths = 0;
		public static int MaxDeaths = 100;
		
		
		
		

    public bool isDefending = false;
	
	private GameObject player;
	//Used by the death system, needs to be merged with Oxygen system
	public static int playerHealth = 100;
    public int StartPlayerHealth = 100;

    // public static int gotTokens = 0;
    // public GameObject tokensText;
	public static int MaxHealth = 100;
    public static float CurrentHealth = 100f;
	public float CurrentHealthNotStatic = 100f;
	

	
	// update MISSION status
	public MissionHandler missionHandler;
	public string currentLevel = ""; // name of current level

	public int currentLevelInteger = 0; // integer associated with current level
	public int pointsScored = 0; // temporary way to track total positive behavior
	public static int pointsScoredForUI;

    public string sceneName;
	public string currCheckpointName = "CP_tutorial_1";
	public bool newCheckPointTouched = false;
	public bool respawning = false;
	
        void Awake (){
			
			// this is the volume level!
                // SetLevel (volumeLevel);
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                // if (sliderTemp != null){
                        // sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        // sliderVolumeCtrl.value = volumeLevel;
                // }
        }

        void Start (){
            pauseMenuUI.SetActive(false);
			// ShowMission.SetActive(false);
            GameisPaused = false;
				
            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            // if (sceneName=="MainMenu"){ 
                  // playerHealth = StartPlayerHealth;
            // }
            // updateStatsDisplay();
        }
		
		public void updateUIMissionThoughtUI() 
		{
			missionHandler.ThoughtUpdater();
		}
		public void updateUIMissionUI() 
		{
			missionHandler.MissionUpdater();
		}
		
		
				// update section called repeatedly
        public void Update (){
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused){
                                Resume();
                        }
                        else{
                                Pause();
                        }
                }
				
				// two health meters for now
				CurrentHealth = OxygenLevel;
				CurrentHealthNotStatic = OxygenLevel;
				
				
				pointsScoredForUI = pointsScored;
				// update oxygen level constantly
				OxygenLevel = Drag_Canvas_Here_OxygenTracker.getOxLevel();
				if (Deaths >=  MaxDeaths) 
				{
					playerDies();
				}
				print("the Oxygen level from GameHandler is " + OxygenLevel);
				// update oxygen UI textbox to show percent
				UpdateOxygenPercentTextBox();
				UpdateDeathTracker();
				UpdateAchievements();
				
				// show mission when user presses M
				// if (Input.GetKeyDown(KeyCode.M))
				 // {
					 // ShowMission.SetActive(true);
					 
					 
				 // }
				 // if (Input.GetKeyUp(KeyCode.M))
				 // {
					 // ShowMission.SetActive(false);
					 
				 // }
        }
		
//Start of death system
		// public void playerGetHit(int damage){
           // if (isDefending == false){
                  // playerHealth -= damage;
                  // if (playerHealth >=0){
                        // updateStatsDisplay();
                  // }
                  // player.GetComponent<PlayerHurt>().playerHit();
            // }

           // if (playerHealth >= StartPlayerHealth){
                  // playerHealth = StartPlayerHealth;
            // }

           // if (playerHealth <= 0){
                  // playerHealth = 0;
                  // playerDies();
            // }
      // }


	public void replenishHealth(){
		print("gameHandler has replenishHealth");
		Drag_Canvas_Here_OxygenTracker.setOxLevel100(); // reset OX levels 
		CurrentHealthNotStatic = 100f;
		Deaths++;
	}
	

      public void playerDies(){
            // player.GetComponent<PlayerHurt>().playerDead();
			StartCoroutine(DeathPause());
			// SceneManager.LoadScene("Death");
            
      }

      IEnumerator DeathPause(){
            // player.GetComponent<PlayerMove>().isAlive = false;
            // player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("Death");
      }
	  
	  // public void checkpointUpdatesLevel(checkpoint){
		  // currentLevel = checkpoint;
	  // }


       public void UpdateOxygenPercentTextBox(){
              // Text OxygenPercentTextBoxText = OxygenPercentTextBox.GetComponent<Text>();
			  // float OxPercent = OxBarScript.getOxLevel();
			  
			  
			  // took out below due to error in build
					  // float OxygenPercent = OxygenLevel/100f;
					  // OxygenPercentTextBox.text = "Ox: " + OxygenPercent + " %";
					  // print("the text of OXBar " + OxygenPercentTextBox.text );
       }
	   
	   public void UpdateDeathTracker()
	   {
		   
		   // TAKEN OUT DUE TO BUILD
		  // var respawnsLeft = MaxDeaths - Deaths;
		  // RespawnsLeft.text =  respawnsLeft + " Respawns";
		
	   }
	   
	   public void UpdateAchievements(){
		   
		   //TAKEN OUT DUE TO BUILD
		   // GoalsMet.text = pointsScored + " Goals met";
	   }




        void Pause(){
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameisPaused = true;
        }

        public void Resume(){
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameisPaused = false;
        }

        public void SetLevel (float sliderValue){
                // mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                volumeLevel = sliderValue;
        }



        public int CheckPlayerStat(){
                return playerStat;
        }

        public void StartGame(){
                SceneManager.LoadScene("World2");
        }

        public void OpenCredits(){
                SceneManager.LoadScene("Credits");
        }
		
		public void OpenPlayerControls(){
                SceneManager.LoadScene("PlayerControls");
        }

        public void RestartGame(){
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
        }
		
		// gets oxygen from OxBarScript and then tracks it
		public void setOxygen(){
                OxygenLevel = Drag_Canvas_Here_OxygenTracker.getOxLevel();
        }
		
		public void QuitGame(){
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
        }
}

		
		