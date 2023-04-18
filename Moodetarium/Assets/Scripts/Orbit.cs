using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private GameObject sun;
    public float degreesPerSecond = 5;

    void Start() {
        sun = GameObject.Find("Sun");
    }
    
    void Update()
    {
        transform.RotateAround(sun.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);
    }
}
