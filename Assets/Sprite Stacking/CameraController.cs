using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Ins;
    public float zSpeed = 5;
    float zDirec = 0;
    public static float fixedFocalLenght = 12;
    public static float camOrthorToHeightRate = 3;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public float getCamHeight()
    {
        return Camera.main.orthographicSize * camOrthorToHeightRate;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        zDirec = 0;
        if (Input.GetKey(KeyCode.I))
        {
            zDirec = 1;
        }
        if (Input.GetKey(KeyCode.O))
        {
            zDirec = -1;
        }
        Camera.main.orthographicSize += zDirec * zSpeed * Time.deltaTime;
    }

    public static Vector2 GetCamCenterPos()
    {
        var hitPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        return (Vector2)hitPoint;
    }

    public static Vector2 GetCamBottomCenterPos()
    {
        var hitPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, Camera.main.nearClipPlane));
        return (Vector2)hitPoint;
    }
}
