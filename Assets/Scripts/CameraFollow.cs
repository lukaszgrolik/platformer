using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private Transform target;
    [SerializeField] private bool smooth = true;
    [SerializeField] private float smoothSpeed = 10f;


    public void Setup(Transform target)
    {
        this.target = target;
    }

    void LateUpdate()
    {
        var tPos = target.position;
        var desiredPos = new Vector3(tPos.x, tPos.y, targetCamera.transform.position.z);

        if (!smooth) {
            targetCamera.transform.position = desiredPos;
        }
        else {
            var smoothedPos = Vector3.Lerp(targetCamera.transform.position, desiredPos, smoothSpeed * Time.deltaTime);

            targetCamera.transform.position = smoothedPos;
        }
    }
}
