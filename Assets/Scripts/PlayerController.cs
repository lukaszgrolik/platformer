using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerController : MonoBehaviour
{
    private GameplayManager gameplayManager;

    [SerializeField] private Camera cam;

    [SerializeField] private float baseMovementSpeed = 5;
    [SerializeField] private float baseJumpSpeed = 5;

    // private float movementX;

    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool topPressed = false;

    private float timeSpentMovingInOneDirection = 0;
    // private Vector3 lastVelocity;

    private float movSpeed;
    private float jumpSpeed;

    private int speedMode = 0;

    private Vector3 mouseWorldPos;

    private float mouseAngle;
    private bool flipped = false;

    private const KeyCode JUMPING_KEY = KeyCode.W;
    private bool alreadyJumped = false;

    public void Setup(GameplayManager gameplayManager)
    {
        this.gameplayManager = gameplayManager;

        movSpeed = baseMovementSpeed * .5f;
        jumpSpeed = baseJumpSpeed * .75f;
    }

    void Update() {
        var agentController = gameplayManager.PlayableAgent;
        var rb = agentController.RigidBody;

        // movementX = Input.GetAxis("Horizontal");

        mouseWorldPos = cam.ScreenToWorldPoint((Vector2)Input.mousePosition);
        var dir = (mouseWorldPos - agentController.AgentModel.ProjectileSpawnPointCenter.position).normalized;
        mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // bulletSpawnPoint.eulerAngles = new Vector3(0, 0, mouseAngle);
        agentController.ProjectileSpawner.LookAt(mouseWorldPos);

        if (mouseAngle > -90 && mouseAngle < 90 && flipped) {
            // show right side
            agentController.AgentModel.Flip();
            // spriteRend.flipX = !spriteRend.flipX;
            flipped = false;
        }
        else if ((mouseAngle >= 90 || mouseAngle <= -90) && !flipped) {
            // show left side
            agentController.AgentModel.Flip();
            // spriteRend.flipX = !spriteRend.flipX;
            flipped = true;
        }

        if (Input.GetMouseButtonDown(0)) {
            agentController.ProjectileSpawner.Spawn();
        }

        leftPressed = Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.D);
        topPressed = Input.GetKey(JUMPING_KEY);

        if (Input.GetKeyUp(JUMPING_KEY)) {
            alreadyJumped = false;
        }

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
        var agentController = gameplayManager.PlayableAgent;
        var rb = agentController.RigidBody;

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
            if (rb.velocity.y == 0 && !alreadyJumped) {
                // var resJumpSpeed = Mathf.Abs(rb.velocity.x) > ;

                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed);

                alreadyJumped = true;
            }

            // rb.velocity = new Vector3(0, 5, 0);
            // rb.MovePosition(transform.position + new Vector3(0, 10, 0) * speed * Time.deltaTime);
            // rb.AddForce(new Vector3(0, 2000, 0));
        }
    }

// #if UNITY_EDITOR
//     void OnDrawGizmos()
//     {
//         // Handles.Label(transform.position, $"{mouseWorldPos.ToString()} | {Input.mousePosition}");
//         Handles.Label(transform.position, $"{mouseAngle.ToString()}");
//         Gizmos.DrawWireSphere(bulletSpawnPoint.position, bulletSpawnPointRadiusScaled);
//     }
// #endif
}
