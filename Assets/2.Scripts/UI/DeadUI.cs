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
        text.text = $"총 {_cnt}마리 처치했습니다.";
        ActiveUI();
    }
}
