using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDataRequester : MonoBehaviour
{
    protected virtual void Start() {
        StartCoroutine(GetAvailableColleges());
        startGettingData();
    }

    public abstract void setPlanetManager();

    public abstract void startGettingData();

    protected abstract IEnumerator GetAverageMoods();

    protected abstract IEnumerator GetSubmissionCounts();

    public abstract IEnumerator GetAvailableColleges();

    public virtual void FinishedCollectingData() {}
}
