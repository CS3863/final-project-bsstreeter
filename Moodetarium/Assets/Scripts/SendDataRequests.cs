using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SendDataRequests : MonoBehaviour
{
    // private PlanetManager planetManager;
    AbstractDataRequester requester;

    void Awake()
    {
        requester = GetComponent<AbstractDataRequester>();
        requester.setPlanetManager(); 
        StartCoroutine(requester.GetAvailableColleges());
        // this is moved here instead of awake to make sure planets are created before things are done to them
        InvokeRepeating("callGetData", 1.0f, 5.0f);
    }

    public void callGetData() {
        requester.getData();
    }
}
