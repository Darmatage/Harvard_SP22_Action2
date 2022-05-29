using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class flipDirection : MonoBehaviour
{
	public bool isGrounded = false;
	public bool isGroundedOther =  false;
	public bool doNotActivateOnPlatform = false;
	public bool doNotActivateOnPlatformOther = false;
	public PlatformChecker PlatformCheckerScript;
	Rigidbody2D rb;
	public GameObject torso;
	Transform torsoTrans;

    void Start()
    {
	rb = GetComponent<Rigidbody2D>();;
	torsoTrans = torso.transform;
    }

    // Update is called once per frame
    void Update()
    {
		if(!doNotActivateOnPlatform) 
		{
			isGrounded = PlatformCheckerScript.isGrounded;
		}
		if(!doNotActivateOnPlatformOther)   
		{			
			isGroundedOther = PlatformCheckerScript.isGroundedOther;
		}
		if(isGrounded)
		{
			Flip();
		}
    }
	
	void Flip2()
	{
		print("flip2 " + transform.rotation + " vel " + rb.velocity);
		// Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);
		// transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime);
			
			Vector2 moveDirection = rb.velocity;
			if(moveDirection.y > 0)
			{
				print("Quaternion");
				transform.rotation = Quaternion.AngleAxis( 180, this.transform.up ) * transform.rotation;
 
			}
			if(moveDirection.y < 0)
			{
				print("Quaternion2");
				transform.rotation = new Quaternion(0, 0, 0, 1);
			}
		
             // float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
             // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			 print("flip2 AFTER " + transform.rotation + " vel " + rb.velocity);
	}

	
	// facing direction... NOTE: this ruins the oxygen script :(
    void Flip()
    {
       
        Vector3 theScale = torsoTrans.localScale;
		if(Input.GetAxis("Horizontal") > 0)
		{
			print("thescale = " + theScale);
			if(theScale.x < 0){
			 theScale.x *= -1;
			}
		}
		if(Input.GetAxis("Horizontal") < 0) 
		{
			theScale.x = -1*Math.Abs(theScale.x);
		}
        // theScale.x *= -1;
		
        torsoTrans.localScale = theScale;
	}
}
