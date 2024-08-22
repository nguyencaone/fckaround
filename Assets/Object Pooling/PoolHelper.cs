using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PoolHelper 
{
    public static void Populate(this GameObject pref, int count)
    {
        Pool.GetPoolByPrefab(pref).Populate(count);
    }

    public static void GetInstance(this GameObject pref)
    {
        Pool.GetPoolByPrefab(pref).GetInstance();
    }

    public static void ReturnToPool(this GameObject obj)
    {
        var v = obj.GetComponent<IPoolable>();
        if (v != null)
        {
            v.ReturnToPool();
        }
        else
        {
            Object.Destroy(obj);
        }
    }
}
