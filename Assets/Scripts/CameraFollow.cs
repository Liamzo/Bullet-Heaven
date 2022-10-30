using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0,0,-10);

    public float smoothSpeed = 0.5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null) {
            return;
        }
        Vector3 targetPos = target.position + offset;
        Vector3 movePos = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = movePos;
    }
}
