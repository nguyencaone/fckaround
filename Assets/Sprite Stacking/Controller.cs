using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = (Input.GetAxisRaw("Vertical") * transform.up + Input.GetAxisRaw("Horizontal") * transform.right) * speed;
    }
}
