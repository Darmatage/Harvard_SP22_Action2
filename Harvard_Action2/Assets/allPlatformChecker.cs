using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class allPlatformChecker : MonoBehaviour

{
	public bool objectCollision = false;
	public String typeOfPlatform;
	public GameObject gravHelper;
	// public GameObject centerOfGravity;
    // Start is called before the first frame update
    void Start()
    {
        typeOfPlatform = "";
    }

    // Update is called once per frame
    void Update()
    {
		
		Vector2 dir2 = transform.position - gravHelper.transform.position;
        dir2 = -dir2.normalized;
        Debug.DrawRay(transform.position, dir2*2,Color.yellow, 1.0f);
		
        // RaycastHit2D [] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
		RaycastHit2D [] raycastAll = Physics2D.RaycastAll(transform.position, dir2, 2f);
		
		print("raycastAll.Length " + raycastAll.Length);
		
		foreach (RaycastHit2D other in raycastAll) 
				{
				   // if(other.collider.tag == "platform" || other.collider.tag == "platformLeft" || other.collider.tag == "platformRight" || other.collider.tag == "platformUpsidedown" )
					// {
						// objectCollision = true;
						// typeOfPlatform = other.collider.tag;
						
					// }
					// else
					// {
						// typeOfPlatform = "did not find platform";
					// }
				}
    }
	
	public void OnTriggerEnter2D(Collider2D other) {
		
		print("I have hit something....hmmm ");
		if(other.tag == "platform" || other.tag == "platformLeft" || other.tag == "platformRight" || other.tag == "platformUpsidedown" )
					{
						objectCollision = true;
						typeOfPlatform = other.tag;
						
					}
		
	}
	
	public void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "platform" || other.tag == "platformLeft" || other.tag == "platformRight" || other.tag == "platformUpsidedown" )
					{
						objectCollision = false;
						typeOfPlatform = "did not find platform";
						
					}
		
	}
}
