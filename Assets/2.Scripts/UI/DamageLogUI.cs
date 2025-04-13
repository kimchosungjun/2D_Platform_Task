using System.Collections;
using TMPro;
using UnityEngine;

public class DamageLogUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI floatText;
    [SerializeField] Animator anim;
    [SerializeField] float floatTime = 1.5f;
    WaitForSeconds waitTime = null;

    void Awake()
    {
        waitTime = new WaitForSeconds(floatTime);
        if(anim == null) anim = GetComponent<Animator>();
        if(canvas ==null) canvas = GetComponentInChildren<Canvas>();
    }

    void Start()
    {
        canvas.worldCamera = Camera.main;    
    }

    public void SetDamage(int _damage)
    {
        if (this.gameObject.activeSelf == false)
            this.gameObject.SetActive(true);

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
