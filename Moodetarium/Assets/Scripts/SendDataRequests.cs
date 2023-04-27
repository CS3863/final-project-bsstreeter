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
    }

    private void Start()
    {
        StartCoroutine(requester.GetAvailableColleges());
        // getData is here instead of awake to make sure planets are created before things are done to them
        requester.getData();
    }
}
