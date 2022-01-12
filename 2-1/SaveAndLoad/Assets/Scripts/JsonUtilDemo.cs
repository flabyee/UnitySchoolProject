using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class JsonUtilDemo : MonoBehaviour
{
    const string saveFileName = "jsonUtilFile.sav";

    [SerializeField]    // 저장할수있게해줌
    private string name = "이희주";

    [SerializeField]
    [HideInInspector]   // 저장은 할거지만 인스펙트에서는 표기 x
    private int level = 99;

    [System.NonSerialized]  // 저장할수없게해줌
    public int age = 100;

    public Transform myTrm;

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

            string jsonString = JsonUtility.ToJson(this);

            StreamWriter sw = new StreamWriter(getFilePath(saveFileName));
            sw.WriteLine(jsonString);
            sw.Close();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Load to : " + getFilePath(saveFileName));

            string fileStr = getFilePath(saveFileName);
            if(File.Exists(fileStr))
            {
                StreamReader sr = new StreamReader(fileStr);
                string jsonString = sr.ReadToEnd();
                sr.Close();

                JsonUtility.FromJsonOverwrite(jsonString, this);

                print(jsonString);
            }
            else
            {
                print("파일이 없어요");
            }
        }
    }
}
