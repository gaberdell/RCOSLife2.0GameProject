using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float movementSpeed = 0.125f;

    public Vector2 maximumPosition;
    public Vector2 minimumPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minimumPosition.x, maximumPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minimumPosition.y, maximumPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, movementSpeed);
        }

    }
}
