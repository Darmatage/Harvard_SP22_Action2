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
	public int currThoughtIndex = 0;
	public int maxIndex=0;
	
	public string currDisplayScene;
	
	void Awake()
	{
	
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		currGameStatus = gameHandler.currCheckpointName;
		currDisplayScene = currGameStatus;
		thinkingList.Add("basic controls: Right/Left arrows for movement on platforms. Right Mouse Button to catch an Object and Throw it. Left Mouse Button fires Oxygen. Walk directly off a platform to allow Oxygen Thrusters");
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
			StatusUpdater();
		}
		
		// maxIndex = thinkingList.Count-1;
		// print("maxIndex " + maxIndex);
		// this will display the current index on the canvas
		thinkingText.text = thinkingList[currThoughtIndex];
				// print( " the thinkingText.text = thinkingList[currThoughtIndex]);
    }
	
	public void StatusUpdater() 
	{
		print("the currGameStatus " + currGameStatus);
		
		if(currGameStatus == "CP_sample_1" || currGameStatus == "Checkpoint (1)")
		{
			print("update checkpoint 1");
			thinkingList.Add("Where am I? I heard an explosion... Let me travel forward (arrow keys). I notice a hole in my suit. What happens when I release more oxygen (left click)");
			missionText.text = "use the debris to get to the upper level of the storage component of the space station";
		}
		
		if(currGameStatus == "CP_sample_2")
		{
			print("update checkpoint 2");
			thinkingList.Add("message 2: press mouse left button and fly! uh oh but don't let your oxygent run out...");
		}
		
		if(currGameStatus == "CP_sample_3")
		{
			thinkingList.Add("message 3 yes yes yes!!");
			
			missionText.text = "testing another mission but the prev mission still applies!";
		}
		
		if(currGameStatus == "CP_debris_1_1" || currGameStatus == "CP_debris_1_2" || currGameStatus == "CP_debris_1_3")
		{
			thinkingList.Add("I need to catch and throw objects. I need to watch out for yellow spikes!");
			
			missionText.text = "Get to the top of the debris field";
		}
		//
		if(currGameStatus == "CP_debris_18" || currGameStatus == "CP_debris_19" || currGameStatus == "CP_debris_20")
		{
			thinkingList.Add("What is this ahead? Look for a green egg shaped thing. Walk into it...");
			
			missionText.text = "Get into the Green Egg!";
		}
		if(currGameStatus == "CP_spaceship_1")
		{
			thinkingList.Add("I am in an egg? I still have my Oxygen Thruster (right click), my grabber (leftclick). Hmmm. I notice I also can stick to the ground. Notice pushing arrow left goes right when I'm upside down on this Alien Orb!");
			
			missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if(currGameStatus == "CP_spaceship_2")
		{
			thinkingList.Add("I am in an egg? I still have my Oxygen Thruster (right click), my grabber (leftclick). Hmmm. I notice I also can stick to the ground. Notice pushing arrow left goes right when I'm upside down on this Alien Orb!");
			
			missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if(currGameStatus == "CP_spaceship_3")
		{
			thinkingList.Add("If I push the up arrow, I jump and the magnetization of this egg turns off!");
			
			missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if(currGameStatus == "CP_spaceship_4")
		{
			thinkingList.Add("I notice I have more energy. This Egg is giving me oxygen! Somehow it is more powerful than my suit!");
			
			missionText.text = "Go Around the Alien Orb and Travel to the Green Alien Teleporter";
		}
		if(currGameStatus == "CP_spaceship_15")
		{
			thinkingList.Add("I'm almost at the end. Keep pushing the up arrow and launching from orb to orb. My magnetism is helping me!");
			
			missionText.text = "I need to push the Green Alien Machine into it's puzzle slot. Only then will this egg be able to leave this alien ship and fly me home";
		}
		if(currGameStatus == "CP_spaceship_16")
		{
			thinkingList.Add("I need to push the Green Alien Machine into it's puzzle slot. Only then will this egg be able to leave this alien ship and fly me home");
			
			missionText.text = "Push the green machine component into the engine slot";
		}
		if(currGameStatus == "CP_spaceship_17")
		{
			thinkingList.Add("All I need to do is to push into it. Then it will move");
			
			missionText.text = "Push the green machine component into the engine slot";
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
		MissionDisplay.SetActive(true);
		ThinkingDisplay.SetActive(true);

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
	
	
}
