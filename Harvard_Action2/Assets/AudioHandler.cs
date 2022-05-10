using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
	public static AudioClip walk, ox, jump;
	public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        walk = Resources.Load<AudioClip> ("metal_clank");
		ox = Resources.Load<AudioClip> ("steam");
		jump = Resources.Load<AudioClip> ("jump");
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
			case "ox":
				audioSrc.clip = ox;
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
