using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryFormatterDemo : MonoBehaviour
{
    const string saveFileName = "binaryFomatterFile.sav";

    private string name = "������";
    private int level = 99;

    // Ŭ������ �ڵ����� ����ȭ �ǵ��� ���ִ� ����
    [System.Serializable]   // ������ ����
    private class DataContainer
    {
        public string _name;
        public int _level;

        public DataContainer(string name, int level)    // ������ �ʱ�ȭ
        {
            _name = name;
            _level = level;
        }
    }

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

            DataContainer dc = new DataContainer(name, level);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(getFilePath(saveFileName), FileMode.OpenOrCreate);

            bf.Serialize(fs, dc);

            fs.Close();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Load to : " + getFilePath(saveFileName));

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(getFilePath(saveFileName), FileMode.Open);

            DataContainer dc = bf.Deserialize(fs) as DataContainer;

            print("name" + dc._name);
            print("level" + dc._level);

            fs.Close();
        }
    }
}
