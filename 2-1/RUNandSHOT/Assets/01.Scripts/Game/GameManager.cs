using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text waitForStartText;

    public GameObject endPopup;
    public Text damageText;
    public Text earnGoldText;


    public GameObject obstaclePrefab = null;
    public GameObject exitDoorPrefab = null;


    public int spawnCount;

    private bool isSpawn;

    void Start()
    {
        DataManager.instance.t = 0;

        StartCoroutine(StartForStart());
        DataManager.instance.isExit = false;
        DataManager.instance.isKill = false;
        DataManager.instance.isOut = false;

        spawnCount = 0;
        isSpawn = true;

        endPopup.SetActive(false);
    }

    void Update()
    {
        if (!DataManager.instance.isShoting)
            DataManager.instance.t += Time.deltaTime;

        if(isSpawn)
        {
            if (DataManager.instance.t > DataManager.instance.spawnDelay)
            {
                if (spawnCount == 15)
                {
                    Instantiate(exitDoorPrefab, new Vector2(-10, -2.5f), Quaternion.identity);
                    isSpawn = false;
                }
                else
                {
                    DataManager.instance.t = 0;
                    Instantiate(obstaclePrefab, new Vector2(-10, -3), Quaternion.Euler(0, 0, 90));
                    spawnCount++;

                }
            }
        }
        
    }

    public void EndPopupOpen()
    {
        Time.timeScale = 0;

        int _gold = DataManager.instance.score;
        if (DataManager.instance.isExit) _gold *= 2;
        if (DataManager.instance.isKill) _gold *= 3;
        if (DataManager.instance.isOut) _gold /= 4;
        if (DataManager.instance.isDie) _gold /= 2;

        if (DataManager.instance.isNormal) _gold *= 2;
        if (DataManager.instance.isHard) _gold *= 4;

        float _damage = DataManager.instance.score;

        damageText.text = string.Format("¿‘»˘ «««ÿ∑Æ : " + _damage.ToString());
        earnGoldText.text = string.Format("»πµÊ«— µ∑ : " + _gold.ToString());

        endPopup.SetActive(true);
    }

    public void GameEnd()
    {
        int _gold = DataManager.instance.score;
        if (DataManager.instance.isExit) _gold *= 2;
        if (DataManager.instance.isKill) _gold *= 3;
        if (DataManager.instance.isOut) _gold /= 4;
        if (DataManager.instance.isDie) _gold /= 2;

        if (DataManager.instance.isNormal) _gold *= 2;
        if (DataManager.instance.isHard) _gold *= 4;

        DataManager.instance.AddGold(_gold);
        DataManager.instance.ResetScore();

        DataManager.instance.SaveData();

        SceneManager.LoadScene("LobyScene");
    }

    IEnumerator StartForStart()
    {
        waitForStartText.gameObject.SetActive(true);
        Time.timeScale = 0;
        waitForStartText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        waitForStartText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        waitForStartText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1 + (DataManager.instance.playerSpeedLevel * 0.1f);
        waitForStartText.text = "START!";
        yield return new WaitForSecondsRealtime(1f);
        waitForStartText.gameObject.SetActive(false);
    }
}
