using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyColliderPathTest : MonoBehaviour{

	public PolygonCollider2D myCollider;
	public int totalPoints; // length of the array

	public Transform testObject; // the object that will be moved to each point

	public Vector2[] shapePoints; 

	public Vector2 currentPoint;
	public int currentIndex = 0;
	public int startIndex; 
	private float i = 0;

    void Start(){
		myCollider = GetComponent<PolygonCollider2D>();
		
        totalPoints = myCollider.GetTotalPointCount();		
		shapePoints = myCollider.points; // How to get the points to recalculate when roatted?
    }


	void Update(){
		if (Input.GetKeyDown("q")){
			movePlayer();
		}
	}

    void FixedUpdate(){
		//currentIndex = Bounds.ClosestPoint -- use this to find the starting point?
		//for (currentIndex = 0; currentIndex < totalPoints; currentIndex++){
		
		//TIMER:
		// i += .01f;
		// if (i == 1){
			// movePlayer();
			// i=0;
		// }
    }
	
	public void movePlayer(){
		currentIndex++; 
		if (currentIndex >= totalPoints){currentIndex=0;} 
		currentPoint.x = shapePoints[currentIndex].x + transform.position.x;
		currentPoint.y = shapePoints[currentIndex].y + transform.position.y;
		testObject.position = currentPoint;
		Debug.Log("current point = " + currentPoint);
	}

}

//Unity Docs on PolygonCollider2D: https://docs.unity3d.com/ScriptReference/PolygonCollider2D.html