using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectMenu : Menu<StageSelectMenu>
{
    public void OnStage1Pressed()
    {
        Stage1Menu.Open();
    }
    public void OnStage2Pressed()
    {
        Stage2Menu.Open();
    }
    public void OnStage3Pressed()
    {
        Stage3Menu.Open();
    }
    public void OnStage4Pressed()
    {
        Stage4Menu.Open();
    }
    public void OnStage5Pressed()
    {
        Stage5Menu.Open();
    }
    public void OnStage6Pressed()
    {
        Stage6Menu.Open();
    }
    
}
