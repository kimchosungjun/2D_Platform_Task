using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPIndicator : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image fillImage;

    int currentHp = 0;
    int maxHp = 0;

    void Start()
    {
        if (canvas != null)
            canvas.worldCamera = Camera.main;
    }

    public void SetHP(int _hp)
    {
        maxHp = _hp;
        currentHp = _hp;

        fillImage.fillAmount = 1;

        if(this.gameObject.activeSelf == false)
            this.gameObject.SetActive(true);
    }

    public void DecreaseHP(int _decreaseValue)
    {
        currentHp-= _decreaseValue;
        fillImage.fillAmount = ((float)currentHp / maxHp);

        if (currentHp <= 0)
            this.gameObject.SetActive(false);
    }
}
