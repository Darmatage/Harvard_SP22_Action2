using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManagerEnd : MonoBehaviour
{
   public MusicHandler MH;
   public VideoPlayer videoPlayer;
   public GameObject screen;
 
 
	void Awake()
	{
		screen.SetActive(false);
		// video.Play();
        // video.loopPointReached += CheckOver;
	}
	
	
    void Start()
    {
		MH.muteLayer2();
		MH.muteLayer3();
		screen.SetActive(true);
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Ending.mp4");
		videoPlayer.SetDirectAudioMute(0,false);
		videoPlayer.Play();
        videoPlayer.loopPointReached += CheckOver;
    }
 
	
	public void startVideo ()
	{
		screen.SetActive(true);
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Ending.mp4");
		videoPlayer.SetDirectAudioMute(0,false);
		videoPlayer.Play();
        videoPlayer.loopPointReached += CheckOver;
	}
 

	
	     void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
         SceneManager.LoadScene("WinScene");//the scene that you want to load after the video has ended.
    }
	
}