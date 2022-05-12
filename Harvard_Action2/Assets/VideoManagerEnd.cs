using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManagerEnd : MonoBehaviour
{
   // public GameObject VideoP;
   public VideoPlayer video;
   public MusicHandler MH;
 
 
	void Awake()
	{
		video.Play();
        video.loopPointReached += CheckOver;
	}
	
	
    void Start()
    {
		// MH.muteLayer2();
		// MH.muteLayer3();
        // video = VideoP.GetComponent<VideoPlayer>();
        // video.Play();
        // StartCoroutine("WaitForMovieEnd");
    }
 
	     void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
       SceneManager.LoadScene("WinScene");//the scene that you want to load after the video has ended.
    }
	
}