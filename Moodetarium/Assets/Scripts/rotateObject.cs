using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed = 2.0f;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
    }
}
