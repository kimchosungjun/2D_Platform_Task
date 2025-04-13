using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPIndicator : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image fillImage;
    [SerializeField] float showTime = 1f;

    int currentHp = 0;
    int maxHp = 0;

    bool isActive = false;

    void Start()
    {
        if (canvas != null)
            canvas.worldCamera = Camera.main;

        DecideActiveState(false);
    }

    public void SetHP(int _hp)
    {
        maxHp = _hp;
        currentHp = _hp;

        fillImage.fillAmount = 1;

        isActive = false;
        if (canvas.gameObject.activeSelf)
            DecideActiveState(false);
    }

    public void DecreaseHP(int _decreaseValue)
    {
        currentHp-= _decreaseValue;
        fillImage.fillAmount = ((float)currentHp / maxHp);
        
        if (currentHp <= 0)
        {
            StopAllCoroutines();
            DecideActiveState(false);
            return;
        }

        if(isActive == false)
            StartCoroutine(CShowHP());
    }

    public void DecideActiveState(bool _isActive)
    {
        isActive = _isActive;
        canvas.gameObject.SetActive(_isActive);
    }

    IEnumerator CShowHP()
    {
        float time = 0;
        DecideActiveState(true);
        while(time< showTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        DecideActiveState(false);
    }
}
