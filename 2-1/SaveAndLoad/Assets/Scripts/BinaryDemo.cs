using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class BinaryDemo : MonoBehaviour
{
    const string saveFileName = "binaryFile.sav";

    private string name = "이희주";
    private int level = 99;

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

            FileStream fs = new FileStream(getFilePath(saveFileName), FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(fs); //바이너리화

            bw.Write(name);
            bw.Write(level);

            //무조건 무조건이야~~~ 
            fs.Close();
            bw.Close();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Load to : " + getFilePath(saveFileName));

            FileStream fs = new FileStream(getFilePath(saveFileName), FileMode.Open);
            BinaryReader br = new BinaryReader(fs); //바이너리화

            print(br.ReadString());
            print(br.ReadInt32());

            fs.Close();
            br.Close();
        }
    }
}
