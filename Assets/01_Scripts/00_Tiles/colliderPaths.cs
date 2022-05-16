using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class colliderPaths : MonoBehaviour
{
    public UnityEvent OnEnter;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Car"))
        {
           OnEnter.Invoke();
        }
    }
}
