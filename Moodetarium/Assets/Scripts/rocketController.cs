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
        transform.Rotate(new Vector3(0, 90, 0));
    }

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);
    }
    /*// Start is called before the first frame update
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
    }*/
}
