using System;
using System.Collections;
using UnityEngine;
using UtilEnums;

public class MonsterMgr : MonoBehaviour
{
    #region Singleton
    public static MonsterMgr Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            oneSec = new WaitForSeconds(1f);
            waitSeqSpawnTerm = new WaitForSeconds(seqSpawnTerm);
            GlobalMgr.MonsterMgr = this;
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }
    #endregion

    #region Manage Monster kill Log
    int monsterKillCnt = 0;
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

    #region Manage Monster Variables
    [Header("Monster Spawn")]
    const int maxAliveMonsterCnt = 20;
    const int respawnMonsterCnt = 15;

    int currentAliveMonsterCnt = 0;
    [SerializeField] float seqSpawnTerm = 0.5f;
    [SerializeField] float[] nextSpawnTerm;

    WaitForSeconds oneSec = null;
    WaitForSeconds waitSeqSpawnTerm = null;
    Coroutine[] spawnCors = new Coroutine[3];
    #endregion

    #region Manage Monster Functions
    public void StartSpawn()
    {
        spawnCors[0] = StartCoroutine(CSpawnMonster(Randoms.GetRandomFloatValue(nextSpawnTerm[0], nextSpawnTerm[1]), LayerEnums.Monster_Line1));
        spawnCors[1] = StartCoroutine(CSpawnMonster(Randoms.GetRandomFloatValue(nextSpawnTerm[0], nextSpawnTerm[1]), LayerEnums.Monster_Line2));
        spawnCors[2] = StartCoroutine(CSpawnMonster(Randoms.GetRandomFloatValue(nextSpawnTerm[0], nextSpawnTerm[1]), LayerEnums.Monster_Line3));
    }

    public void DecreaseMonsterCnt()
    {
        currentAliveMonsterCnt -= 1;

        if (currentAliveMonsterCnt > respawnMonsterCnt)
            return;

        for (int i=0; i<3; i++)
        {
            if (spawnCors[i] != null)
                return;
        }

        Tuple<LayerEnums, int> getRand = Randoms.GetRandomLayer();
        int index = getRand.Item2;
        spawnCors[index-1] = StartCoroutine(CSpawnMonster(Randoms.GetRandomFloatValue(nextSpawnTerm[0], nextSpawnTerm[1]), getRand.Item1));
    }

    IEnumerator CSpawnMonster(float _nextSpawnTime, LayerEnums _monsterLayer)
    {
        int delta = (int)_monsterLayer - 11;
        Vector3 spawnPos = new Vector3(9, -3, delta);
        Quaternion identity = Quaternion.identity;

        yield return new WaitForSeconds(_nextSpawnTime);
        int randNum = Randoms.GetRandomIntValue(1, 4);
        
        for(int i =0; i<randNum; i++)
        {
            currentAliveMonsterCnt += 1;
            GlobalMgr.PoolMgr.GetPool(PoolEnums.Zombie, PoolParentEnums.Monster, spawnPos, identity)?.GetComponent<BaseMonster>().Pooling(_monsterLayer);
            yield return waitSeqSpawnTerm;
        }

        int index = (int)_monsterLayer - 11;
        if (currentAliveMonsterCnt < maxAliveMonsterCnt)
        {
            yield return oneSec;
            spawnCors[index] = StartCoroutine(CSpawnMonster(Randoms.GetRandomFloatValue(nextSpawnTerm[0], nextSpawnTerm[1]), _monsterLayer));
        }
        else
            spawnCors[index] = null;
    }
    #endregion

    #endregion
}
