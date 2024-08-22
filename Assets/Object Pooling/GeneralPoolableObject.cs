using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPoolableObject : MonoBehaviour, IPoolable
{
    Pool _pool;
    public virtual void ReturnToPool()
    {
        gameObject.SetActive(false);
        PrepareToUse();

        if(_pool == null)
        {
            return;
        }
        _pool.ReturnObject(this.gameObject);
    }

    public virtual void PrepareToUse()
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
    }

    public virtual void SetPool(Pool pool)
    {
        _pool = pool;
    }
}
