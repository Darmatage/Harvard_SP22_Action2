using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
	public AudioClip layer1, layer2, layer3;
	public AudioSource audioSrc1;
	public AudioSource audioSrc2;
	public AudioSource audioSrc3;
	
	
	void Awake()
	{
		print("I'm wide awake!");
		//initiate game's sound!
		playLayer1();
		playLayer2();
		playLayer3();
		muteLayer2();
		muteLayer3();
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
	public void playLayer1()
	{
		print("I am playing layer1");
		audioSrc1.loop = true;
		audioSrc1.PlayOneShot(layer1);
			
	}
	
	public void playLayer2()
	{
		audioSrc2.loop = true;
		audioSrc2.PlayOneShot(layer2);
			
	}
	
	
	public void playLayer3()
	{
		audioSrc3.loop = true;
		audioSrc3.PlayOneShot(layer3);
			
	}
	
	
	public void muteLayer1()
	{

		audioSrc1.mute = !audioSrc1.mute;
			
	}
	
	public void muteLayer2()
	{
		
		audioSrc2.mute = !audioSrc2.mute;
			
	}
	
	
	public void muteLayer3()
	{
		
		audioSrc3.mute = !audioSrc3.mute;
			
	}
	
	
	
	
}
