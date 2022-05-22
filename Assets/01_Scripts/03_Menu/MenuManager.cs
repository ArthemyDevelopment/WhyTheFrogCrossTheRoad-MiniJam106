using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject G_QuitButton;
    private void Awake()
    {
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
}
