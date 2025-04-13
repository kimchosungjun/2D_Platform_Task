using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageLogUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI floatText;
    [SerializeField] Animator anim;
    [SerializeField] float floatTime = 1.5f;
    WaitForSeconds waitTime = null;

    void Awake()
    {
        waitTime = new WaitForSeconds(floatTime);
        if(anim == null) anim = GetComponent<Animator>();   
    }

    public void SetDamage(int _damage)
    {
        floatText.text = _damage.ToString();
        anim.Play("Effect");
        StartCoroutine(CDisable());
    }

    IEnumerator CDisable()
    {
        yield return waitTime;
        this.gameObject.SetActive(false);
    }
}
