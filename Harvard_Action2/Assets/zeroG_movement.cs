using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zeroG_movement : MonoBehaviour
{
	public float moveSpeed = 15;
	private Vector2 moveDir;
    Rigidbody2D rb;
	// Start is called before the first frame update
	//
	public void Start() 
	{
			rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
    void Update()
    {
      moveDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;  
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector3 transPos = transform.TransformDirection(moveDir);
		Vector2 transPos2 =  new Vector2(transPos.x, transPos.y);
        rb.MovePosition(rb.position + transPos2 * moveSpeed * Time.deltaTime);
    }
}
