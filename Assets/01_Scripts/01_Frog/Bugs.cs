using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TilesManager.current.AddScore();
            Destroy(this.gameObject);
        }
    }
}
