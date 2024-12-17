using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float orthoSize = 5f; // Ajuste este valor para aumentar ou diminuir o zoom

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic)
        {
            cam.orthographicSize = orthoSize;
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Ajuste o tamanho ortográfico em tempo real (opcional)
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, orthoSize, smoothSpeed);
        }
    }
}
