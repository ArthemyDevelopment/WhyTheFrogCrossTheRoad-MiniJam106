using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    public Sprite[] Medals;
    public Image[] MedalIcons;
    public GameObject VictoryText;
    public GameObject LoseText;
    public GameObject NextLevel;
    public ChangeScene retryScene;
    public ChangeScene nextScene;
    public bool OverrideChangeScene;


    private void OnEnable()
    {
        if (!TilesManager.current.won)
        {
           LoseText.SetActive(true);
           return;
        }
        VictoryText.SetActive(true);
        NextLevel.SetActive(true);
        for (int i = 0; i < TilesManager.current.Score; i++)
        {
            MedalIcons[i].sprite = Medals[i];
        }

        if (OverrideChangeScene)
            return;
        retryScene.GoToScene = TilesManager.current.thisLevel;
        nextScene.GoToScene = TilesManager.current.thisLevel+1;

    }
}
