using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Start() {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        var agent = other.gameObject.GetComponent<AgentController>();
        if (agent) {
            // Destroy(gameObject);
            // Destroy(other.gameObject);
        }
    }

    // void OnCollisionEnter() {
    //     Debug.Log("collision");
    //     Destroy(gameObject);
    // }
}
