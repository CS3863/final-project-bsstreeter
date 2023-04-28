using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followRocket : MonoBehaviour
{
    public GameObject rocket;
    private Vector3 offset;
    

    // Start is called before the first frame update
    void Start()
    {
        // get the offset of the camera at the start so that it isn't hardcoded
        offset = transform.position - rocket.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = rocket.transform.position + offset; 
    }
}
