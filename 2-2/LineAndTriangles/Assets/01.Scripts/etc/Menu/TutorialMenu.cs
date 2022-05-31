using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : Menu<TutorialMenu>
{
    public List<GameObject> textList;

    public int pageNum;

    // ��� text�� ���� ù��° text�� ����
    public void Initialized()
    {
        textList.FindAll(x => x.activeSelf).ForEach(x => x.SetActive(false));
        pageNum = 0;
        textList[pageNum].SetActive(true);
    }

    // �������� �ѱ��
    public void OnLeft()
    {
        pageNum = Mathf.Clamp(pageNum - 1, 0, textList.Count - 1);

        textList.FindAll(x => x.activeSelf).ForEach(x => x.SetActive(false));

        textList[pageNum].SetActive(true);
    }

    // ���������� �ѱ��
    public void OnRight()
    {
        pageNum = Mathf.Clamp(pageNum + 1, 0, textList.Count - 1);

        textList.FindAll(x => x.activeSelf).ForEach(x => x.SetActive(false));

        textList[pageNum].SetActive(true);
    }
}
