using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public List<TileConfig> lst;

    private void Start()
    {
        List<TileConfig> res = new List<TileConfig>();
        List<TileConfig> tmp;
        foreach (var v in lst)
        {
            // lu
             res = new List<TileConfig>();
            foreach(var i in v.left)
            {
                foreach(var j in v.up)
                {
                    tmp = GetGeneralPoint(i.up, j.left);
                    foreach(var k in tmp)
                    {
                        if (!res.Contains(k))
                        {
                            res.Add(k);
                        }
                    }
                }
            }
            v.lu = res;

            // ru
             res = new List<TileConfig>();
            foreach (var i in v.right)
            {
                foreach (var j in v.up)
                {
                    tmp = GetGeneralPoint(i.up, j.right);
                    foreach (var k in tmp)
                    {
                        if (!res.Contains(k))
                        {
                            res.Add(k);
                        }
                    }
                }
            }
            v.ru = res;

            // ld
            res = new List<TileConfig>();
            foreach (var i in v.left)
            {
                foreach (var j in v.down)
                {
                    tmp = GetGeneralPoint(i.down, j.left);
                    foreach (var k in tmp)
                    {
                        if (!res.Contains(k))
                        {
                            res.Add(k);
                        }
                    }
                }
            }
            v.ld = res;

            // rd
            res = new List<TileConfig>();
            foreach (var i in v.right)
            {
                foreach (var j in v.down)
                {
                    tmp = GetGeneralPoint(i.down, j.right);
                    foreach (var k in tmp)
                    {
                        if (!res.Contains(k))
                        {
                            res.Add(k);
                        }
                    }
                }
            }
            v.rd = res;
        }
    }

    List<TileConfig> GetGeneralPoint(List<TileConfig> a, List<TileConfig> b)
    {
        List<TileConfig> res = new List<TileConfig>();
        for(int i = 0; i < a.Count; i++)
        {
            if (b.Contains(a[i]))
            {
                res.Add(a[i]);
            }
        }
        return res;
    }
}
