using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private AgentController playableAgent;
    public AgentController PlayableAgent { get => playableAgent; }

    [SerializeField] private LayerMask agentLayerMask;
    public LayerMask AgentLayerMask { get => agentLayerMask; }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.Setup(this);
        // var playerAgentController = playerController.AgentController;

        var agentControllers = FindObjectsOfType<AgentController>();
        for (int i = 0; i < agentControllers.Length; i++)
        {
            var agentCtrl = agentControllers[i];
            agentCtrl.Setup(this);

            if (agentCtrl != playableAgent) {
                var agentAI = agentCtrl.gameObject.AddComponent<AgentAI>();
                agentAI.Setup(agentCtrl);
            }
        }
    }
}
