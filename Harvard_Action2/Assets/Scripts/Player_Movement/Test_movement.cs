using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_movement: MonoBehaviour {
    
	public float speed;


  Rigidbody2D rigidbody2d;

    [Header("Movement Settings")]
    // [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 1f;

    [Header("Bools")]
    [SerializeField] bool isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			
			// Transform movePos = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y + (v * speed)*Time.deltaTime);
			gameObject.transform.position = new Vector2 (transform.position.x + (h * speed)*Time.deltaTime, transform.position.y + (v * speed)*Time.deltaTime);
			// gameObject.transform.position = movePos;
			
			// rigidbody2d.AddForce(new Vector2(movePos, 0f), ForceMode2D.Impulse);
			// if (h != 0 || v != 0) 
			// {
			// rigidbody2d.velocity = gameObject.transform.position * speed;
			// }
			
			
			
			
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }
	}