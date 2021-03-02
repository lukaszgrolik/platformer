using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private float radius = 5f;
    private float speed = 5f;

    private float currentPos = 0f;
    private int dir = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var posChange = speed * dir * Time.deltaTime;
        currentPos += posChange;

        transform.position += new Vector3(posChange, 0, 0);

        if (currentPos > radius || currentPos < -radius) dir *= -1;
    }
}
