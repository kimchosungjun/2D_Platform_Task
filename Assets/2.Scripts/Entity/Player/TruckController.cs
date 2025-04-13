using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    const int MaxBoxCnt = 5;
    [Header("Low Index = Bottom")]
    [SerializeField] List<Hero> heroList = new List<Hero>();
    [SerializeField] GameObject truckObject;
    Vector3[] initPositions;

    int heroBoxCnt = -1;

    void Awake()
    {
        Init();
    }


    void Init()
    {
        heroBoxCnt = heroList.Count;
        initPositions  = new Vector3[heroBoxCnt];
        for (int i=0; i< heroBoxCnt; i++)
        {
            heroList[i].InitByTruck(this, i);
            initPositions[i] = heroList[i].transform.position;
        }
    }

    public void AnnounceBreakBox(int _index)
    {
        heroList[_index].gameObject.SetActive(false);
    }

    public void AnnounceBreakTruck() 
    {
        truckObject.SetActive(false); 
    }

    public void ResetHeros()
    {
        truckObject.SetActive(true);
        for (int i = 0; i < heroBoxCnt; i++)
        {
            heroList[i].transform.position = initPositions[i];
            heroList[i].gameObject.SetActive(true);
            heroList[i].ResetHero();
        }
    }
}
