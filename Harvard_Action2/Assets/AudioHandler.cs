using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is for isGrounded movement
public class AudioHandler : MonoBehaviour
{
	public static AudioClip walk, ox, jump, spike, checkpoint, level, ox_refill, throw_debris, land, no_air, egg_jump, egg_walk, oxActivated, airlock, door, door_warning;
	public static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        walk = Resources.Load<AudioClip> ("metal_clank");
		ox = Resources.Load<AudioClip> ("ox");
		jump = Resources.Load<AudioClip> ("jump");
		spike = Resources.Load<AudioClip> ("spike");
		checkpoint = Resources.Load<AudioClip> ("checkpoint");
		level = Resources.Load<AudioClip> ("level");
		ox_refill = Resources.Load<AudioClip> ("ox_refill");
		throw_debris = Resources.Load<AudioClip> ("throw_debris");
		land = Resources.Load<AudioClip> ("metal");
		no_air = Resources.Load<AudioClip> ("no_air");
		egg_jump = Resources.Load<AudioClip> ("egg_jump");
		egg_walk = Resources.Load<AudioClip> ("egg_walk");
		oxActivated = Resources.Load<AudioClip> ("oxActivated");
		airlock = Resources.Load<AudioClip> ("airlock");
		door_warning = Resources.Load<AudioClip> ("door_warning");
		door = Resources.Load<AudioClip> ("door");
		audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// play sound one time
	public static void PlaySound(string clip)
	{

		switch(clip) 
		{
			case "walk":
				audioSrc.PlayOneShot(walk);
				break;
			case "ox":
				audioSrc.PlayOneShot(ox);
				break;
			case "jump":
				audioSrc.PlayOneShot(jump);
				break;
				
				
			case "spike":
				audioSrc.PlayOneShot(spike);
				break;
			case "checkpoint":
				audioSrc.PlayOneShot(checkpoint, 0.5f);
				break;
			case "level":
				audioSrc.PlayOneShot(level);
				break;
			case "ox_refill":
				audioSrc.PlayOneShot(ox_refill, 1);
				break;
			case "throw_debris":
				audioSrc.PlayOneShot(throw_debris, 1);
				break;
			case "land":
				audioSrc.PlayOneShot(land);
				break;
			case "no_air":
				audioSrc.PlayOneShot(no_air);
				break;
			case "egg_jump":
				audioSrc.PlayOneShot(egg_jump);
				break;
			case "egg_walk":
				audioSrc.PlayOneShot(egg_walk);
				break;
			case "oxActivated":
				audioSrc.PlayOneShot(oxActivated);
				break;
			case "airlock":
				audioSrc.PlayOneShot(airlock, 1);
				print("I AM PLAYING AIRLOCK!!");
				break;
			case "door_warning":
				audioSrc.PlayOneShot(door_warning);
				print("I AM PLAYING doorW!!");
				break;
			case "door":
				audioSrc.PlayOneShot(door, 1);
				print("I AM PLAYING door!!");
				break;
		}
	}
	
		public static void PlaySoundLoop(string clip, bool play)
	{

		switch(clip) 
		{
			case "walk":
			print("Im in walk LOOP");
				audioSrc.clip = walk;
				audioSrc.loop = true;
				if(play)
				{
					if(!audioSrc.isPlaying)
						audioSrc.Play();
				}
				else
				{
					audioSrc.Stop();
					audioSrc.loop = false;
				}
				break;
		}
	}
	
	// slight delay for loops
	IEnumerator delay()  
	{
    yield return new WaitForSeconds(0.5f);
	}
	
	
}
