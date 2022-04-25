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
	public GameObject missionNextCloseButton;
	public GameObject ThinkingDisplay;
	public Text thinkingText;
	public GameObject thinkingPrevButton;
	public GameObject thinkingNextButton;
	
	List<string> thinkingList = new List<string>();
	private GameHandler gameHandler;
	public string currGameStatus;
	public int currThoughtIndex = 0;
	private int maxIndex=0;
	
	public string currDisplayScene;
	
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		currGameStatus = gameHandler.currCheckpointName;
		currDisplayScene = currGameStatus;
		
		// very first mission. Append thinking to list
	
	
	}

    // Update is called once per frame
    void Update()
    {
		
		if(gameHandler.newCheckPointTouched)
		{
			print("gamehandler new updated!");
			currGameStatus = gameHandler.currCheckpointName;
			StatusUpdater();
		}
		
		maxIndex = thinkingList.Count-1;
		
		// this will display the current index on the canvas
		thinkingText.text = thinkingList[currThoughtIndex];
		
    }
	
	public void StatusUpdater() 
	{
		
		if(currGameStatus == "CP_tutorial_1")
		{
			print("update checkpoint 1");
			thinkingList.Add("Where am I? I heard an explosion... Let me travel forward (arrow keys). I notice a hole in my suit. What happens when I release more oxygen (left click)");
			missionText.text = "use the debris to get to the upper level of the storage component of the space station";
		}
		
		if(currGameStatus == "CP_tutorial_2")
		{
			print("update checkpoint 2");
			thinkingList.Add("message 2: press mouse left button and fly! uh oh but don't let your oxygent run out...");
		}
		
		if(currGameStatus == "CP_tutorial_3")
		{
			thinkingList.Add("message 3 yes yes yes!!");
			
			missionText.text = "testing another mission but the prev mission still applies!";
		}
		
		currThoughtIndex = thinkingList.Count - 1; // this updates the index to the most current one
		// reset the displays when a new checkpoint it reached.
		MissionDisplay.SetActive(true);
		ThinkingDisplay.SetActive(true);
	}
}
