using UnityEngine;

public class Tentacle : MonoBehaviour
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
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * spaceBetween, ref segmentVelocities[i], smoothTime);
        }

        lineRenderer.SetPositions(segmentPoses);
    }
}
