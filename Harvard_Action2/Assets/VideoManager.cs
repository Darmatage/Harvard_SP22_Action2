using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
   // public GameObject VideoP;
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
		screen.SetActive(true);
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Opening.mp4");
		videoPlayer.SetDirectAudioMute(0,false);
		videoPlayer.Play();
        videoPlayer.loopPointReached += CheckOver;
        // video = VideoP.GetComponent<VideoPlayer>();
        // video.Play();
        // StartCoroutine("WaitForMovieEnd");
    }
 
	
	public void startVideo ()
	{
		screen.SetActive(true);
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Opening.mp4");
		videoPlayer.SetDirectAudioMute(0,false);
		videoPlayer.Play();
        videoPlayer.loopPointReached += CheckOver;
	}
 
     void OnMovieEnded()
    {
        SceneManager.LoadScene("World2");
    }
	
	 // void EndReached(UnityEngine.Video.VideoPlayer vp)
    // {
        // vp.playbackSpeed = vp.playbackSpeed / 10.0F;
		// SceneManager.LoadScene("World2");
    // }
	     void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
         SceneManager.LoadScene("World2");//the scene that you want to load after the video has ended.
    }
	
}