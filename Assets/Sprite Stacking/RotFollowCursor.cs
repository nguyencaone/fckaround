using UnityEngine;

public class RotFollowCursor : MonoBehaviour
{
    public float rotSpeed;
    Vector2 _v2;

    // Update is called once per frame
    void Update()
    {
        _v2 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(_v2.y, _v2.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
    }
}
