using System.Collections.Generic;
using UnityEngine;

public class SpriteStackingObject : MonoBehaviour
{
    public List<Sprite> sprites;
    public bool isReverse;
    List<SpriteRenderer> spriteRenderers;
    List<Vector3> space = new List<Vector3>();
    List<Vector3> spaceScale = new List<Vector3>();
    public float deltaSpace = 1 / 15f;
    bool started = false;
    Vector2 dis;
    float xSlash;
    float ySlash;
    float hDis;

    float sinAy;
    float sinAx;

    // Start is called before the first frame update
    void Start()
    {
        started = false;
        spriteRenderers = new List<SpriteRenderer>();
        if (isReverse)
        {
            sprites.Reverse();
        }
        SpriteRenderer spriteLayer;
        for (int j = 0; j < sprites.Count; j++)
        {
            spriteLayer = new GameObject("SpriteLayer_" + j).AddComponent<SpriteRenderer>();
            spriteLayer.transform.SetParent(transform);
            spriteLayer.transform.localPosition = Vector3.zero;
            spriteLayer.transform.localScale = Vector3.one;
            spriteLayer.sprite = sprites[j];
            spriteRenderers.Add(spriteLayer);

            space.Add(new Vector3(0, 0, 0));
            spaceScale.Add(Vector3.one);
            spriteRenderers[j].sortingOrder = j;
        }

        started = true;
    }

    void SetSpritePos()
    {
        if (spaceScale.Count == 0) return;
        if (space.Count == 0) return;
        Vector3 v3;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            v3 = new Vector3(space[i].x * spaceScale[i].x, space[i].y * spaceScale[i].y, space[i].z * spaceScale[i].z);
            spriteRenderers[i].transform.position = (Vector2)(spriteRenderers[0].transform.position + v3);
        }
    }


    // element at i
    void DoSth(int i)
    {
        dis = (Vector2)spriteRenderers[i].transform.position - CameraController.GetCamBottomCenterPos();
        hDis = CameraController.Ins.getCamHeight() - i * deltaSpace;
        xSlash = Mathf.Sqrt(Mathf.Pow(hDis, 2) + dis.x * dis.x);
        ySlash = Mathf.Sqrt(Mathf.Pow(hDis, 2) + dis.y * dis.y);

        // space scale
        // can be optimize
        spaceScale[i] = new Vector3(hDis / ySlash, hDis / xSlash, spaceScale[i].z);

        sinAy = dis.y / ySlash;
        sinAx = dis.x / xSlash;

        // space dis
        space[i] = new Vector3(i * sinAx * deltaSpace, i * sinAy * deltaSpace, space[i].z);

    }

    bool IsTargetVisible(Camera c, GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    private void FixedUpdate()
    {
        if (!started) return;
        if (!IsTargetVisible(Camera.main, gameObject))
        {
            return;
        }
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            DoSth(i);
        }
        SetSpritePos();
    }
}
