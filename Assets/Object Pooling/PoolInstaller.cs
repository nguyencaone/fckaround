using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInstaller : MonoBehaviour
{
    [Serializable]
    struct ObjectConfig
    {
        [SerializeField] GameObject obj;
        [SerializeField, Min(1)] int initAmount;

        public void Populate()
        {
            obj.Populate(initAmount);
        }
    }
    [SerializeField] List<ObjectConfig> poolableItems;

    private void Awake()
    {
        foreach (var item in poolableItems)
        {
            item.Populate();
        }
    }
}
