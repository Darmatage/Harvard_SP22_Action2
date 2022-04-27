using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this script can be made to make simple goals happen. GameHandler updates
// points
public class puzzleTarget : MonoBehaviour
{
	private GameHandler gameHandler;
	public GameObject goalItem;
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
        if (collision.gameObject == goalItem)
        {
            gameHandler.pointsScored = gameHandler.pointsScored + 1;
			print("a mission has just been completed ");
        }
	}
}
