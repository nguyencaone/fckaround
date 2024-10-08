
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    IPoolable _prototype;
    Stack<GameObject> _usableStack;
    private static Dictionary<GameObject, Pool> prefabLookup=null;

    public Pool(IPoolable pref)
    {
        _prototype = pref;
        _usableStack = new Stack<GameObject>();
    }

    public static void InitPrefabLookup()
    {
        prefabLookup = new Dictionary<GameObject, Pool>(64);
    }

    public static Pool GetPoolByPrefab(GameObject pref)
    {
        var v = pref.GetComponent<IPoolable>();
        if (v == null) return null;

        prefabLookup.TryGetValue(pref, out var pool);

        if (pool == null)
        {
            pool = new Pool(v);
            prefabLookup[pref] = pool;
        }
        else
        {
            // Debug.Log("Pool existed");
        }
        return pool;
    }

    public void Populate(int count)
    {
        List<GameObject> lst = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            lst.Add(CreateNewInstance());
        }

        for (int i = 0; i < count; i++)
        {
            lst[i].ReturnToPool();
            _usableStack.Push(lst[i]);
        }
    }

    public static GameObject GetInstance(GameObject pref, Transform parentTrans = null)
    {
        return GetPoolByPrefab(pref).GetInstance(parentTrans);
    }

    public GameObject GetInstance(Transform parentTrans)
    {
        Debug.Log($"Usable: {_usableStack.Count}");
        if (_usableStack.Count == 0)
        {
            Debug.Log("Stack not enough, instantiate new instance");
            return CreateNewInstance(parentTrans);
        }
        var v = _usableStack.Pop();
        v.SetActive(true);
        if (parentTrans != null) v.transform.SetParent(parentTrans);
        return v;
    }

    public GameObject CreateNewInstance(Transform parentTrans = null)
    {
        var v = _prototype as MonoBehaviour;
        if (v == null) return null;
        var res = Object.Instantiate(v.gameObject);
        if (parentTrans != null) res.transform.SetParent(parentTrans);
        res.GetComponent<IPoolable>().SetPool(this);
        return res;
    }

    public void ReturnObject(GameObject obj)
    {
        var v = obj.GetComponent<IPoolable>();
        if (v == null) return;

        _usableStack.Push(obj);
    }
}
