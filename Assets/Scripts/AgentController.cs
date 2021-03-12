using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ProjectileSpawner))]
public class AgentController : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;
    public GameplayManager GameplayManager { get => gameplayManager; }

    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody { get => rigidBody; }

    private ProjectileSpawner projectileSpawner;
    public ProjectileSpawner ProjectileSpawner { get => projectileSpawner; }

    private AgentModel agentModel;
    public AgentModel AgentModel { get => agentModel; }

    private bool setupFinished = false;

    public void Setup(GameplayManager gameplayManager) {
        if (setupFinished) {
            Debug.LogWarning("Setup called more than once");
            return;
        }

        this.gameplayManager = gameplayManager;

        rigidBody = GetComponent<Rigidbody2D>();

        projectileSpawner = GetComponent<ProjectileSpawner>();
        projectileSpawner.Setup(this);

        agentModel = GetComponentInChildren<AgentModel>();
        agentModel.Setup();

        setupFinished = true;
    }

    // void Awake() {

    // }
}
