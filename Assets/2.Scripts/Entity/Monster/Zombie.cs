using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : BaseMonster
{
    #region Unity Life Cycle

    void Awake()
    {
        Init();   
    }

    void Start()
    {
        Setup();
    }

    void FixedUpdate()
    {
        
    }
    #endregion

}
