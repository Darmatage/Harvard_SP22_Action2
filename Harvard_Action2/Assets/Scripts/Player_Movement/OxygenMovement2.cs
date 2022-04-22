using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMovement2 : MonoBehaviour
{
    [Header("Oxygen Burst")]
    Rigidbody2D rigidbody2d;
    [SerializeField] float burstPower;
    [SerializeField] bool isGrounded = true;

    [Header("Particle Effect")]
    public GameObject oxygenParticles;
    public Transform handEnd;
    public Transform shoulder;
    public float OxygenDepletion = .05f;
    public float OxygenBurstDepletion;

    public OxBarScript oxBar;
    public bool OxygenOn;
    GameObject particlesTemp;
    private Quaternion armRotation;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Burst();

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        armRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        startOxygen();
    }

    public void startOxygen()
    {


        if (Input.GetMouseButton(1))
        {
            OxygenOn = true;
            oxBar.timeToDamage = OxygenBurstDepletion;
            print("the ox level in THRUSTER is " + oxBar.timeToDamage);
            particlesTemp = Instantiate(oxygenParticles, handEnd.position, Quaternion.identity);
            particlesTemp.transform.SetParent(handEnd);
            particlesTemp.transform.LookAt(particlesTemp.transform.position - (shoulder.position - particlesTemp.transform.position));
        }

        else
        {
            OxygenOn = false;
            Destroy(particlesTemp);
            oxBar.timeToDamage = OxygenDepletion;
        }
    }
    void Burst()
    {
        if (Input.GetMouseButton(1))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * burstPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }
}