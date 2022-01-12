using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json.Linq;

public class Enemy : MonoBehaviour, ISerializable
{
    public int hp = 10;
    public string name = "ΩΩ∂Û¿”";

    void Start()
    {
        FindObjectOfType<SaveManager>().objToSaveList.Add(this);
    }

    public JObject Serialize()
    {
        string jsonString = JsonUtility.ToJson(this);
        JObject returnValue = JObject.Parse(jsonString);

        return returnValue;
    }

    public void Deserialize(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    public string GetJsonKey()
    {
        return this.gameObject.name;
    }
}
