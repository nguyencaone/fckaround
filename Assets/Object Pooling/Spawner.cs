using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject targetPref;
    public float deltaTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        PoolHelper.GetInstance(targetPref);
        yield return new WaitForSeconds(deltaTime);
        StartCoroutine(Spawn());
    }
}
