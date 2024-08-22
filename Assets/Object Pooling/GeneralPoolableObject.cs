using UnityEngine;

public class GeneralPoolableObject : MonoBehaviour, IPoolable
{
    Pool _pool;
    public virtual void ReturnToPool()
    {
        Debug.Log("Return to pool");
        gameObject.SetActive(false);
        PrepareToUse();

        if (_pool == null)
        {
            Debug.Log("Pool null");
            return;
        }
        _pool.ReturnObject(gameObject);
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
