using UnityEngine;

public class C_CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.25f;
    public float distanceMultiplier = 1.0f;

    private void Start()
    {
        transform.position = target.position;
    }

    private void FixedUpdate()
    {
        Vector3 smoothPosition = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        smoothPosition.z = -10;
        transform.position = smoothPosition;
    }

    public void OnValidate()
    {
        GetComponent<Camera>().orthographicSize = 3.0f * distanceMultiplier;
    }
}
