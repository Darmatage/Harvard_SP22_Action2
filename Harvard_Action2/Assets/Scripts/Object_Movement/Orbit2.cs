
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Orbit2 : MonoBehaviour
{
    public GameObject objectToOrbit;
    public Vector3 direction;
    public float angle;
    public float radius;
    public float degreesPerSecond = 10;
    private void Start()
    {
        direction = (transform.position - objectToOrbit.transform.position).normalized;
        radius = Vector3.Distance(objectToOrbit.transform.position, transform.position);
    }
    private void Update()
    {
        angle += degreesPerSecond * Time.deltaTime;
        if (angle > 360)
        {
            angle -= 360;
        }
        Vector3 orbit = Vector3.forward * radius;
        orbit = Quaternion.LookRotation(direction) * Quaternion.Euler(0, angle, 0) * orbit;
        transform.position = objectToOrbit.transform.position + orbit;
    }
}