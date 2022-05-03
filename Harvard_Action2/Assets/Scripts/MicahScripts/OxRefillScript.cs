using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxRefillScript : MonoBehaviour
{
    private GameObject player;
  
    private Rigidbody2D rigidBody;

    public float Ox;

    public GameHandler gameHandler;
    public OxBarScript OxBarScript;




    void Start()
    {

    }

    void FixedUpdate()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "OxRefill")
        {
            Ox = Ox + 10;
        }
    }
}
