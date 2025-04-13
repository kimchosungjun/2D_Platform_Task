using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HitEffect : MonoBehaviour
{
    [Header("Must Set")]
    [SerializeField, Range(0,5f)] float effectTime = 0.5f;
    [SerializeField] Transform scaleTransform;
    [SerializeField] Material material;

    bool isScaleEffect = true;
    bool isHitEffect = false;
    Material currentMat = null;
    SpriteRenderer[] sprites;
    WaitForFixedUpdate fixedTime = null;
    Vector3[] scaleEffects;
    Color color = Color.white;

    public void Init(SpriteRenderer[] _sprites, bool _isScaleEffect = true)
    {
        if(_sprites==null)
            sprites = GetComponentsInChildren<SpriteRenderer>();
        else
            sprites = _sprites;

        isScaleEffect = _isScaleEffect;
        fixedTime = new WaitForFixedUpdate();

        Material mat = Instantiate(material);
        currentMat = mat;
        int spriteCnt = sprites.Length;
        for (int i = 0; i < spriteCnt; i++)
        {
            sprites[i].material = mat;
        }

        if (_isScaleEffect)
        {
            scaleEffects = new Vector3[4] {new Vector3(1,1.1f,1), new Vector3(1.1f, 0.9f, 1), new Vector3(0.9f, 1.1f, 1), new Vector3(1, 1, 1)};
        }
    }

    public void DoHitEffect(UnityAction _action = null)
    {
        if (isHitEffect) return;
        StartCoroutine(CDoHitEffect(_action));
    }

    IEnumerator CDoHitEffect(UnityAction _action = null)
    {
        isHitEffect = true;
        float colorTime = 0f;
        float dividColorTime = effectTime / 2;

        float scaleTime = 0;
        float dividScaleTime = effectTime / 4;

        int spriteCnt = sprites.Length;
        int scaleIndex = 0;

        Vector3 startScale = scaleTransform.localScale;
        bool isHalf = false;

        while (colorTime < effectTime)
        {
            colorTime += Time.fixedDeltaTime;
            scaleTime += Time.fixedDeltaTime;
            // Scale
            if (isScaleEffect)
            {
                if(scaleTime >= dividScaleTime && scaleIndex < 3)
                {
                    scaleIndex += 1;
                    scaleTime = 0;
                    startScale = scaleTransform.localScale;
                }
                scaleTransform.localScale = Vector3.Lerp(startScale, scaleEffects[scaleIndex], scaleTime/dividScaleTime);
            }

            if(isHalf == false && colorTime > dividColorTime)
                isHalf = true;

            // Color
            if (isHalf)
                currentMat.SetFloat("_FlashAmount", Mathf.Lerp(1,0,colorTime/ effectTime));
            else
                currentMat.SetFloat("_FlashAmount", Mathf.Lerp(0,1,colorTime/dividColorTime));
                
            yield return fixedTime;
        }

        currentMat.SetFloat("_FlashAmount", 0);
        scaleTransform.localScale = new Vector3(1,1,1);
        isHitEffect = false;

        if (_action != null)
            _action();
    }
}
