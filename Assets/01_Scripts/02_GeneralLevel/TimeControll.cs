using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeControll : MonoBehaviour
{
    public int Time;
    private int actTime;
    public TMP_Text timeText;


    private void Start()
    {
        actTime = Time;
        UpdateText();
    }


    public void StartTimer()
    {

            StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        while (actTime >= 0 && TilesManager.current.actState == GameStates.frogPath)
        {
            yield return ScriptsTools.GetWait(TilesManager.current.timeScale);
            actTime--;
            UpdateText();

            if (actTime<0 )
            {  
                StartCoroutine(TilesManager.current.EndGame());
                timeText.text = "0";

            }
        }
    }

    private void UpdateText()
    {
        timeText.text = actTime.ToString();
    }
}
