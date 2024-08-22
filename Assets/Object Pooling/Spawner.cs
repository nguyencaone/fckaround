using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject targetPref;
    public float deltaTime;

    Rigidbody2D rb;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        rb = PoolHelper.MyInstantiate(targetPref).GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-.5f, 0.5f), 1) * force;
        yield return new WaitForSeconds(deltaTime);
        StartCoroutine(Spawn());
    }
}
