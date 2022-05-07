using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;  // maybe delete?

public class MissionHandler : MonoBehaviour
{
	
	public GameObject MissionDisplay;
	public Text missionText;
	// public GameObject missionNextCloseButton;
	public GameObject ThinkingDisplay;
	public Text thinkingText;
	// public GameObject thinkingPrevButton;
	// public GameObject thinkingNextButton;
	
	List<string> thinkingList = new List<string>();
	private GameHandler gameHandler;
	public string currGameStatus;
	public string currLevelOfGame;
	public int currThoughtIndex = 0;
	public int maxIndex=0;
	
	public string currDisplayScene;
	
	
	// use this for tracking if new mission is applied
	string prevMission;
	
	
	// for fading UI thought boxes
	public bool makeBoxesFade = true;
	public float NotificationDuration = 5f;
	private YieldInstruction fadeInstruction = new YieldInstruction();
	
	// for button
	public Button hideBox;
	public Text hideButtonText;
	
	Color origC1;
	Color origC2;
	Color origT1;
	Color origT2;
	
	void Awake()
	{
	
	}
	
    // Start is called before the first frame update
    void Start()
    {
		origC1 = ThinkingDisplay.GetComponent<Image>().color;
		origC2 = MissionDisplay.GetComponent<Image>().color;
		origT1 = thinkingText.color;
		origT2 = missionText.color;
		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		currGameStatus = gameHandler.currCheckpointName;
		currLevelOfGame = gameHandler.currentLevel;
		currDisplayScene = currGameStatus;
		thinkingList.Add("What the hell just happened? I'd better go check the rest of the space station. (Walk = A/D)");
		thinkingText.text = thinkingList[currThoughtIndex];
		maxIndex = thinkingList.Count-1;
		
		// very first mission. Append thinking to list
	
	
	}

    // Update is called once per frame
    void Update()
    {
		
		if(gameHandler.newCheckPointTouched)
		{
			// print("gamehandler new updated!");
			currGameStatus = gameHandler.currCheckpointName;
			ThoughtUpdater();
		}
		prevMission = currLevelOfGame;
		print("the prev mission is " + prevMission);
		currLevelOfGame = gameHandler.currentLevel;
		print("the prev mission is " + currLevelOfGame);
		if (prevMission != currLevelOfGame)
		{
			MissionUpdater();
					// do not change below
			gameHandler.currentLevelInteger ++;
			prevMission = currLevelOfGame;
		}
		// maxIndex = thinkingList.Count-1;
		// print("maxIndex " + maxIndex);
		// this will display the current index on the canvas
		thinkingText.text = thinkingList[currThoughtIndex];
				// print( " the thinkingText.text = thinkingList[currThoughtIndex]);
			
		// im M is pressed show mission and thought box
		if (Input.GetKeyDown(KeyCode.M))
        {
			colorOriginator();
			// if any one of them are actice we can hide them both with M
            if(MissionDisplay.activeSelf || ThinkingDisplay.activeSelf){
				hideMissionBox();
				hideThinkingBox();
			}
			else
			{
				//actice both of them
				// colorOriginator();
				MissionDisplay.SetActive(true);
				ThinkingDisplay.SetActive(true);
				
				
				//change button
				hideBox.GetComponent<Image>().color = Color.red;
				hideButtonText.text = "[x]";
				
				// hide after duration
					// Text t1 = ThinkingDisplay.GetComponent<Text>();
				if(makeBoxesFade)
				{
					Image image1 = ThinkingDisplay.GetComponent<Image>();
					Image image2 = MissionDisplay.GetComponent<Image>();
					StartCoroutine(FadeOut(image1, image2, thinkingText, missionText));
					
				}
			}
        }
    }
	
	public void MissionUpdater()
	{
		if(currLevelOfGame == "tutorial")
		{
			missionText.text = "MISSION: Follow the checkpoints (green alien eyes) to the space station storage room";
		}
		if(currLevelOfGame == "MicahsLevel")
		{
			missionText.text = "MISSION: use the debris to get to the upper level of the storage component of the space station";
		}
		if(currLevelOfGame == "DanielsLevel")
		{
			missionText.text = "MISSION: Avoid the spikes to get to the top of the antenna";
		}
		if(currLevelOfGame == "KaisLevel1")
		{
			missionText.text = "MISSION: Get through the debris field and reach the alien mother ship";
		}
		if(currLevelOfGame == "KaisLevel2")
		{
			missionText.text = "MISSION: Use the egg's magnetism to hop to green machine and throw the alient object into the basket";
		}
		
		
		
		

	}
	
	
	
	// // @Daniel and @Micah Use a conditional on the name of the checkpoint in your level and write in
	// // the thought the player has as they pass the checkpoint. The thought should
	// // offer help about gameplay or tips for them on what they need to do 
	public void ThoughtUpdater()
	{//beginning of Tutorial

		

		if (currGameStatus == "ThoughtJump")
		{

			thinkingList.Add("(Aim with your mouse and press W/Space to JUMP ");
		}

		if (currGameStatus == "ThoughtGrab")
        {

			thinkingList.Add("(Click to GRAB and THROW debris)");
        }

        if (currGameStatus == "ThoughtPropel")
		{

			thinkingList.Add("Right Click to RELEASE OXYGEN. But keep an eye on your Oxygen Meter...");
		}

		if (currGameStatus == "ThoughtSeeCP")
		{
			thinkingList.Add("What in the name of Space Jesus is that thing?");
		}

		if (currGameStatus == "Thought")
		{

			thinkingList.Add("");
		}

		if (currGameStatus == "Thought")
		{

			thinkingList.Add("");
		}


		if (currGameStatus == "CP1")
		{
			thinkingList.Add("Oh god I think it touched me. Why do I feel like it collected a sample?");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		

		if (currGameStatus == "CP_sample_1" || currGameStatus == "Checkpoint (1)")
		{
			thinkingList.Add("");

		}

		if (currGameStatus == "CP_sample_2")
		{

			thinkingList.Add("message 2: press mouse right button and fly! uh oh but don't let your oxygent run out...");
		}

		if (currGameStatus == "CP_sample_3")
		{
			thinkingList.Add("message 3 yes yes yes!!");

			// missionText.text = "testing another mission but the prev mission still applies!";
		}

		if (currGameStatus == "CP_debris_1_1" || currGameStatus == "CP_debris_1_2" || currGameStatus == "CP_debris_1_3")
		{
			thinkingList.Add("I need to catch and throw objects. I need to watch out for red spikes!");

			// missionText.text = "Get to the top of the debris field";
		}
		//
		if (currGameStatus == "CP_debris_18" || currGameStatus == "CP_debris_19" || currGameStatus == "CP_debris_20")
		{
			thinkingList.Add("What is this ahead? Look for a green egg shaped thing. Walk into it...");

			// missionText.text = "Get into the Green Egg!";
		}
		if (currGameStatus == "CP_spaceship_1")
		{
			thinkingList.Add("I am in an egg? I still have my Oxygen Thruster (right click), my grabber (leftclick). Hmmm. I notice I also can stick to the ground. Notice pushing arrow left goes right when I'm upside down on this Alien Orb!");

			// missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if (currGameStatus == "CP_spaceship_2")
		{
			thinkingList.Add("I am in an egg? I still have my Oxygen Thruster (right click), my grabber (leftclick). Hmmm. I notice I also can stick to the ground. Notice pushing arrow left goes right when I'm upside down on this Alien Orb!");

			// missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if (currGameStatus == "CP_spaceship_3")
		{
			thinkingList.Add("If I push the up arrow, I jump and the magnetization of this egg turns off!");

			// missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if (currGameStatus == "CP_spaceship_4")
		{
			thinkingList.Add("I notice I have more energy. This Egg is giving me oxygen! Somehow it is more powerful than my suit!");

			// missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if (currGameStatus == "CP_spaceship_15")
		{
			thinkingList.Add("I'm almost at the end. Keep pushing the up arrow and launching from orb to orb. My magnetism is helping me!");

			// missionText.text = "I need to push the Green Alien Machine into it's puzzle slot. Only then will this egg be able to leave this alien ship and fly me home";
		}
		if (currGameStatus == "CP_spaceship_16")
		{
			thinkingList.Add("I need to push the Green Alien Machine into it's puzzle slot. Only then will this egg be able to leave this alien ship and fly me home");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		if (currGameStatus == "CP_spaceship_17")
		{
			thinkingList.Add("All I need to do is to push into it. Then it will move");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		if (currGameStatus == "tutorial_1")
		{
			thinkingList.Add("Oh god I think it touched me. ");

			// missionText.text = "Push the green machine component into the engine slot";
		}

		//beginning of Tutorial level CP

		if (currGameStatus == "tutorial_2")
		{
			thinkingList.Add("I need to escape, but there's too much in the way! I'll have to cut my suit... While floating, right mouse button to spend oxygen for speed and course corrections.");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		if (currGameStatus == "tutorial_3")
		{
			thinkingList.Add("Those red objects look dangerous. I should avoid them.");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		if (currGameStatus == "tutorial_4")
		{
			thinkingList.Add("An oxygen tank! I should stand in it and replenish my oxygen before I head out.");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		if (currGameStatus == "tutorial_5")
		{
			thinkingList.Add("This blue debris looks safe. I can use it to my advantage... Press left mouse button to pick up and throw debris highlighted in blue.");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		if (currGameStatus == "tutorial_6")
		{
			thinkingList.Add("Everything is in disarray... After I roll this silo into its slot, I should move up towards the lifeboats.");

			// missionText.text = "Push the green machine component into the engine slot";
		}
		//Beginnning of Daniel level CP
		if (currGameStatus == "AntennaCP_1")
		{
			thinkingList.Add("The antenna is far up at the top... I should stick close to the station... or...?");
			
		}
		
		if(currGameStatus == "AntennaCP_1.5")
		{
			thinkingList.Add("Oh no... John, Doe... If only they saw these alien eyes...");
			
		}
		
		if(currGameStatus == "AntennaCP_2")
		{
			thinkingList.Add("Halfway There...");
			
		}
		
		if(currGameStatus == "AntennaCP_3")
		{
			thinkingList.Add("Nearly... There");
			
		}
		
		if(currGameStatus == "AntennaCP_B")
		{
			thinkingList.Add("I knew there was a shortcut! Not all astronauts have pro gamer moves");
			
		}
		
		if(currGameStatus == "AntennaCP_4")
		{
			thinkingList.Add("Phew, that was nerve wracking. I should look for the service elevator behind the antenna after I fix it.");
			
		}
		
		//Beginning of Micah level CP
		
		if(currGameStatus == "MCP1")
		{
			thinkingList.Add("This is the docking port. I gotta be careful not to fly off into space.");
			
		}
		
		if(currGameStatus == "MCP2")
		{
			thinkingList.Add("What a mess. I should head up towards the lifeboats");
			
		}
		
		if(currGameStatus == "MCP3")
		{
			thinkingList.Add("Am I the only survivor? Is anyone still alive?");
			
		}
		if(currGameStatus == "kai_test")
		{
			thinkingList.Add("THIS IS JUST A TEST!!! only survivor? Is anyone still alive?");
			
		}
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		  // do not change below
		 // this updates the index to the most current one
		// reset the displays when a new checkpoint it reached.
		maxIndex = thinkingList.Count-1;
		print("thinkingList.Count " + thinkingList);
		print("thinkingList.Count " + thinkingList.Count);
		thinkingText.text = thinkingList[currThoughtIndex];
		foreach(var p in thinkingList) {
         print(" thinkingList " + p );
      }
		print("thinkingList[currThoughtIndex]; " + thinkingList[currThoughtIndex]);
		currThoughtIndex = maxIndex;
		

			colorOriginator();
		MissionDisplay.SetActive(true);
		ThinkingDisplay.SetActive(true);
		
				
				// button changes
				ThinkingDisplay.SetActive(true);;
				hideBox.GetComponent<Image>().color = Color.blue;
				hideButtonText.text = "[o]";
		
		
		// Text t1 = ThinkingDisplay.GetComponent<Text>();
		if(makeBoxesFade)
		{
			Image image1 = ThinkingDisplay.GetComponent<Image>();
			Image image2 = MissionDisplay.GetComponent<Image>();
			StartCoroutine(FadeOut(image1, image2, thinkingText, missionText));
			
		}
	}
	
	// connect to 'x' button of mission
	public void hideMissionBox()
	{	

		MissionDisplay.SetActive(false);
	}
	
	// connect to 'x' button of thinking box
	public void hideThinkingBox()
	{
		ThinkingDisplay.SetActive(false);
						
				// button will change
		// button changes

				hideBox.GetComponent<Image>().color = Color.blue;
				hideButtonText.text = "[o]";
		
	}
	public void thinkingBoxButton()
	{
		if(ThinkingDisplay.activeSelf)  // turn off box
			{
				colorOriginator();
				// hideThinkingBox();
				ThinkingDisplay.SetActive(false);
				// MissionDisplay.SetActive(true);
				// ThinkingDisplay.SetActive(false);
				hideBox.GetComponent<Image>().color = Color.blue;
				hideButtonText.text = "[o]";
			
				
			}
		else // turn on box
			{
				//actice both of them
				// colorOriginator();
				colorOriginator();
				ThinkingDisplay.SetActive(true);
				
				
					hideBox.GetComponent<Image>().color = Color.red;
				hideButtonText.text = "[x]";
			}
		
	}
	
	// displays the prev thought -- connect this to prevButton of thinking box
	public void prevThought()
	{
		print("prevThought is clicked ! " );
		if(currThoughtIndex >0)
		{
			currThoughtIndex = currThoughtIndex - 1;
		}
	}
	
	// displays the next thought -- connect to the nextButton of thinking box
	public void nextThought()
	{
		print("nextThought is clicked " );
		if(currThoughtIndex < maxIndex)
		{
			currThoughtIndex = currThoughtIndex + 1;
		}
	}

	IEnumerator FadeOut(Image image, Image image2, Text text, Text text2)
{
    float elapsedTime = 0.0f;
	// Color origC1 = image.color;
	// Color origC2 = image2.color;
	// Color origT1 = text.color;
	// Color origT2 = text2.color;
	
    Color c = image.color;
    while (elapsedTime < NotificationDuration)
    {
		print("elapsedTime vs. fadeTime " + elapsedTime + " " + NotificationDuration);
        yield return fadeInstruction;
        elapsedTime += Time.deltaTime ;
        c.a = 1.0f - Mathf.Clamp01(elapsedTime / NotificationDuration);
        image.color = c;
		image2.color = c;
		
		
		// text
		float alpha = Mathf.Lerp(1f, 0f, elapsedTime/NotificationDuration);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
		text2.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
		
		if (Input.GetKeyDown(KeyCode.M))
        {
			colorOriginator();
			break;
		}
    }
	// t1.CrossFadeAlpha(0.0f, 0.05f, false);
	hideThinkingBox();
	hideMissionBox();
	// image.color = origC1;
	// image2.color = origC2;
	// text.color = origT1;
	// text2.color = origT2;
	colorOriginator();
}

void colorOriginator()
{
			Image image1 = ThinkingDisplay.GetComponent<Image>();
			Image image2 = MissionDisplay.GetComponent<Image>();
			image1.color = origC1;
			image2.color = origC2;
			thinkingText.color = origT1;
			missionText.color = origT2;
}

	
	
}
