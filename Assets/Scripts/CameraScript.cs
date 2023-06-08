using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothing = 5f;

    // Define the boundaries
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        targetCamPos = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        // Clamp the camera position to the defined boundaries
        targetCamPos.x = Mathf.Clamp(targetCamPos.x, minX, maxX);
        targetCamPos.y = Mathf.Clamp(targetCamPos.y, minY, maxY);

        transform.position = targetCamPos;
    }
}
