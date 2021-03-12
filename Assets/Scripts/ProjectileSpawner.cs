using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    private AgentController agentController;

    private bool setupFinished = false;

    public void Setup(AgentController agentController)
    {
        if (setupFinished)
        {
            Debug.LogWarning("Setup called more than once");
            return;
        }

        this.agentController = agentController;

        setupFinished = true;
    }

    public void LookAt(Vector3 targetPos) {
        var agentModel = agentController.AgentModel;
        var center = agentModel.ProjectileSpawnPointCenter;
        var dir = (targetPos - center.position).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        center.eulerAngles = new Vector3(0, 0, angle);
    }

    public void Spawn() {
        var agentModel = agentController.AgentModel;
        var center = agentModel.ProjectileSpawnPointCenter;
        var pos = center.position + center.right * agentModel.ProjectileSpawnPointRadiusScaled;
        var bullet = Instantiate(projectilePrefab, pos, center.rotation);
    }

    public void Spawn(GameObject other) {
        LookAt(other.transform.position);
        Spawn();
    }
}
