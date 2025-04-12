using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeTime = 3f;
    Color color;
    public void Fade(bool _isFadeIn, UnityAction _action = null)
    {
        color = fadeImage.color;
        if (_isFadeIn)
        {
            StartCoroutine(CDoFade(1,0, _action));
        }
        else
        {
            StartCoroutine(CDoFade(0,1, _action));
        }
    }


    IEnumerator CDoFade(float _start, float _end, UnityAction _action = null)
    {
        float time = 0f;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(_start, _end , time/ fadeTime);
            fadeImage.color = color;
            yield return null;  
        }

        color.a = _end;
        fadeImage.color = color;

        if(_action!=null)
            _action();
    }
}
