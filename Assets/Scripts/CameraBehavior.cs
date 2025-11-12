using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float translationFactor = 10;

    void LateUpdate()
    {
        if (targetObject == null)
        {
            return;
        }

        Vector3 desiredPosition = new Vector3(
            targetObject.transform.position.x + offset.x,
            targetObject.transform.position.y + offset.y,
            transform.position.z + offset.z
        );

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
