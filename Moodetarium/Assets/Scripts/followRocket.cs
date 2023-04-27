using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followRocket : MonoBehaviour
{
    public GameObject rocket;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = rocket.transform.position + new Vector3(0, 4, -4);
       // Vector3 rPos = transform.position + transform.forward + 
    }
}
