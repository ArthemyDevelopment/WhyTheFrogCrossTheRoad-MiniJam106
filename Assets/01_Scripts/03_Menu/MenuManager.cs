using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject G_QuitButton;

    public List<LevelButtonController> allLevels;
    
    
    
    
    private void Awake()
    {
        initDic();
#if UNITY_WEBGL
    G_QuitButton.SetActive(false);
#endif
    }
    
    
    public void ExitGame()
    {
#if UNITY_STANDALONE_WIN
        Application.Quit();
#endif
    }


    void initDic()
    {
        if (LevelsManager.current.Levels.Count == 0)
        {
            foreach (LevelButtonController lv in allLevels)
            {
                LevelsManager.current.Levels.Add(lv.thisLevel.ThisLevel,lv.thisLevel );
            }
        }
    }
    
    
}
