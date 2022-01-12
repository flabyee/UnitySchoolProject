using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class StringDemo : MonoBehaviour
{
    const string saveFileName = "stringFile.sav";

    private string name = "¿Ã»Ò¡÷";
    private int level = 99;

    string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            print("Save to : " + getFilePath(saveFileName));


            StreamWriter sw = new StreamWriter(getFilePath(saveFileName));
            sw.WriteLine(name);
            sw.WriteLine(level);

            //ª©∏‘¿∏∏È æ»µ 
            sw.Close();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Load to : " + getFilePath(saveFileName));

            StreamReader sr = new StreamReader(getFilePath(saveFileName));
            print(sr.ReadLine());
            print(sr.ReadLine());

            sr.Close();
        }
    }
}
