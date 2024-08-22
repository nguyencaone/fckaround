
using UnityEngine;

public class SampleObject : GeneralPoolableObject
{
    // Start is called before the first frame update

    private void OnBecameInvisible()
    {
        // Destroy(gameObject);
        gameObject.ReturnToPool();
    }

    public override void PrepareToUse()
    {
        base.PrepareToUse();
        transform.position = Vector3.zero;
    }
}
