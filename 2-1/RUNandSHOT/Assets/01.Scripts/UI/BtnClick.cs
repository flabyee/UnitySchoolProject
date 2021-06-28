using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnClick : MonoBehaviour
{
    private UIManager ui;
    private GameManager gameManager;

    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BtnClickMainStartBtn()
    {
        SceneManager.LoadScene("LobyScene");
    }

    public void BtnClickStartPopupOpen()
    {
        ui.StartPopupOpen();
    }
    public void BtnClickStartPopupClose()
    {
        ui.StartPopupClose();
    }

    public void BtnClickEasy()
    {
        DataManager.instance.isEasy = true;
        DataManager.instance.isNormal = false;
        DataManager.instance.isHard = false;

        SceneManager.LoadScene("PlayScene");
    }
    public void BtnClickNormal()
    {
        if(DataManager.instance.gold >= 5000)
        {
            DataManager.instance.SubGold(5000);

            DataManager.instance.isEasy = false;
            DataManager.instance.isNormal = true;
            DataManager.instance.isHard = false;

            SceneManager.LoadScene("PlayScene");
        }
    }
    public void BtnClickHard()
    {
        if(DataManager.instance.gold > 20000)
        {
            DataManager.instance.SubGold(20000);

            DataManager.instance.isEasy = false;
            DataManager.instance.isNormal = false;
            DataManager.instance.isHard = true;

            SceneManager.LoadScene("PlayScene");
        }
    }
    
    public void BtnClickPowerUp()
    {
        if(DataManager.instance.playerPowerLevel * 150 <= DataManager.instance.gold && DataManager.instance.playerPowerLevel < 30)
        {
            DataManager.instance.SubGold(DataManager.instance.playerPowerLevel * 150);
            DataManager.instance.PowerUp();

            ui.RenewalUI();
        }
        
    }
    public void BtnClickSpeedUp()
    {
        if ((DataManager.instance.playerSpeedLevel * 2500 <= DataManager.instance.gold) && DataManager.instance.playerSpeedLevel < 5)
        {
            DataManager.instance.SubGold(DataManager.instance.playerSpeedLevel * 2500);
            DataManager.instance.SpeedUp();

            ui.RenewalUI();
        }
    }

    public void BtnClickShotSpeedUp()
    {
        if ((DataManager.instance.playerShotSpeedLevel * 2500 <= DataManager.instance.gold) && DataManager.instance.playerShotSpeedLevel < 10)
        {
            DataManager.instance.SubGold(DataManager.instance.playerShotSpeedLevel * 2500);
            DataManager.instance.ShotSpeedLevelUp();

            ui.RenewalUI();
        }
    }

    public void BtnClickUpgradePopupOpen()
    {
        ui.UpgradePopupOpen();
    }
    public void BtnClickUpgradePopupClose()
    {
        ui.UpgradePopupClose();
    }

    public void BtnClickExplainPopup1Open()
    {
        ui.ExplainPopup1Open();
    }
    public void BtnClickExplainPopup1Right()
    {
        ui.ExplainPopup1Right();
    }

    public void BtnClickExplainPopup2Left()
    {
        ui.ExplainPopup2Left();
    }
    public void BtnClickExplainPopup2Right()
    {
        ui.ExplainPopup2Right();
    }

    public void BtnClickExplainPopup3Left()
    {
        ui.ExplainPopup3Left();
    }
    public void BtnClickExplainPopup3Right()
    {
        ui.ExplainPopup3Right();
    }

    public void BtnClickExplainPopup4Left()
    {
        ui.ExplainPopup4Left();
    }
    public void BtnClickExplainPopup4Close()
    {
        ui.ExplainPopup4Close();
    }

    public void BtnClickGoLoby()
    {
        gameManager.GameEnd();
        Time.timeScale = 1;
        SceneManager.LoadScene("LobyScene");
    }
}
