using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeTextUI : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] float fadeTime = 1f;
    int curIndex = 0;
    string[] infoText = new string[2]
    {
        "타워 데스티니\n 서바이벌",
        "Client Task\n by 조성준"
    };

    public void ShowFade()
    {
        StartCoroutine(CShowFade());
    }

    IEnumerator CShowFade()
    {
        float time = 0;
        Color color =Color.white;
        color.a = 0;
        text.color = color;
        text.text = infoText[curIndex];
        while (time < fadeTime)
        {
            color.a = Mathf.Lerp(0, 1, time / fadeTime);
            text.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        time = 0;
        while (time < fadeTime)
        {
            color.a = Mathf.Lerp(1, 0, time / fadeTime);
            text.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        curIndex += 1;
        color.a = 0;
        text.color = color;
        if (curIndex < infoText.Length)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(CShowFade());
        }
        else
        {
            yield return new WaitForSeconds(3f);
            GlobalMgr.SceneMgr.LoadScene(UtilEnums.SceneEnums.MainScene);
        }
    }
}
