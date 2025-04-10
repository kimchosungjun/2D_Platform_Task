using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UtilEnums;

public class SceneMgr 
{
    SceneEnums currentScene = SceneEnums.TitleScene;    

    public void LoadScene(SceneEnums _nextScene)
    {
        if (currentScene == _nextScene) return;

        currentScene = _nextScene;
        SceneManager.LoadScene(Enums.EnumToValue(_nextScene));
    }
}
