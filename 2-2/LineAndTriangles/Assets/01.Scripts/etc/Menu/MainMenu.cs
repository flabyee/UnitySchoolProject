using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void OnOptionPressed()
    {
        OptionMenu.Open();  // == MenuManager.Open(OptionMenu.Instance);
        

        GameManager.Instance.OnOptionLoad();
    }

    public void OnStageSelectPressed()
    {
        StageSelectMenu.Open();
    }

    public void OnTutorialPressed()
    {
        TutorialMenu.Instance.Initialized();

        TutorialMenu.Open();
    }

    public override void OnBackPressed()
    {
        // 메인메뉴에서의 뒤로가기는 나가기
        //base.OnBackPressed();
        Application.Quit();
    }
}
