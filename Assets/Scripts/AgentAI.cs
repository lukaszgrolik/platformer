using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    private AgentController agentController;

    public void Setup(AgentController agentController) {
        this.agentController = agentController;
    }

    void Start()
    {
        StartCoroutine(KeepDetectingEnemies());
    }

    IEnumerator KeepDetectingEnemies() {
        while (true) {
            // @todo melee attack
            if (agentController.AgentModel.ProjectileSpawnPointCenter) {
                var enemy = FindTarget();
                if (enemy) agentController.ProjectileSpawner.Spawn(enemy);
            }

            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    GameObject FindTarget() {
        var colls = Physics2D.OverlapCircleAll(transform.position, 50f, agentController.GameplayManager.AgentLayerMask);

        for (int i = 0; i < colls.Length; i++)
        {
            // @todo detect enemies, not all agents
            var agentCtrl = colls[i].GetComponent<AgentController>();

            if (agentCtrl)
            {
                // don't attack itself
                if (agentCtrl == agentController) continue;

                return agentCtrl.gameObject;
            }

        }

        return null;
    }
}
