using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followRocket : MonoBehaviour
{
    public GameObject rocket;
//    public float camMotionSpeed = 50f;
    public float camRotationSpeed = 50f;
    private Vector3 offset;
    public float cameraLookAbove;

    void Start()
    {
        offset = transform.position - rocket.transform.position;
        transform.position = rocket.transform.position + (transform.forward * offset.z) + (rocket.transform.up * offset.y);
        transform.LookAt(rocket.transform.position + new Vector3(0, cameraLookAbove, 0));
    }

    private void UpdateCameraPosition()
    {
        //Move
        Vector3 newCamPosition = rocket.transform.position + (rocket.transform.forward * offset.z) + (rocket.transform.up * offset.y);
        transform.position = newCamPosition;

        //Rotate
        Quaternion newCamRotation = Quaternion.LookRotation(rocket.transform.position - transform.position);
        newCamRotation = Quaternion.Slerp(transform.rotation, newCamRotation, camRotationSpeed * Time.smoothDeltaTime); //spherical lerp smoothing
        transform.rotation = newCamRotation;

    }

    private void Update()
    {
        UpdateCameraPosition();
    }
}
