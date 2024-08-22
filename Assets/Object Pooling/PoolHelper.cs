using UnityEngine;

public static class PoolHelper
{
    public static void Populate(this GameObject pref, int count)
    {
        Pool.GetPoolByPrefab(pref).Populate(count);
    }

    public static GameObject MyInstantiate(GameObject pref, Transform parentTrans = null)
    {
        var pool = Pool.GetPoolByPrefab(pref);
        if (pool != null)
        {
            Debug.Log("Get from pool");
            return pool.GetInstance(parentTrans);
        }
        else
        {
            Debug.Log("Normal Instantiate");
            return Object.Instantiate(pref, parentTrans);
        }
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
            Debug.Log("Normal destroy");
            Object.Destroy(obj);
        }
    }
}
