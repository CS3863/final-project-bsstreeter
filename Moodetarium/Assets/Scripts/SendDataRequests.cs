using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SendDataRequests : MonoBehaviour
{
    // private PlanetManager planetManager;
    WebDataRequester webRequester;

    void Awake()
    {
        webRequester = GetComponent<WebDataRequester>();
        webRequester.setPlanetManager(); 
        StartCoroutine(webRequester.GetAvailableColleges());
        // this is moved here instead of awake to make sure planets are created before things are done to them
        InvokeRepeating("callGetData", 1.0f, 5.0f);
    }

    public void callGetData() {
        webRequester.getData();
    }

    /*void setPlanetManager()
    {
        planetManager = GameObject.Find("Planets").GetComponent<PlanetManager>();
    }

    IEnumerator GetAvailableColleges()
    {
        Debug.Log("Getting available colleges");
        UnityWebRequest dataReq = UnityWebRequest.Get("https://ineffablezoe.wixsite.com/moodetarium/_functions/collegesWithResponses");
        yield return dataReq.SendWebRequest();
        if (dataReq.result == UnityWebRequest.Result.ConnectionError || dataReq.result == UnityWebRequest.Result.ProtocolError) 
        {
            Debug.Log("ERROR: " + dataReq.error);
        } 
        else 
        {
            colleges =   new List<string>(DataHandler.parseResponse(dataReq.downloadHandler.text).Keys);
            planetManager.createPlanets(colleges);
        }
    }

    void getData() 
    {
        Debug.Log("retrieving data");
        StartCoroutine(GetAverageMoods());
        StartCoroutine(GetSubmissionCounts());
    }

    IEnumerator GetAverageMoods()
    {
        UnityWebRequest dataReq = UnityWebRequest.Get("https://ineffablezoe.wixsite.com/moodetarium/_functions/averageMoods");
        yield return dataReq.SendWebRequest();
        if (dataReq.result == UnityWebRequest.Result.ConnectionError || dataReq.result == UnityWebRequest.Result.ProtocolError) 
        {
            Debug.Log("ERROR: " + dataReq.error);
        } 
        else 
        {
            moodDict =  DataHandler.parseResponse(dataReq.downloadHandler.text);
            planetManager.setPlanetColors(moodDict);
        }
    }

    IEnumerator GetSubmissionCounts()
    {
        UnityWebRequest dataReq = UnityWebRequest.Get("https://ineffablezoe.wixsite.com/moodetarium/_functions/countSubmissionsByCollege");
        yield return dataReq.SendWebRequest();
        if (dataReq.result == UnityWebRequest.Result.ConnectionError || dataReq.result == UnityWebRequest.Result.ProtocolError) 
        {
            Debug.Log("ERROR: " + dataReq.error);
        } 
        else 
        {
            countDict =  DataHandler.parseResponse(dataReq.downloadHandler.text);
            planetManager.setPlanetSizes(countDict);
        }
    }*/
}
