using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Menu : Menu<Stage6Menu>
{
    public void OnStage1Pressed()
    {
        // MainMenu�� ���������� GetcomponentsInChild�� �ȵǼ� ����
        MainMenu.Open();

        GameManager.Instance.OnStage6_1();
    }
    public void OnStage2Pressed()
    {
        // MainMenu�� ���������� GetcomponentsInChild�� �ȵǼ� ����
        MainMenu.Open();

        GameManager.Instance.OnStage6_2();
    }
    public void OnStage3Pressed()
    {
        // MainMenu�� ���������� GetcomponentsInChild�� �ȵǼ� ����
        MainMenu.Open();

        GameManager.Instance.OnStage6_3();
    }
    public void OnStage4Pressed()
    {
        // MainMenu�� ���������� GetcomponentsInChild�� �ȵǼ� ����
        MainMenu.Open();

        GameManager.Instance.OnStage6_4();
    }
    public void OnStage5Pressed()
    {
        // MainMenu�� ���������� GetcomponentsInChild�� �ȵǼ� ����
        MainMenu.Open();

        GameManager.Instance.OnStage6_5();
    }
}
