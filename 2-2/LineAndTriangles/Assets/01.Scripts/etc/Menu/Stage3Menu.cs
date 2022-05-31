using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Menu : Menu<Stage3Menu>
{
    public void OnStage1Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage3_1();
    }
    public void OnStage2Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage3_2();
    }
    public void OnStage3Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage3_3();
    }
    public void OnStage4Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage3_4();
    }
    public void OnStage5Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage3_5();
    }
}
