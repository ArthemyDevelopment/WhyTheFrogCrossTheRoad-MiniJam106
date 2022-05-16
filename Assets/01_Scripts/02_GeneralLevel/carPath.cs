using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPath : MonoBehaviour
{
    public Transform target;

    public void SetTarget(Transform t)
    {
        Debug.Log("Set target");
        target = t;
    }
}
