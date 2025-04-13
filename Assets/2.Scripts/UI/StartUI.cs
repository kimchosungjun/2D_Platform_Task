using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : ManageButtonUI
{
    public override void PressUI()
    {
        GameSystem.Instance.StartGame();
        uiObject.SetActive(false);
    }

    public override void ActiveUI()
    {
        uiObject.SetActive(true);
    }
}
