using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvDataRequester : AbstractDataRequester
{
    private PlanetManager planetManager;
    private List<Dictionary<string, object>> data;

    private int index = 0;

    private Dictionary<string, float> moods;
    // store the total mood so we can just divide by the count on the next one
    private Dictionary<string, float> cumulativeMoods;
    private Dictionary<string, float> submissionCounts;

    private string tempString;
    private float tempFloat;

    public float timeConversionFactor = 100000.0f;
    public float maxTimeBetweenChanges = 30.0f;
    public float minTimeBetweenChanges = 1.0f;

    private void Awake()
    {
        data = CSVReader.Read("testResponses");
        moods = new Dictionary<string, float>();
        cumulativeMoods = new Dictionary<string, float>();
        submissionCounts = new Dictionary<string, float>();
    }

    public override void setPlanetManager()
    {
        planetManager = GameObject.Find("Planets").GetComponent<PlanetManager>();
    }

    public override IEnumerator GetAvailableColleges()
    {
        Debug.Log("Getting available colleges");

        List<string> colleges = new List<string>();
        string currCollege = "";
        
        // horribly inefficient way to do this, fix if we have time
        foreach (Dictionary<string, object> row in data)
        {
            currCollege = System.Convert.ToString(row["college"]);
            if (!colleges.Contains(currCollege)) {
                colleges.Add(currCollege);
            }
        }
        planetManager.createPlanets(colleges);
        yield break;
    }

    public override void startGettingData() {
        StartCoroutine(getData());
    }

    private IEnumerator getData() {
        while (index < data.Count - 1)
        {
            Debug.Log("retrieving data row " + index);
            // get average moods, it calls getsubmissioncounts on its own
            StartCoroutine(GetAverageMoods());
            // yield the amount of time between the current point and the next point
            yield return new WaitForSeconds(timeToNextRow());
            // move to next row
            index++;
        }
        Debug.Log("retrieving data row " + index);
        // get average moods, it calls getsubmissioncounts on its own
        StartCoroutine(GetAverageMoods());
    }

    protected override IEnumerator GetSubmissionCounts()
    {
        tempString = System.Convert.ToString(data[index]["college"]);

        if (submissionCounts.ContainsKey(tempString)) {
            // increment the count for this college by one
            tempFloat = submissionCounts[tempString] + 1;
            submissionCounts[tempString] = tempFloat;
        } else {
            submissionCounts.Add(tempString, 1);
        }
        planetManager.setPlanetSizes(submissionCounts);
        yield break;
    }

    protected override IEnumerator GetAverageMoods()
    {
        // wait for the counts to happen so we can get a correct average
        yield return StartCoroutine(GetSubmissionCounts());

        tempString = System.Convert.ToString(data[index]["college"]);
        tempFloat = System.Convert.ToSingle(data[index]["mood"]);

        
        if (cumulativeMoods.ContainsKey(tempString)) {
            cumulativeMoods[tempString] += tempFloat;
            moods[tempString] = cumulativeMoods[tempString] / submissionCounts[tempString];
        } else {
            cumulativeMoods.Add(tempString, tempFloat);
            moods.Add(tempString, tempFloat);
        }
        
        planetManager.setPlanetColors(moods);
        yield break;
    }

    private float timeToNextRow() {
        // read the datetimes
        tempString = System.Convert.ToString(data[index]["time"]);
        DateTime currDateTime = DateTime.ParseExact(tempString, "yyyy-MM-dTHH:mm:ssZ",
                                       System.Globalization.CultureInfo.InvariantCulture);
        tempString = System.Convert.ToString(data[index + 1]["time"]);
        DateTime nextDateTime = DateTime.ParseExact(tempString, "yyyy-MM-dTHH:mm:ssZ",
                                       System.Globalization.CultureInfo.InvariantCulture);
        // find elapsed time
        TimeSpan elapsed = nextDateTime - currDateTime;
        // convert to a scaled down wait time 
        float waitTime = (float)(elapsed.TotalSeconds / timeConversionFactor);
        // make sure the wait time isn't zero
        waitTime = Math.Max(minTimeBetweenChanges, waitTime);
        // make sure the wait time isn't too long
        waitTime = Math.Min(maxTimeBetweenChanges, waitTime);

        Debug.Log("waiting " + waitTime + " seconds");
        return waitTime;
    }
}
