using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataHandler : MonoBehaviour
{
    public static Dictionary<string, float> parseResponse(string jsonString) {
        JObject jsonContents = JsonConvert.DeserializeObject<JObject>(jsonString);
        Dictionary<string, float> dict = jsonContents.ToObject<Dictionary<string, float>>();
        return dict;
    }
}
