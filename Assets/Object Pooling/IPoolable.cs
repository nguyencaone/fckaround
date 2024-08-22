using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public void ReturnToPool();
    public void PrepareToUse();
    public void SetPool(Pool pool);
}
