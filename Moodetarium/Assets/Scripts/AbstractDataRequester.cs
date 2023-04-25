using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDataRequester : MonoBehaviour
{
    public abstract void setPlanetManager();

    public void getData()
    {
        Debug.Log("retrieving data");
        StartCoroutine(GetAverageMoods());
        StartCoroutine(GetSubmissionCounts());
    }

    protected abstract IEnumerator GetAverageMoods();

    protected abstract IEnumerator GetSubmissionCounts();

    public abstract IEnumerator GetAvailableColleges();
}
