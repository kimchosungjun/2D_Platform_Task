using UnityEngine;
using UnityEngine.UI;

public class DeadUI : ManageButtonUI
{
    [SerializeField] Text text;
    public override void PressUI()
    {
        GameSystem.Instance.ReadyGame();
        uiObject.SetActive(false);   
    }

    public override void ActiveUI()
    {
        uiObject.SetActive(true);
    }

    public void ActiveDeadUI(int _cnt)
    {
        text.text = $"�� {_cnt}���� óġ�߽��ϴ�.";
        ActiveUI();
    }
}
