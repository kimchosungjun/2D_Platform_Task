using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : OperateByScene
{
    [SerializeField] FadeUI fadeUI;
    void Awake()
    {
        Init();   
    }
    public override void Init()
    {
        if(fadeUI==null)
            fadeUI = FindObjectOfType<FadeUI>();    
    }

    void Start()
    {
        Setup();
    }

    public override void Setup()
    {
        fadeUI.Fade(true, () => { fadeUI.gameObject.SetActive(false); });
    }

    IEnumerator StartPool()
    {
        while (true)
        {
            Vector3 Pos = new Vector3(8, -4, 0);
            Transform tf = GlobalMgr.Pool.GetPool(UtilEnums.PoolEnums.Zombie);
            tf.position = Pos;
            if (tf.gameObject.activeSelf == false)
                tf.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
        }
    }
}
