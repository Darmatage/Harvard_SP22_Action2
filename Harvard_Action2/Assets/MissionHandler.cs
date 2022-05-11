using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;  // maybe delete?

public class MissionHandler : MonoBehaviour
{

	// for sound
	public MusicHandler MH;

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
	public int maxIndex = 0;

	public string currDisplayScene;


	// use this for tracking if new mission is applied
	string prevMission;


	// for fading UI thought boxes
	public bool makeBoxesFade = false;
	public float NotificationDuration = 5f;
	private YieldInstruction fadeInstruction = new YieldInstruction();

	// or make boxes vanish
	public bool makeBoxesVanish = true;

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
		thinkingList.Add("What just happened? I'd better check the rest of the space station.                    (Right Mouse Button to move in space)");
		thinkingText.text = thinkingList[currThoughtIndex];
		maxIndex = thinkingList.Count - 1;

		// very first mission. Append thinking to list


	}

	// Update is called once per frame
	void Update()
	{

		if (gameHandler.newCheckPointTouched)
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
			gameHandler.currentLevelInteger++;
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
			print("m has been pushed!");
			colorOriginator();
			// if any one of them are actice we can hide them both with M
			if (ThinkingDisplay.activeSelf)
			{
				// hideMissionBox();
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
				if (makeBoxesFade)
				{
					Image image1 = ThinkingDisplay.GetComponent<Image>();
					Image image2 = MissionDisplay.GetComponent<Image>();
					StartCoroutine(FadeOut(image1, image2, thinkingText, missionText));

				}
				if (makeBoxesVanish)
				{
					StartCoroutine(DelayDisappear());
				}
			}
		}
	}

	public void MissionUpdater()
	{
		if (currLevelOfGame == "GateDebris")
		{
			missionText.text = "MISSION: Find your way through the debris field to the escape pod";
		}
		if (currLevelOfGame == "MicahsLevel")
		{
			missionText.text = "MISSION: use the debris to get to the upper level of the storage component of the space station";
		}
		if (currLevelOfGame == "DanielsLevel")
		{
			missionText.text = "MISSION: Get to the top of the station and test the antenna";
			MH.muteLayer2();
		}

		if (currLevelOfGame == "MissionEscapePod")
		{
			missionText.text = "MISSION: Find the escape pod";
		}

		if (currLevelOfGame == "alien_level")
		{
			missionText.text = "MISSION: Use the orb to get past the alien vessel";
			MH.muteLayer3();
		}
		if (currLevelOfGame == "winGameGateway")
		{
			missionText.text = "You activate the escape pod. And you've escaped!";
			SceneManager.LoadScene("WinScene");
		}





	}



	// // @Daniel and @Micah Use a conditional on the name of the checkpoint in your level and write in
	// // the thought the player has as they pass the checkpoint. The thought should
	// // offer help about gameplay or tips for them on what they need to do 
	public void ThoughtUpdater()
	{//beginning of Tutorial


		if (currGameStatus == "Thought")
		{

			thinkingList.Add("");
		}

		if (currGameStatus == "ThoughtJump")
		{

			thinkingList.Add("(Aim with your mouse and press W/Space to JUMP.)");
		}

		if (currGameStatus == "ThoughtDestroyed")
		{

			thinkingList.Add("The station is destroyed...                                        I'd better watch my oxygen levels...");
		}

		if (currGameStatus == "ThoughtGrab")
		{

			thinkingList.Add("That's a lot of debris. Maybe I can use it?                                          (Left Click to GRAB and THROW debris)");
		}

		if (currGameStatus == "ThoughtOxy")
		{

			thinkingList.Add("Look! I can replenish my oxygen at that tank.");
		}

		if (currGameStatus == "ThoughtMagnetic")
		{

			thinkingList.Add("That's a magnetic surface.                                                          I'll stick to it if my feet get close. (press A/D to walk)");
		}

		if (currGameStatus == "ThoughtSeeCP")
		{
			thinkingList.Add("What in the name of Space Jesus is that thing?");
		}

		if (currGameStatus == "CP1")
		{
			thinkingList.Add("Oh god I think it touched me. Why do I feel like it collected a sample?");

			// missionText.text = "Push the green machine component into the engine slot";
		}

		if (currGameStatus == "ThoughtSave")
		{

			thinkingList.Add("I don't know how many oxygen tanks I'm gonna find from here on out- I'd better mind my meter.");
		}


		if (currGameStatus == "CP2" || currGameStatus == "Checkpoint (2)")
		{
			thinkingList.Add("Okay, that DEFINITELY touched me. Not cool, Audrey II.");

		}

		if (currGameStatus == "ThoughtSpike")
		{

			thinkingList.Add("Those red things look dangerous... I'd better avoid them.");
		}

		if (currGameStatus == "CargoCP")
		{
			thinkingList.Add("I've made it to the cargo hold. I should lock in that gear to test the system's functionality.");

		}

		if (currGameStatus == "ThoughtGear1")
		{
			thinkingList.Add("It looks like the cargo system still has power. But what about the rest of the station?");

		}

		if (currGameStatus == "DebrisCP")
		{
			thinkingList.Add("What a mess! I hope the escape pod bay is still intact. It's my only hope of survival...");

		}

		//Beginnning of Daniel level CP
		if (currGameStatus == "AntennaCP_1")
		{
			thinkingList.Add(" If I can get up to the antenna maybe I can call for help! I should stick close to the station... or...?");

		}

		if (currGameStatus == "AntennaCP_1.5")
		{
			thinkingList.Add("Oh no... John, Doe... If only they saw these alien eyes...");

		}

		if (currGameStatus == "AntennaCP_2")
		{
			thinkingList.Add("Halfway There...");

		}

		if (currGameStatus == "AntennaCP_3")
		{
			thinkingList.Add("Nearly there...");

		}

		if (currGameStatus == "AntennaCP_B")
		{
			thinkingList.Add("I knew there was a better way! Who's the space cadet now, Mom??");

		}

		if (currGameStatus == "AntennaCP_4")
		{
			thinkingList.Add("Phew, that was nerve wracking. Time to test the antenna.");

		}

		if (currGameStatus == "ThoughtRory")
		{
			thinkingList.Add("Lieutenant Rory, no! Damn these vines.");

		}

		if (currGameStatus == "ThoughtGear2")
		{
			thinkingList.Add("Comms systems are still up, amazingly. Maybe I can get inside to access the controls...");

		}

		if (currGameStatus == "ThoughtBackside")
		{
			thinkingList.Add("Is is just me or are the vines getting bigger? I must be nearing the source...");

		}

		if (currGameStatus == "ThoughtDoor")
		{
			thinkingList.Add("Damn, the vines are blocking entry. It sounds like the escape pod is my only hope.");

		}

		if (currGameStatus == "AntennaCP_7")
		{
			thinkingList.Add("Maybe I can push that satellite out of the way!");

		}


		// Beginning of Alien Area
		if (currGameStatus == "CPSeeEgg")
		{
			thinkingList.Add("It looks like I have no choise... Into the alien hamster ball thing!");

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

		


		// ALIEN LEVEL
		if (currGameStatus == "alien_thought")
		{
			thinkingList.Add("Go into the Large egg. Notice what you become encased in. Everything still works the same, however, now you can magentize to these circular alien artifacts");

		}
		if (currGameStatus == "alien_thought2")
		{
			thinkingList.Add("A/D move along surface of Giant Mother Egg. Notice, I magnetize to the surface. ");

		}
		if (currGameStatus == "cp_alien")
		{
			thinkingList.Add("A/D move along a surface (sometimes these commands may reverse due to alien gravity). Move along the surface to the top of the Giant Mother Egg");

		}
		if (currGameStatus == "cp_alien1")
		{
			thinkingList.Add("There is a Small Mother Egg to my left. What happens when I jump (W), aiming with the mouse cursor. Then use my rocket thrusters (right mouse button)");

		}
		if (currGameStatus == "cp_alien2")
		{
			thinkingList.Add("I notice a smaller gree object to my left. Let me try the same thing (aim with mouse cursor + W = jump). Use my magnets to latch onto it.");

		}
		if (currGameStatus == "cp_alien3")
		{
			thinkingList.Add("Keep hopping from one alien artifact to another. I must be in some sort of egg chamber. I am not losing any oxygen!");

		}
		if (currGameStatus == "cp_alien4")
		{
			thinkingList.Add("go upwards and continue to latch");

		}
		if (currGameStatus == "alien_thought3")
		{
			thinkingList.Add("Beware there are red viruses. These can kill!");

		}
		if (currGameStatus == "cp_alien5")
		{
			thinkingList.Add("Go around the Giant Mother Egg. Find the smaller eggs, and travel along them, hopping from each one until I reach the spacecraft");

		}
		if (currGameStatus == "cp_alienEND")
		{
			thinkingList.Add("Push this gear into the machine so that the Escape Pod can be actived! Then get to the bright teal capsule in the nose of the escape pod");

		}
		if (currGameStatus == "alien_thought_end")
		{
			thinkingList.Add("Push this gear! Then head to the teal capsule above!");

		}
		if (currGameStatus == "CP_alienship_5")
		{
			thinkingList.Add("This alien egg should protect me from alien spikes!");

		}
		if (currGameStatus == "alien_thought22")
		{
			thinkingList.Add("Since my arms are free, I can still catch and launch myself with debris.");

		}














		// do not change below
		// this updates the index to the most current one
		// reset the displays when a new checkpoint it reached.
		maxIndex = thinkingList.Count - 1;
		print("thinkingList.Count " + thinkingList);
		print("thinkingList.Count " + thinkingList.Count);
		thinkingText.text = thinkingList[currThoughtIndex];
		foreach (var p in thinkingList)
		{
			print(" thinkingList " + p);
		}
		print("thinkingList[currThoughtIndex]; " + thinkingList[currThoughtIndex]);
		currThoughtIndex = maxIndex;


		colorOriginator();
		MissionDisplay.SetActive(true);
		ThinkingDisplay.SetActive(true);


		// button changes
		ThinkingDisplay.SetActive(true); ;
		hideBox.GetComponent<Image>().color = Color.blue;
		hideButtonText.text = " [o]";


		// Text t1 = ThinkingDisplay.GetComponent<Text>();
		if (makeBoxesFade)
		{
			Image image1 = ThinkingDisplay.GetComponent<Image>();
			Image image2 = MissionDisplay.GetComponent<Image>();
			StartCoroutine(FadeOut(image1, image2, thinkingText, missionText));

		}
		if (makeBoxesVanish)
		{
			StartCoroutine(DelayDisappear());
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
		hideButtonText.text = " [o]";

	}
	public void thinkingBoxButton()
	{
		if (ThinkingDisplay.activeSelf)  // turn off box
		{
			colorOriginator();
			// hideThinkingBox();
			ThinkingDisplay.SetActive(false);
			// MissionDisplay.SetActive(true);
			// ThinkingDisplay.SetActive(false);
			hideBox.GetComponent<Image>().color = Color.blue;
			hideButtonText.text = " [o]";


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
		print("prevThought is clicked ! ");
		if (currThoughtIndex > 0)
		{
			currThoughtIndex = currThoughtIndex - 1;
		}
	}

	// displays the next thought -- connect to the nextButton of thinking box
	public void nextThought()
	{
		print("nextThought is clicked ");
		if (currThoughtIndex < maxIndex)
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
			elapsedTime += Time.deltaTime;
			c.a = 1.0f - Mathf.Clamp01(elapsedTime / NotificationDuration);
			image.color = c;
			image2.color = c;


			// text
			float alpha = Mathf.Lerp(1f, 0f, elapsedTime / NotificationDuration);
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
		// hideMissionBox();
		// image.color = origC1;
		// image2.color = origC2;
		// text.color = origT1;
		// text2.color = origT2;
		colorOriginator();
	}


	IEnumerator DelayDisappear()
	{
		yield return new WaitForSeconds(NotificationDuration);
		hideThinkingBox();
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
