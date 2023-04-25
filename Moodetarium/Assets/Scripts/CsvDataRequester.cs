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

    public override void getData() {
        Debug.Log("retrieving data");
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

        index++;
        
        planetManager.setPlanetColors(moods);
        yield break;
    }   
}
