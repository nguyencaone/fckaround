using UnityEngine;

public class CameraSubject : MonoBehaviour
{
    Camera _camera;
    Vector3 _position;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _position = _camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _position.x = transform.position.x;
        _position.y = transform.position.y;
        _camera.transform.position = _position;
        //_camera.transform.rotation = transform.rotation;
    }
}
