using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAppearanceController : MonoBehaviour
{
    private Renderer planetRenderer;
    private float originalScale = 2.0f;

    private float changeSpeed = 1.5f;

    void Start() {
        planetRenderer = GetComponent<Renderer>();
    }

    public void setColor(Color color) {
    //    planetRenderer.material.SetColor("_BaseColor", color);
        StartCoroutine(changeColorGradual(color));
    }

    private IEnumerator changeColorGradual(Color endColor)
    {
        Color startColor = planetRenderer.material.color;
        float tick = 0f;
        while (planetRenderer.material.color != endColor)
        {
            tick += Time.deltaTime * changeSpeed;
            planetRenderer.material.SetColor("_BaseColor", Color.Lerp(startColor, endColor, tick) );
            yield return null;
        }
        yield break;
    }

    public float getScale()
    {
        return transform.localScale.x;
    }

    public void setSize(float scaleValue) {
        scaleValue = originalScale * scaleValue;
        // gameObject.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        StartCoroutine(changeSizeGradual(new Vector3(scaleValue, scaleValue, scaleValue)));
    }

    private IEnumerator changeSizeGradual(Vector3 endScale)
    {
        Vector3 startScale = gameObject.transform.localScale;
        float tick = 0f;
        while (gameObject.transform.localScale != endScale)
        {
            tick += Time.deltaTime * changeSpeed;
            gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, tick);
            yield return null;
        }
        yield break;
    }  

    public GameObject getObject()
    {
        return gameObject;
    }
}
