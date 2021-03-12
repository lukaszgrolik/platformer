using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpawnPointRadius;
    [SerializeField] private float baseMovementSpeed = 5;
    [SerializeField] private float baseJumpSpeed = 5;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private float movementX;

    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool topPressed = false;

    private float timeSpentMovingInOneDirection = 0;
    // private Vector3 lastVelocity;

    private float movSpeed;
    private float jumpSpeed;

    private int speedMode = 0;

    // private Vector3 resBulletSpawnPointCenter;
    private float bulletSpawnPointRadiusScaled;

    private Vector3 mouseWorldPos;

    private float mouseAngle;
    private bool flipped = false;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        movSpeed = baseMovementSpeed * .5f;
        jumpSpeed = baseJumpSpeed * .75f;

        // resBulletSpawnPointCenter = transform.localPosition + (bulletSpawnPointCenter * transform.localScale.y);
        bulletSpawnPointRadiusScaled = bulletSpawnPointRadius * transform.localScale.y;
    }

    // Update is called once per frame
    void Update() {
        // movementX = Input.GetAxis("Horizontal");

        mouseWorldPos = cam.ScreenToWorldPoint((Vector2)Input.mousePosition);
        var dir = (mouseWorldPos - bulletSpawnPoint.position).normalized;
        mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bulletSpawnPoint.eulerAngles = new Vector3(0, 0, mouseAngle);

        if (mouseAngle > -90 && mouseAngle < 90 && flipped) {
            // show right side
            spriteRend.flipX = !spriteRend.flipX;
            flipped = false;
        }
        else if ((mouseAngle >= 90 || mouseAngle <= -90) && !flipped) {
            // show left side
            spriteRend.flipX = !spriteRend.flipX;
            flipped = true;
        }

        if (Input.GetMouseButtonDown(0)) {
            // var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            var pos = bulletSpawnPoint.position + bulletSpawnPoint.right * bulletSpawnPointRadiusScaled;
            var bullet = Instantiate(bulletPrefab, pos, bulletSpawnPoint.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.right * 35;
            // transform.Translate(0, 0, speed * Time.deltaTime);
        }

        leftPressed = Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.D);
        topPressed = Input.GetKey(KeyCode.W);

        if (rb.velocity.Equals(Vector3.zero) || (!leftPressed && !rightPressed)) {
            speedMode = 0;

            timeSpentMovingInOneDirection = 0;

            movSpeed = baseMovementSpeed * .5f;
            jumpSpeed = baseJumpSpeed * .75f;
        } else {
            timeSpentMovingInOneDirection += Time.deltaTime;

            if (timeSpentMovingInOneDirection > .2f && speedMode == 0) {
                speedMode = 1;

                movSpeed = baseMovementSpeed * .75f;
                jumpSpeed = baseJumpSpeed * .85f;
            }

            if (timeSpentMovingInOneDirection > .4f && speedMode == 1) {
                speedMode = 2;

                movSpeed = baseMovementSpeed;
                jumpSpeed = baseJumpSpeed;
            }
        }

        // lastVelocity = rb.velocity;
    }

    void FixedUpdate()
    {
        // rb.MovePosition(transform.position + new Vector3(movementX, 0, 0) * speed * Time.deltaTime);

        // if (Input.GetKey(KeyCode.A)) {
        if (leftPressed) {
            // Debug.Log(rb.velocity);
            rb.velocity = new Vector3(-movSpeed, rb.velocity.y);
        }
        // else if (Input.GetKey(KeyCode.D)) {
        else if (rightPressed) {
            // Debug.Log(rb.velocity);
            rb.velocity = new Vector3(movSpeed, rb.velocity.y);
        }

        // if (Input.GetKey(KeyCode.W)) {
        if (topPressed) {
            // Debug.Log(rb.velocity);
            if (rb.velocity.y == 0) {
                // var resJumpSpeed = Mathf.Abs(rb.velocity.x) > ;

                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed);
            }

            // rb.velocity = new Vector3(0, 5, 0);
            // rb.MovePosition(transform.position + new Vector3(0, 10, 0) * speed * Time.deltaTime);
            // rb.AddForce(new Vector3(0, 2000, 0));
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Handles.Label(transform.position, $"{mouseWorldPos.ToString()} | {Input.mousePosition}");
        Handles.Label(transform.position, $"{mouseAngle.ToString()}");
        Gizmos.DrawWireSphere(bulletSpawnPoint.position, bulletSpawnPointRadiusScaled);
    }
#endif
}
