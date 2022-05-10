using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
	public AudioClip layer1, layer2, layer3;
	public AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        layer1 = Resources.Load<AudioClip> ("musicL1");
		layer2 = Resources.Load<AudioClip> ("musicL2");
		layer3 = Resources.Load<AudioClip> ("music/l3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// play sound one time
	public void playLayer1()
	{
		
		audioSrc.PlayOneShot(layer1);
			
	}
	
	public void playLayer2()
	{
		
		audioSrc.PlayOneShot(layer2);
			
	}
	
	
	public void playLayer3()
	{
		
		audioSrc.PlayOneShot(layer3);
			
	}
	
	
	
	
}
