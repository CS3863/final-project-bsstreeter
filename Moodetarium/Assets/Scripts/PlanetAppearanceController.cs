using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAppearanceController : MonoBehaviour
{
    private Renderer planetRenderer;
    private float originalScale = 2.0f;

    void Start() {
        planetRenderer = GetComponent<Renderer>();
    }

    public void setColor(Color color) {
       planetRenderer.material.SetColor("_BaseColor", color);
    }

    public void setSize(float scaleValue) {
        scaleValue = originalScale * scaleValue;
        gameObject.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
