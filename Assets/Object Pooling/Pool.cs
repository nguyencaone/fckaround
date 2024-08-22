
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    IPoolable _prototype;
    Stack<GameObject> _usableStack;
    private static readonly Dictionary<GameObject, Pool> _prefabLookup = new Dictionary<GameObject, Pool>(64);

    public Pool(IPoolable pref)
    {
        _prototype = pref;  
        _usableStack = new Stack<GameObject>();
    }

    public static Pool GetPoolByPrefab(GameObject pref)
    {
        var v = pref.GetComponent<IPoolable>();
        if (v == null) return null;
        _prefabLookup.TryGetValue(pref, out var pool);
        if(pool == null)
        {
            pool = new Pool(v);
            _prefabLookup[pref] = pool;
        }
        return pool;
    }

    public void Populate(int count)
    {
        List<GameObject> lst = new List<GameObject>();
        for(int i = 0; i < count; i++)
        {
            lst.Add(CreateNewInstance());
        }

        for (int i = 0; i < count; i++)
        {
            lst[i].ReturnToPool();
            _usableStack.Push(lst[i]) ;
        }
    }

    public static GameObject GetInstance(GameObject pref)
    {
        return GetPoolByPrefab(pref).GetInstance();
    }

    public GameObject GetInstance()
    {
        if(_usableStack.Count == 0)
        {
            return CreateNewInstance();
        }
        return _usableStack.Pop();
    }

    public GameObject CreateNewInstance()
    {
        var v = _prototype as MonoBehaviour;
        if(v == null) return null;
        var res = Object.Instantiate(v.gameObject);
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
