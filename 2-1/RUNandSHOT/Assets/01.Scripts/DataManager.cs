using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

[SerializeField]
public class SaveData
{
    public int gold = 0;

    public int playerPowerLevel = 1;
    public int playerSpeedLevel = 1;
    public int playerShotSpeedLevel = 1;

    public bool isEasyClear = false;
    public bool isNormalClear = false;
    public bool isHardClear = false;
}
public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(DataManager)) as DataManager;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "PropertyObj";
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<DataManager>();
                }
            }

            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public int score;
    public int gold;

    public bool isExit;
    public bool isKill;
    public bool isOut;
    public bool isDie;

    public bool isShoting = false;      //플레이어가 공격중인가?
    public bool isKnife = false;
    public int playerPowerLevel;
    public int playerSpeedLevel;
    public int playerShotSpeedLevel;

    public bool isEasy;
    public bool isNormal;
    public bool isHard;

    public bool isEasyClear;
    public bool isNormalClear;
    public bool isHardClear;


    public float spawnDelay = 3f;      //장애물 생성 딜레이
    public float t = 0f;               //장애물 생성 쿨타임?
    public float obstacleSpeed = 10;    //장애물 속도


    private void Start()
    {
        playerPowerLevel = 1;
        playerSpeedLevel = 1;
        playerShotSpeedLevel = 1;

        isEasy = false;
        isNormal = false;
        isHard = false;

        LoadData();
    }

    public void PowerUp()
    {
        playerPowerLevel++;
    }
    public void SpeedUp()
    {
        playerSpeedLevel++;
    }
    public void ShotSpeedLevelUp()
    {
        playerShotSpeedLevel++;
    }

    public void AddGold(int _gold)
    {
        gold += _gold;
    }
    public void SubGold(int _gold)
    {
        gold -= _gold;
    }


    public void AddScore(int _score)
    {
        score += _score;
    }
    public void ResetScore()
    {
        score = 0;

        isExit = false;
        isKill = false;
        isOut = false;
        isDie = false;
    }


    public void SaveData()
    {
        SaveData saveData = new SaveData();

        saveData.gold = gold;
        saveData.playerPowerLevel = playerPowerLevel;
        saveData.playerSpeedLevel = playerSpeedLevel;
        saveData.playerShotSpeedLevel = playerShotSpeedLevel;
        saveData.isEasyClear = isEasyClear;
        saveData.isNormalClear = isNormalClear;
        saveData.isHardClear = isHardClear;

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", JsonUtility.ToJson(saveData));
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"));

            gold = saveData.gold;
            playerPowerLevel = saveData.playerPowerLevel;
            playerSpeedLevel = saveData.playerSpeedLevel;
            playerShotSpeedLevel = saveData.playerShotSpeedLevel;
            isEasyClear = saveData.isEasyClear;
            isNormalClear = saveData.isNormalClear;
            isHardClear = saveData.isHardClear;
        }
        else
        {
            SaveData();
        }

    }
}
