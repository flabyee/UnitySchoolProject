using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : Menu<TutorialMenu>
{
    public List<GameObject> textList;

    public int pageNum;

    // 모든 text를 끄고 첫번째 text만 켜줌
    public void Initialized()
    {
        textList.FindAll(x => x.activeSelf).ForEach(x => x.SetActive(false));
        pageNum = 0;
        textList[pageNum].SetActive(true);
    }

    // 왼쪽으로 넘기기
    public void OnLeft()
    {
        pageNum = Mathf.Clamp(pageNum - 1, 0, textList.Count - 1);

        textList.FindAll(x => x.activeSelf).ForEach(x => x.SetActive(false));

        textList[pageNum].SetActive(true);
    }

    // 오른쪽으로 넘기기
    public void OnRight()
    {
        pageNum = Mathf.Clamp(pageNum + 1, 0, textList.Count - 1);

        textList.FindAll(x => x.activeSelf).ForEach(x => x.SetActive(false));

        textList[pageNum].SetActive(true);
    }
}
