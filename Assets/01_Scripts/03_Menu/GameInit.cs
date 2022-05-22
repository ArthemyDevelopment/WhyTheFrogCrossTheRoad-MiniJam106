using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInit : MonoBehaviour
{
    public UnityEvent OnGameInit;

    private void Start()
    {
        OnGameInit.Invoke();
    }
}
