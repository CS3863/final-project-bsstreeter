using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class rocketController : MonoBehaviour
{
    private GameObject target;
    public float degreesPerSecond = 5;

    void Start()
    {
        target = GameObject.Find("Sun");
        transform.Rotate(new Vector3(0, -90, 0));
    }

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);
    }
}
