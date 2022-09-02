using System;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient
{
    private readonly ISerializationOption serialization;
    public HttpClient(ISerializationOption serializationOption)
    {
        serialization = serializationOption;
    }
    public async Task<TResultType> Get<TResultType>(string url)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);

            www.SetRequestHeader("Content-Type", serialization.ContenType);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var jsonResponse = www.downloadHandler.text;

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{www.error}");
            }
  
            var result = serialization.Deserialize<TResultType>(jsonResponse);
            
            return result;

        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
            return default;
        }


        
    }
}
