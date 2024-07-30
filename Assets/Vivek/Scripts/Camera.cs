using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform cameraFollowTarget;
    public Vector3 velocity = Vector3.zero;
    [Range(0, 1)]
    public float smoothTime;
    public Vector3 positionOffset;
    [Header("Axis Limitation")]
    public Vector2 xLimit; // X axis limitation
    public Vector2 yLimit; // Y axis limitation
    public bool freeZAxis = false; // Flag to control whether z-axis is free

    private void LateUpdate()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        Vector3 targetPosition = cameraFollowTarget.position + positionOffset;

        if (!freeZAxis)
        {
            // Keep the camera's z position unchanged
            targetPosition.z = transform.position.z;
        }

        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y),
            Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y),
            targetPosition.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

}
