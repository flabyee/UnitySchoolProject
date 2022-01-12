using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json.Linq;

public class JsonDemo : MonoBehaviour
{
    const string saveFileName = "jsonObjFile.sav";

    private string name = "������";
    private int level = 99;

    public string[] friends;

    string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("Save to : " + getFilePath(saveFileName));

            // JSON ������ ����(Ʈ��)
            JObject jObj = new JObject();
            jObj.Add("ComponentName", GetType().ToString());

            JObject jDataObject = new JObject();
            jObj.Add("Data", jDataObject);

            jDataObject.Add("Name", name);
            jDataObject.Add("Level", level);

            JArray jFriendsArray = JArray.FromObject(friends);
            jDataObject.Add("Friends", jFriendsArray);

            // ������ ����
            StreamWriter sw = new StreamWriter(getFilePath(saveFileName));
            sw.WriteLine(jObj.ToString());
            sw.Close();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Load to : " + getFilePath(saveFileName));

            StreamReader sr = new StreamReader(getFilePath(saveFileName));
            string jsonString = sr.ReadToEnd();
            sr.Close();

            print(jsonString);

            // �о�帰 ��Ʈ�� �����͸� JObject�� ���� �簡��
            JObject jObj = JObject.Parse(jsonString);

            name = jObj["Data"]["Name"].Value<string>();
            level = jObj["Data"]["Level"].Value<int>();

            friends = jObj["Data"]["Friends"].ToObject<string[]>();

        }
    }
}
