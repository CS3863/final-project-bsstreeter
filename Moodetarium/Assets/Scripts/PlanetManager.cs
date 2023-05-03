using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    // this is icky, find a nicer way to do it if at all possible
    public string[] planetNames;
    public GameObject[] planetPrefabs;
    private Dictionary<string, PlanetAppearanceController> planetControllers = new Dictionary<string, PlanetAppearanceController>();

    public CameraController camController;

    public void createPlanets(List<string> names) {
        float dist = 3.0f;
        List<float> angles = new List<float>() { 0, 270, 90, 180 };

        Debug.Log(names.Count + " names");
        for (int i = 0;i < names.Count; i++) {
            // create a child element of the gameobject the script is attached to and get the needed references
            GameObject newPlanet = Instantiate(planetPrefabs[Array.IndexOf(planetNames, names[i])]) as GameObject;
            newPlanet.transform.parent = gameObject.transform;

            // move the planet to a random point in the orbit around the sun at its set distance
            float angle = angles[i] * Mathf.Deg2Rad;
            newPlanet.transform.position = new Vector3( Mathf.Cos(angle) * dist , 0, Mathf.Sin(angle) * dist );
            
            // add new planet to the dictionary to modify its look later
            PlanetAppearanceController newObjController = newPlanet.GetComponent<PlanetAppearanceController>();
            planetControllers.Add(names[i], newObjController);
        }
        Debug.Log("Created planets: " + string.Join(",", names));
    }

    public void setPlanetColors(Dictionary<string, float> moods) {
        foreach (KeyValuePair<string, PlanetAppearanceController> kvp in planetControllers)
        {
            // try catch handles if a key wasn't provided for one of the planets
            try {
                Color mappedColor = convertColor(moods[kvp.Key]);
                kvp.Value.setColor(mappedColor);
            } catch (KeyNotFoundException e) {
                kvp.Value.setColor(Color.gray);
            }
        }
    }

    public void setPlanetSizes(Dictionary<string, float> counts) {
        // sum the values of the counts dictionary
        int totalSubmissions = countTotalSubmissions(counts);

        foreach (KeyValuePair<string, PlanetAppearanceController> kvp in planetControllers)
        {
            // try catch handles if a key wasn't provided for one of the planets
            try {
                float prevScale = kvp.Value.getScale();
                // normalize the scale
                float scaleValue = counts[kvp.Key] / totalSubmissions;
                kvp.Value.setSize(scaleValue);

                if (prevScale == 0 && scaleValue > 0)
                {
                    GameObject target = kvp.Value.getObject();
                    camController.target = target;
                    // StartCoroutine(lookForTime(target, 25));
                }
            } catch (KeyNotFoundException e) {
                kvp.Value.setSize(0);
            }
        }
    }

    int countTotalSubmissions(Dictionary<string, float> counts) {
        return counts.Sum(x => (int) x.Value);
    }

    Color convertColor(float num) {
        // this implementation isn't great, but it just sorts the mood number into a color
        if (num < 1) {
            return Color.grey;
        } else if (num < 2) {
            return new Color(0.07f, 0.23f, 0.54f, 1.0f);
        } else if (num < 3) {
            return new Color(0.40f, 0.07f, 0.66f, 1.0f);
        } else if (num < 4) {
            return new Color(0.94f, 0.00f, 0.30f, 1.0f);
        } else {
            return new Color(0.94f, 0.73f, 0.00f, 1.0f);
        }
    }

    /*IEnumerator lookForTime(GameObject target, float waitTime)
    {
        camController.target = target;
        yield return new WaitForSeconds(waitTime);
        camController.target = null;
        yield break;
    }*/
}
