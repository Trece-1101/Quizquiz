using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializationOption
{
    string ContenType { get; }
    T Deserialize<T>(string text);

}
