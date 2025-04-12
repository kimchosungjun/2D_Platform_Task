using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManageButtonUI : MonoBehaviour
{
    [SerializeField] protected GameObject uiObject;

    public abstract void PressUI();
    public abstract void ActiveUI();
}
