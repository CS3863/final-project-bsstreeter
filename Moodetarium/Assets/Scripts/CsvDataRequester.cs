using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvDataRequester : AbstractDataRequester
{
    private PlanetManager planetManager;
    private List<Dictionary<string, object>> data;
    private int i = 0;

    private void Awake()
    {
        data = CSVReader.Read("udata");
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
        foreach (KeyValuePair<string, object> item in data)
        {
            currCollege = System.Convert.ToString(item["college"]);
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
        /* TODO: implement */
        yield break;
    }
}
