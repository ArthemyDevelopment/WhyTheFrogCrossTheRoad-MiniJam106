using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptsTools
{
    public static float MapValues(float value, float from1, float to1, float from2, float to2) //Map a value based on two ranges
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWait(float time) //Get a wait for second element in case it was already used before, optimizing its use
    {
        if (WaitDictionary.TryGetValue(time, out var wait))return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }
    
    
    public static TComponent AddComponent<TComponent, TFirstArgument>
        (this GameObject gameObject, TFirstArgument firstArgument)
        where TComponent : MonoBehaviour
    {
        Arguments<TFirstArgument>.First = firstArgument;

         
        var component = gameObject.AddComponent<TComponent>();
 
        Arguments<TFirstArgument>.First = default;
 
        return component;
    }

    public static float GetRotation(Transform originObject, Transform target)
    {
        return Mathf.Atan2(target.position.y - originObject.position.y,  target.position.x - originObject.position.x) * Mathf.Rad2Deg;
    }

}

public static class Arguments<TFirstArgument>
{
    public static TFirstArgument First { get; internal set; }
    
}

public abstract class SingletonManager<T> : MonoBehaviour where T : SingletonManager<T>
{
    public static T current; 
    
    
    public virtual void init()
    {
        if (current == null)
            current = this as T;
        else if (current != this)
        {
            Destroy(this);
            return;
        }
        
    }
}
