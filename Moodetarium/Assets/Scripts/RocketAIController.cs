using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketAIController : MonoBehaviour
{
    public GameObject followTarget;
    public GameObject orbitTarget;
    private NavMeshAgent nav;

    private float degreesPerSecond = 20;
    private float orbitDistance = 2; 

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget == null) {
            orbit();
        } else {
            // follow the target
            nav.SetDestination(followTarget.transform.position);
            Debug.Log("remaining distance: " + nav.remainingDistance);
            // stop following if rocket is close to planet
            if (nav.remainingDistance <= 1.0f) {
                // stop following
                orbitTarget = followTarget;
                followTarget = null;
                // set orbit distance to our current distance remaining
                // orbitDistance = nav.remainingDistance;
                // disable nav so that we can orbit
                nav.enabled = false;
            }
        }
    }

    void orbit()
     {
         if(orbitTarget != null)
         {
             // Keep us at orbitDistance from target
             transform.position = orbitTarget.transform.position + (transform.position - orbitTarget.transform.position).normalized * orbitDistance;
             transform.RotateAround(orbitTarget.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);
         }
     }

    public void setTarget(GameObject target) {
        this.followTarget = target;
        nav.enabled = true;
    }
}
