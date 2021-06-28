using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject upgradePopup;

    public GameObject explainPopup1;
    public GameObject explainPopup2;
    public GameObject explainPopup3;
    public GameObject explainPopup4;

    public GameObject startPopup;

    public GameObject easyClear;
    public GameObject normalClear;
    public GameObject hardClear;

    public Text t_Gold;

    public Text t_UpgradePowerCost;
    public Text t_PowerChangeValue;

    public Text t_UpgradeSpeedCost;
    public Text t_SpeedChangeValue;

    public Text t_UpgradeShotSpeedCost;
    public Text t_ShotSpeedChangeValue;



    private void Start()
    {
        upgradePopup.SetActive(false);

        explainPopup1.SetActive(false);
        explainPopup2.SetActive(false);
        explainPopup3.SetActive(false);
        explainPopup4.SetActive(false);

        startPopup.SetActive(false);

        RenewalUI();
    }

    public void RenewalUI()
    {
        t_Gold.text = DataManager.instance.gold.ToString();

        if(DataManager.instance.playerPowerLevel == 30)
        {
            t_UpgradePowerCost.text = "0";
            t_PowerChangeValue.text = string.Format(DataManager.instance.playerPowerLevel.ToString() + "\n" + "бщ" + "\n" + "x");
        }
        else
        {
            t_UpgradePowerCost.text = string.Format(DataManager.instance.playerPowerLevel * 150 + "G");
            t_PowerChangeValue.text = string.Format(DataManager.instance.playerPowerLevel.ToString() + "\n" + "бщ" + "\n" + (DataManager.instance.playerPowerLevel + 1).ToString());
        }

        if(DataManager.instance.playerSpeedLevel == 5)
        {
            t_UpgradeSpeedCost.text = "0";
            t_SpeedChangeValue.text = string.Format(DataManager.instance.playerSpeedLevel.ToString() + "\n" + "бщ" + "\n" + "x");
        }
        else
        {
            t_UpgradeSpeedCost.text = string.Format(DataManager.instance.playerSpeedLevel * 2500 + "G");
            t_SpeedChangeValue.text = string.Format(DataManager.instance.playerSpeedLevel.ToString() + "\n" + "бщ" + "\n" + (DataManager.instance.playerSpeedLevel + 1).ToString());
        }

        if(DataManager.instance.playerShotSpeedLevel == 10)
        {
            t_UpgradeShotSpeedCost.text = "0";
            t_ShotSpeedChangeValue.text = string.Format(DataManager.instance.playerShotSpeedLevel.ToString() + "\n" + "бщ" + "\n" + "x");
        }
        else
        {
            t_UpgradeShotSpeedCost.text = string.Format(DataManager.instance.playerShotSpeedLevel * 2500 + "G");
            t_ShotSpeedChangeValue.text = string.Format(DataManager.instance.playerShotSpeedLevel.ToString() + "\n" + "бщ" + "\n" + (DataManager.instance.playerShotSpeedLevel + 1).ToString());
        }

        if (DataManager.instance.isEasyClear) easyClear.SetActive(true);
        else easyClear.SetActive(false);
        if (DataManager.instance.isNormalClear) normalClear.SetActive(true);
        else normalClear.SetActive(false);
        if (DataManager.instance.isHardClear) hardClear.SetActive(true);
        else hardClear.SetActive(false);

        DataManager.instance.SaveData();
    }

    public void UpgradePopupOpen()
    {
        RenewalUI();
        upgradePopup.SetActive(true);
    }
    public void UpgradePopupClose()
    {
        upgradePopup.SetActive(false);
    }

    public void ExplainPopup1Open()
    {
        explainPopup1.SetActive(true);
    }
    public void ExplainPopup1Right()
    {
        explainPopup1.SetActive(false);
        explainPopup2.SetActive(true);
    }

    public void ExplainPopup2Left()
    {
        explainPopup2.SetActive(false);
        explainPopup1.SetActive(true);
    }
    public void ExplainPopup2Right()
    {
        explainPopup2.SetActive(false);
        explainPopup3.SetActive(true);
    }

    public void ExplainPopup3Left()
    {
        explainPopup3.SetActive(false);
        explainPopup2.SetActive(true);
    }
    public void ExplainPopup3Right()
    {
        explainPopup3.SetActive(false);
        explainPopup4.SetActive(true);
    }

    public void ExplainPopup4Left()
    {
        explainPopup4.SetActive(false);
        explainPopup3.SetActive(true);
    }
    public void ExplainPopup4Close()
    {
        explainPopup4.SetActive(false);
    }

    public void StartPopupOpen()
    {
        startPopup.SetActive(true);
    }
    public void StartPopupClose()
    {
        startPopup.SetActive(false);
    }

}
