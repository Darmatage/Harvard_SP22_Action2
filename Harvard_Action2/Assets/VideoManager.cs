using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
   // public GameObject VideoP;
   public VideoPlayer videoPlayer;
   public GameObject screen;
 
	// image fader
	    public Image imageToFade;
        public float alphaLevel = 0f;
 
 
	void Awake()
	{
		screen.SetActive(false);
		// image.SetActive(false);
		// video.Play();
        // video.loopPointReached += CheckOver;
	}
	
	
    void Start()
    {
		// imageToFade.GetComponent().color = new Color(1, 1, 1, alphaLevel);
		
		
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
		 // image.SetActive(true);
		 StartCoroutine(FadeIn(imageToFade));
         SceneManager.LoadScene("World2");//the scene that you want to load after the video has ended.
    }
	
        IEnumerator FadeIn(Image fadeImage){
			print("ALPHA IS WORKING!");
               for (int i = 0; i < 100; i++){
						print("ALPHA IS WORKING! " + i);
                        alphaLevel += 0.02f;
                        
                        fadeImage.color = new Color(1, 1, 1, alphaLevel);
                        Debug.Log("Alpha is: " + alphaLevel);
                }
				yield return null;
				// SceneManager.LoadScene("World2");//the scene that you want to load after the video has ended.
		}
	
}