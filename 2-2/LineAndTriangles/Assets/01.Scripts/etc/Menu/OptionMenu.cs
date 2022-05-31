using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : Menu<OptionMenu>
{
    // optionMenu ²ø¶§ option Àû¿ë
    public override void OnBackPressed()
    {
        base.OnBackPressed();

        GameManager.Instance.OnOptionSave();
    }
}
