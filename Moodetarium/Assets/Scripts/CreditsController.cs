using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    private Animator animController;
    void Awake()
    {
        animController = GetComponent<Animator>();
        animController.enabled = false;     
    }

    public void startCredits() {
        Debug.Log("starting credits");
        animController.enabled = true;
    }
}
