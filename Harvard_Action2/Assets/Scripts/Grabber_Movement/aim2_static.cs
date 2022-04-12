using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// camera needs to be in orthographic
public class aim2_static : MonoBehaviour
{
	
	
public float speed = 5f;


    // Update is called once per frame
    public Quaternion  Update()
    {

			Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
			print("transform.rotation = Quaternion " + transform.rotation);
			return transform.rotation;
    }
}
