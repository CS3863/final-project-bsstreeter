using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebDataRequester : AbstractDataRequester
{
    private PlanetManager planetManager;

    public override void setPlanetManager()
    {
        planetManager = GameObject.Find("Planets").GetComponent<PlanetManager>();
    }

    public override void startGettingData() {
        InvokeRepeating("getData", 0.0f, 5.0f);
    }

    private void getData() {
        Debug.Log("retrieving data");
        StartCoroutine(GetAverageMoods());
        StartCoroutine(GetSubmissionCounts());
    }

    public override IEnumerator GetAvailableColleges()
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
            List<string>  colleges = new List<string>(DataHandler.parseResponse(dataReq.downloadHandler.text).Keys);
            planetManager.createPlanets(colleges);
        }
    }

    protected override IEnumerator GetAverageMoods()
    {
        UnityWebRequest dataReq = UnityWebRequest.Get("https://ineffablezoe.wixsite.com/moodetarium/_functions/averageMoods");
        yield return dataReq.SendWebRequest();
        if (dataReq.result == UnityWebRequest.Result.ConnectionError || dataReq.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("ERROR: " + dataReq.error);
        }
        else
        {
            Dictionary<string, float> moodDict = DataHandler.parseResponse(dataReq.downloadHandler.text);
            planetManager.setPlanetColors(moodDict);
        }
    }

    protected override IEnumerator GetSubmissionCounts()
    {
        UnityWebRequest dataReq = UnityWebRequest.Get("https://ineffablezoe.wixsite.com/moodetarium/_functions/countSubmissionsByCollege");
        yield return dataReq.SendWebRequest();
        if (dataReq.result == UnityWebRequest.Result.ConnectionError || dataReq.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("ERROR: " + dataReq.error);
        }
        else
        {
            Dictionary<string, float> countDict = DataHandler.parseResponse(dataReq.downloadHandler.text);
            planetManager.setPlanetSizes(countDict);
        }
    }
}
