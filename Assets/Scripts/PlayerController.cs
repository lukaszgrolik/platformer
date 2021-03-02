using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    private float speed = 5;

    private Rigidbody2D rb;
    private float movementX;

    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool topPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        // movementX = Input.GetAxis("Horizontal");
        if (Input.GetMouseButtonDown(0)) {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.right * 35;
            // transform.Translate(0, 0, speed * Time.deltaTime);
        }

        // leftPressed = Input.GetKey(KeyCode.A);
        // rightPressed = Input.GetKey(KeyCode.D);
        // topPressed = Input.GetKey(KeyCode.W);
    }

    void FixedUpdate()
    {
        // rb.MovePosition(transform.position + new Vector3(movementX, 0, 0) * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A)) {
            rb.velocity = new Vector3(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D)) {
            rb.velocity = new Vector3(speed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.W)) {
            rb.velocity = new Vector3(rb.velocity.x, speed);

            // rb.velocity = new Vector3(0, 5, 0);
            // rb.MovePosition(transform.position + new Vector3(0, 10, 0) * speed * Time.deltaTime);
            // rb.AddForce(new Vector3(0, 2000, 0));
        }
    }
}
