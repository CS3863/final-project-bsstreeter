using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class rocketController : MonoBehaviour
{
    // Start is called before the first frame update
    // public CharacterController rocket;
    public float speed = 5.0f;
    public float tSpeed = 25.0f;
    
    void Start()
    {
        // rocket = GetComponent<CharacterController>();
    }

    
    
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * vertical); 
        transform.Rotate(Vector3.up, tSpeed * horizontal * Time.deltaTime); 
    }
}
