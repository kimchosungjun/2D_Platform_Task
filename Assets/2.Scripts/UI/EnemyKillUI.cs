using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKillUI : MonoBehaviour
{
    [SerializeField] Text enemyKillCnt;
    [SerializeField] GameObject killObject;

    public void StartGame()
    {
        enemyKillCnt.text = "0";
        killObject.SetActive(true);
    }

    public void EndGame()
    {
        enemyKillCnt.text= "0";
        killObject.SetActive(false);
    }

    public void UpdateKillCnt(int _cnt)
    {
        enemyKillCnt.text = _cnt.ToString();
    }
}
