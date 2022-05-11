using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// non grounded movement
public class AudioHandlerObj : MonoBehaviour

{
	public AudioClip walk, ox, jump, low_ox, egg_walk;
	public  AudioSource audioSrcOther;
	public  AudioSource audioSrcFlying;
	public  AudioSource audioSrcLife;
	public  AudioSource audioSrcWalk;
	public  AudioSource audioSrcEgg;

    // Start is called before the first frame update
    void Start()
    {
        walk = Resources.Load<AudioClip> ("metal_clank");
		ox = Resources.Load<AudioClip> ("ox");
		jump = Resources.Load<AudioClip> ("jump");
		low_ox = Resources.Load<AudioClip> ("low_ox");
		egg_walk = Resources.Load<AudioClip> ("egg_walk");
		// audioSrcWalking = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// play sound one time
	public void PlaySound(string clip)
	{

		switch(clip) 
		{
			case "walk":
				audioSrcOther.PlayOneShot(walk);
				break;
			case "ox":
				audioSrcOther.PlayOneShot(ox);
				break;
			case "jump":
				audioSrcOther.PlayOneShot(jump);
				break;
			
		}
	}
	
		public void PlaySoundLoop(string clip, bool play)
	{

		switch(clip) 
		{
			case "ox":
				audioSrcFlying.clip = ox;
				audioSrcFlying.loop = true;
				if(play)
				{
					print("WE ARE PLAYING OX FLY");
					if(!audioSrcFlying.isPlaying)
					{
						audioSrcFlying.volume = 0.5f;
						audioSrcFlying.Play();
					}
				}
				else
				{
					audioSrcFlying.Stop();
					audioSrcFlying.loop = false;
				}
				break;
		}
		switch(clip) // quick breathing ox is low
		{
			case "low_ox":
				audioSrcLife.clip = low_ox;
				audioSrcLife.loop = true;
				if(play)
				{
					print("WE ARE PLAYING OX FLY");
					if(!audioSrcLife.isPlaying)
					{
						audioSrcLife.volume = 0.5f;
						audioSrcLife.Play();
					}
				}
				else
				{
					audioSrcLife.Stop();
					audioSrcLife.loop = false;
				}
				break;
		}
				switch(clip) 
		{
		case "walk":
				audioSrcWalk.clip = walk;
				audioSrcWalk.loop = true;
				if(play)
				{
					if(!audioSrcWalk.isPlaying)
						audioSrcWalk.Play();
				}
				else
				{
					audioSrcWalk.Stop();
					audioSrcWalk.loop = false;
				}
				break;
		case "egg_walk":
				audioSrcEgg.clip = egg_walk;
				audioSrcEgg.loop = true;
				if(play)
				{
					if(!audioSrcEgg.isPlaying)
						audioSrcEgg.Play();
				}
				else
				{
					audioSrcEgg.Stop();
					audioSrcEgg.loop = false;
				}
				break;
	}
	
		
	
	}
	
	
	
}
