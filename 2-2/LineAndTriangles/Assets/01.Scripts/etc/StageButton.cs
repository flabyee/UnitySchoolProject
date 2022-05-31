using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public int stageNum;
    public Text numText;
    public Text clearTimeText;
    public Image image;

    [Header("Sprites")]
    public Sprite[] images = new Sprite[4];

    void Start()
    {
        numText.text = stageNum.ToString();
        
        SpriteUpdate();

        GameManager.Instance.endGame += SpriteUpdate;
    }

    // stageNum에 따라 별 갯수를 지정해줌
    private void SpriteUpdate()
    {
        if (GameManager.Instance.stageStarCounts[stageNum - 1] != 0)
        {
            image.sprite = images[GameManager.Instance.stageStarCounts[stageNum - 1]];
        }
    }
}
