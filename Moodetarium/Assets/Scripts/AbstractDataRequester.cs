using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDataRequester : MonoBehaviour
{
    public abstract void setPlanetManager();

    public abstract void getData();

    protected abstract IEnumerator GetAverageMoods();

    protected abstract IEnumerator GetSubmissionCounts();

    public abstract IEnumerator GetAvailableColleges();
}
