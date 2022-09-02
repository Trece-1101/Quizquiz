using Newtonsoft.Json;
using System;
using UnityEngine;

public class JsonSerializationOption : ISerializationOption
{
    public string ContenType => "application/json";

    public T Deserialize<T>(string text)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(text);
            //Debug.Log($"OK: {text}");
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this} Could not parse response {text}{ex}");
            return default;
        }
    }
}
