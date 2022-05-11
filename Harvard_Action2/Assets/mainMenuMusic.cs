using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuMusic : MonoBehaviour
{
	public AudioClip menu;
	public AudioSource audioSrc1;
	
	void Awake()
	{

		//initiate game's sound!
		playMenuLayer();
	}
	
    // Start is called before the first frame update
    void Start()
    {
        // layer1 = Resources.Load<AudioClip> ("musicL1");
		// layer2 = Resources.Load<AudioClip> ("musicL2");
		// layer3 = Resources.Load<AudioClip> ("music/l3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// play sound one time
	public void playMenuLayer()
	{
		print("I am playing menu");
		audioSrc1.loop = true;
		audioSrc1.PlayOneShot(menu);
			
	}
	
	
	public void muteMenu()
	{

		audioSrc1.mute = !audioSrc1.mute;
			
	}
	
	
	
}
