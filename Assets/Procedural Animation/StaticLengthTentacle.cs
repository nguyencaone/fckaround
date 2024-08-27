using UnityEngine;

public class StaticLengthTentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRenderer;
    public float smoothTime;

    Vector3[] segmentPoses;
    Vector3[] segmentVelocities;

    public Transform targetDir;
    public float spaceBetween;

    public float wiggleSpeed, wiggleMagnitude;
    public Transform wiggleDir;

    Vector2 _v2;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocities = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < length; i++)
        {
            _v2 = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * spaceBetween;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], _v2, ref segmentVelocities[i], smoothTime);
        }

        lineRenderer.SetPositions(segmentPoses);
    }
}
