using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject target;
    public float degreesPerSecond = 5;

    void Start() {
        if (target == null) target = GameObject.Find("Sun");
    }
    
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);
    }
}
