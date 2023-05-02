using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketAIController : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) nav.SetDestination(target.transform.position);
    }
}
