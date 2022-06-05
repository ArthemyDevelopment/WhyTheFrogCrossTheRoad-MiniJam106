using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class LevelsManager : SingletonManager<LevelsManager>
{

    public Dictionary<LevelList, Level> Levels = new Dictionary<LevelList, Level>();

    public Sprite[] Medals;
    private void Awake()
    {
        init();
        DontDestroyOnLoad(this.gameObject);
    }



}

[Serializable]
public class Level
{
    public LevelList[] prevLevels;
    public LevelList ThisLevel;
    public bool B_WasCompleted;
    public int I_Score;
    public bool B_Fastest;


    public void ApplyLevel(Level level)
    {
        B_WasCompleted = level.B_WasCompleted;
        I_Score = level.I_Score;
        B_Fastest = level.B_Fastest;
    }

}