using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMgr : MonoBehaviour
{
    #region Singleton
    public static MonsterMgr instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    #region Manage Monster kill Log
    [SerializeField] int monsterKillCnt = 0;
    public void UpdatekillLog()
    {
        monsterKillCnt += 1;
        // To Do UI
    }

    public void ClearKillLog()
    {
        monsterKillCnt = 0;
        // To Do UI
    }
    #endregion

    #region Manage Monster Spawn

    #endregion
}
