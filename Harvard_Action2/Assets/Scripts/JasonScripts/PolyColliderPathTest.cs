using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyColliderPathTest : MonoBehaviour{

	public PolygonCollider2D myCollider;
	public int totalPoints; // how many points on the collider, to limit the array

	public Transform testObject; // the object that will be moved to each point

	public Vector2[] shapePoints; 

	public Vector2 currentPoint; //current location of the object
	public int currentIndex = 0; // current index, should be set according to the closest to astronaught on landing
	public int startIndex; // on collision, 
	private float i = 0;

    // Start is called before the first frame update
    void Start(){
		myCollider = GetComponent<PolygonCollider2D>();
		
        totalPoints = myCollider.GetTotalPointCount(); 
		Debug.Log("Total Vertices on this polygon collider = " + totalPoints);
		
		shapePoints = myCollider.points; // this works??
		
		//movePlayer();
    }


	void Update(){
		if (Input.GetKeyDown("q")){
			movePlayer();
		}
		
	}

    void FixedUpdate(){

		//currentIndex = Bounds.ClosestPoint -- use this to find the starting point?
        
		//for (currentIndex = 0; currentIndex < totalPoints; currentIndex++){
		
		//timer
		i += .01f;
		if (i == 1){
			movePlayer();
			i=0;
		}

	//test
		// currentPoint.x = shapePoints[currentIndex].x;
		// currentPoint.y = shapePoints[currentIndex].y;
		// testObject.position = currentPoint;

		


    }
	
	public void movePlayer(){
		if (currentIndex >= totalPoints){currentIndex=0;} 
		currentPoint.x = shapePoints[currentIndex].x;
		currentPoint.y = shapePoints[currentIndex].y;
		testObject.position = currentPoint;
		Debug.Log("current point = " + currentPoint);
		currentIndex++; 
	}
	
}

//Unity Docs on PolygonCollider2D: https://docs.unity3d.com/ScriptReference/PolygonCollider2D.html