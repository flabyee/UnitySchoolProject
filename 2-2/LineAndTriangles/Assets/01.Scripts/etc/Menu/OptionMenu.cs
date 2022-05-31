using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : Menu<OptionMenu>
{
    // optionMenu ���� option ����
    public override void OnBackPressed()
    {
        base.OnBackPressed();

        GameManager.Instance.OnOptionSave();
    }
}
