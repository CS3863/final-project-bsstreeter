using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvDataRequester : AbstractDataRequester
{
    private PlanetManager planetManager;
    private List<Dictionary<string, object>> data;

    private int i = 0;
    private Dictionary<string, float> moods;
    private Dictionary<string, float> submissionCounts;

    private string tempString;
    private float tempFloat;

    private void Awake()
    {
        data = CSVReader.Read("testResponses");
        moods = new Dictionary<string, float>();
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

    protected override IEnumerator GetAverageMoods()
    {
        /* TODO: implement */
        yield break;
    }

    protected override IEnumerator GetSubmissionCounts()
    {
        tempString = System.Convert.ToString(data[i]["college"]);

        if (submissionCounts.ContainsKey(tempString)) {
            // increment the count for this college by one
            tempFloat = submissionCounts[tempString] + 1;
            submissionCounts[tempString] = tempFloat;
        } else {
            submissionCounts.Add(tempString, 1);
        }

        i++;
        
        planetManager.setPlanetSizes(submissionCounts);
        yield break;
    }
}
