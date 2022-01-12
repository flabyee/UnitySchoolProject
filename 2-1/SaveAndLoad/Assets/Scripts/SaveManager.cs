using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

public class SaveManager : MonoBehaviour
{
    const string saveFileName = "SaveManager";

    // 적 리스트 객체 선언
    public GameObject enemyObj;
    private Enemy[] enemies;

    // 인터페이스를 통한 리스트 선언
    public List<ISerializable> objToSaveList;

    private void Awake()
    {
        //GetEnemyObjs();

        objToSaveList = new List<ISerializable>();
    }

    void Start()
    {
        //string message = "Hello world";
        //byte[] encryptedMessage = Encrypt(message);
        //string decryptedMessage = Decrypt(encryptedMessage);
        //print(decryptedMessage);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 기존 방법
            //JObject jSaveGame = new JObject();
            //for (int i = 0; i < enemies.Length; i++)
            //{
            //    Enemy curEnemy = enemies[i];
            //    JObject jEnemy = curEnemy.Serialize();
            //    jSaveGame.Add(curEnemy.gameObject.name, jEnemy);
            //}

            // 인터페이스 사용
            JObject jSaveGame = new JObject();
            for (int i = 0; i < objToSaveList.Count; i++)
            {
                jSaveGame.Add(objToSaveList[i].GetJsonKey(), objToSaveList[i].Serialize());
            }

            // 파일로 저장
            StreamWriter sw = new StreamWriter(getFilePath(saveFileName));
            print("Save to : " + getFilePath(saveFileName));
            sw.WriteLine(jSaveGame.ToString());
            sw.Close();

            // 암호화
            byte[] encryptedSavegame = Encrypt(jSaveGame.ToString());
            File.WriteAllBytes(getFilePath(saveFileName), encryptedSavegame);

            print(jSaveGame.ToString());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Load to : " + getFilePath(saveFileName));

            string fileStr = getFilePath(saveFileName);
            if (File.Exists(fileStr))
            {
                // 복호화
                byte[] decryptedSavegame = File.ReadAllBytes(getFilePath(saveFileName));
                string jsonString = Decrypt(decryptedSavegame);

                //StreamReader sr = new StreamReader(fileStr);
                //string jsonString = sr.ReadToEnd();
                //sr.Close();

                print(jsonString);

                //// 적 리스트 불러옴 기존 방법
                //JObject jSaveGame = JObject.Parse(jsonString);
                //for (int i = 0; i < enemies.Length; i++)
                //{
                //    Enemy curEnemy = enemies[i];
                //    string enemyString = jSaveGame[curEnemy.gameObject.name].ToString();
                //    curEnemy.Deserialize(enemyString);
                //}


                // 인터페이스 사용
                JObject jSaveGame = JObject.Parse(jsonString);
                for (int i = 0; i < objToSaveList.Count; i++)
                {
                    string objJsonString = jSaveGame[objToSaveList[i].GetJsonKey()].ToString();
                    objToSaveList[i].Deserialize(objJsonString);
                }
            }
            else
            {
                print("파일이 없어요");
                // 새로운 파일 생성
                
            }
        }
    }

    string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    //void GetEnemyObjs()
    //{
    //    if(enemyObj != null)
    //    {
    //        int enemyCount = enemyObj.transform.childCount;
    //        enemies = new Enemy[enemyCount];

    //        for (int i = 0; i < enemyCount; i++)
    //        {
    //            enemies[i] = enemyObj.transform.GetChild(i).GetComponent<Enemy>();
    //        }

    //        print("적 갯수 : " + enemies.Length);
    //    }
    //}

    byte[] _key = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06,
        0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};

    byte[] _initVector = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06,
        0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};

    byte[] Encrypt(string message)
    {
        AesManaged aes = new AesManaged();
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _initVector);

        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        StreamWriter streamWriter = new StreamWriter(cryptoStream);

        streamWriter.WriteLine(message);

        memoryStream.Close();
        cryptoStream.Close();
        streamWriter.Close();

        return memoryStream.ToArray();
    }

    string Decrypt(byte[] message)
    {
        AesManaged aes = new AesManaged();
        ICryptoTransform decryptor = aes.CreateEncryptor(_key, _initVector);

        MemoryStream memoryStream = new MemoryStream(message);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        StreamReader streamReader = new StreamReader(cryptoStream);

        string decryptedMessage = streamReader.ReadToEnd();

        

        memoryStream.Close();
        cryptoStream.Close();
        streamReader.Close();

        return decryptedMessage;
    }
}
