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
        public AudioMixer mixer;
        public static float volumeLevel = 0.5f;
        private Slider sliderVolumeCtrl;
		
		
		// Oxygen
		private static float OxygenLevel = 100f;
		public OxBarScript Drag_Canvas_Here_OxygenTracker;
		public Text OxygenPercentTextBox;
		// public GameObject TextOx;
			//Used by the oxygen system
		// public static int MaxOx = 100;
		// public static int playerOx = 100;
		// public int StartPlayerOx = 100;
		// public GameObject TextOx;
		
		// Current goals
		public GameObject ShowMission;
		
		
		
		

    public bool isDefending = false;
	
	private GameObject player;
	//Used by the death system, needs to be merged with Oxygen system
	public static int playerHealth = 100;
    public int StartPlayerHealth = 100;

    public static int gotTokens = 0;
    // public GameObject tokensText;
	public static int MaxHealth = 100;
    public static int CurrentHealth = 100;



    private string sceneName;

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
			ShowMission.SetActive(false);
            GameisPaused = false;
				
            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            // if (sceneName=="MainMenu"){ 
                  // playerHealth = StartPlayerHealth;
            // }
            // updateStatsDisplay();
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
				
				// update oxygen level constantly
				OxygenLevel = Drag_Canvas_Here_OxygenTracker.getOxLevel();
				print("the Oxygen level from GameHandler is " + OxygenLevel);
				// update oxygen UI textbox to show percent
				UpdateOxygenPercentTextBox();
				
				// show mission when user presses M
				if (Input.GetKeyDown(KeyCode.M))
				 {
					 ShowMission.SetActive(true);
					 
					 
				 }
				 if (Input.GetKeyUp(KeyCode.M))
				 {
					 ShowMission.SetActive(false);
					 
				 }
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



      public void playerDies(){
            player.GetComponent<PlayerHurt>().playerDead();
			SceneManager.LoadScene("Death");
            //StartCoroutine(DeathPause());
      }

      //IEnumerator DeathPause(){
            //player.GetComponent<PlayerMove>().isAlive = false;
            //player.GetComponent<PlayerJump>().isAlive = false;
            //yield return new WaitForSeconds(1.0f);
            //SceneManager.LoadScene("EndLose");
      //}
	  


       public void UpdateOxygenPercentTextBox(){
              // Text OxygenPercentTextBoxText = OxygenPercentTextBox.GetComponent<Text>();
			  // float OxPercent = OxBarScript.getOxLevel();
			  float OxygenPercent = OxygenLevel/100f;
              OxygenPercentTextBox.text = "Ox: " + OxygenPercent + " %";
			  print("the text of OXBar " + OxygenPercentTextBox.text );
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
                mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                volumeLevel = sliderValue;
        }



        public int CheckPlayerStat(){
                return playerStat;
        }

        public void StartGame(){
                SceneManager.LoadScene("Scene1");
        }

        public void OpenCredits(){
                SceneManager.LoadScene("Credits");
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

		
		// Below is Daniel's botched attempt at merging the health and oxygen systems
		
		//public void TakeDamage(int damage){
        //      playerOx -= damage;
        //      UpdateOx();
        //      sceneName = SceneManager.GetActiveScene().name;
        //      if (playerOx >= MaxOx){playerOx = MaxOx;}
        //      if ((playerOx <= 0) && (sceneName != "EndLose")){
         //            SceneManager.LoadScene("EndLose");
         //     }
		//}
	   
	   //public void UpdateOx(){
       //       Text healthTextB = healthText.GetComponent<Text>();
       //       healthTextB.text = "Current Health: " + CurrentHealth + "\n Max Health: " + MaxHealth;
       //}
		
		//public void playerDies(){
        //    player.GetComponent<PlayerHurt>().playerDead();
        //    StartCoroutine(DeathPause());
		//}
		
		//IEnumerator DeathPause(){
        //    player.GetComponent<PlayerMove>().isAlive = false;
        //    player.GetComponent<PlayerJump>().isAlive = false;
        //    yield return new WaitForSeconds(1.0f);
        //    SceneManager.LoadScene("EndLose");
		//}
		
		//public void playerGetHit(int damage){
         //         //if (isDefending == false)
		//		  {
		//			playerOx -= damage;
		//			if (playerOx >=0){
        //                //updateStatsDisplay();
        //          }
        //          player.GetComponent<PlayerHurt>().playerHit();
        //    }
		//
        //   if (playerOx >= StartPlayerOx){
        //          playerOx = StartPlayerOx;
        //    }

        //   if (playerOx <= 0){
        //          playerOx = 0;
        //          playerDies();
        //    }
		//}
		

        