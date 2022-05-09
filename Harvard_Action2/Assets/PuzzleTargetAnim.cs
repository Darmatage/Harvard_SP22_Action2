using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTargetAnim : MonoBehaviour
{
	private GameHandler gameHandler;
	public GameObject goalItem;
	public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnCollisionEnter2D(Collision2D collision)
    {
		
		print("a somethinng has just been completed ");
        if (collision.gameObject == goalItem)
        {
            gameHandler.pointsScored = gameHandler.pointsScored + 1;
			print("a mission has just been completed ");
			anim.SetBool("Light", true);
        }
	}
}
