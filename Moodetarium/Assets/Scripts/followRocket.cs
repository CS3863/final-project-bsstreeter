using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followRocket : MonoBehaviour
{
    public GameObject rocket;
    public float camRotationSpeed = 50f;
    private Vector3 posOffset;
    private Vector3 lookOffset;
    public float cameraLookAbove;

    void Start()
    {
        posOffset = transform.position - rocket.transform.position;
        lookOffset = new Vector3(0, cameraLookAbove, 0);
        transform.position = rocket.transform.position + (transform.forward * posOffset.y) + (rocket.transform.up * posOffset.y);
        transform.LookAt(rocket.transform.position + lookOffset);
    }

    private void UpdateCameraPosition()
    {
        //Move
        Vector3 newCamPosition = rocket.transform.position + (rocket.transform.forward * posOffset.z) + (rocket.transform.up * posOffset.y);
        transform.position = newCamPosition;

        //Rotate
        Quaternion newCamRotation = Quaternion.LookRotation(rocket.transform.position + lookOffset - transform.position);
        newCamRotation = Quaternion.Slerp(transform.rotation, newCamRotation, camRotationSpeed * Time.smoothDeltaTime); //spherical lerp smoothing
        transform.rotation = newCamRotation;

    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }
}
