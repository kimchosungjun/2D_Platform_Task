using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUI : ManageButtonUI
{
    public override void PressUI()
    {
        // To Do ~~ Game System
        uiObject.SetActive(false);
    }

    public override void ActiveUI()
    {
        // To Do ~~ Game System
        uiObject.SetActive(true);
    }
}
