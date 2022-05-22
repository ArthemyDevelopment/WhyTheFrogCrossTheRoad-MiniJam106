using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    public Level thisLevel;

    public Button thisButton;
    public Image Medal;

    private void Awake()
    {
        if (!LevelsManager.current.Levels.ContainsKey(thisLevel.ThisLevel))
        {
            LevelsManager.current.Levels.Add(thisLevel.ThisLevel, thisLevel);
        }
    }



    private void Start()
    {
        int temp = 0;
        foreach (var level in thisLevel.prevLevels)
        {
            if (LevelsManager.current.Levels[level].B_WasCompleted)
                temp++;
        }

        if (temp == thisLevel.prevLevels.Length)
            thisButton.interactable = true;
        else
            thisButton.interactable = false;

        thisLevel.ApplyLevel(LevelsManager.current.Levels[thisLevel.ThisLevel]);
        Medal.sprite = LevelsManager.current.Medals[thisLevel.I_Score];
    }
}
