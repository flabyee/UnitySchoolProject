using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Menu : Menu<Stage5Menu>
{
    public void OnStage1Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage5_1();
    }
    public void OnStage2Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage5_2();
    }
    public void OnStage3Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage5_3();
    }
    public void OnStage4Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage5_4();
    }
    public void OnStage5Pressed()
    {
        // MainMenu°¡ ´ÝÇôÀÖÀ¸¸é GetcomponentsInChild°¡ ¾ÈµÇ¼­ ÄÑÁÜ
        MainMenu.Open();

        GameManager.Instance.OnStage5_5();
    }
}
