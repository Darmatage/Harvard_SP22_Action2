using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
   // public GameObject VideoP;
   public VideoPlayer video;
 
 
	void Awake()
	{
		video.Play();
        video.loopPointReached += CheckOver;
	}
	
	
    void Start()
    {
        // video = VideoP.GetComponent<VideoPlayer>();
        // video.Play();
        // StartCoroutine("WaitForMovieEnd");
    }
 
 
    public IEnumerator WaitForMovieEnd()
    {
		print("i am waiting for video");
        // while (video.isPlaying)
        // {
            // yield return new WaitForEndOfFrame();
         
        // }
		yield return new WaitForSeconds(10);
        OnMovieEnded();
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