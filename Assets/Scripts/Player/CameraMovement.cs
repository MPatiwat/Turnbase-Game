using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public Transform focus;
    public float smoothTime = 2;

    public Vector2 maxCamPosition;
    public Vector2 minCamPosition;




    void Update()
    {
        if (transform.position != focus.position)
        {
            Vector3 targetPosition = new Vector3(focus.position.x, focus.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(focus.position.x, minCamPosition.x, maxCamPosition.x);
            targetPosition.y = Mathf.Clamp(focus.position.y, minCamPosition.y, maxCamPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothTime);
        }
    }
}

