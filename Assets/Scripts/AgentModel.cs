using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentModel : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPointCenter;
    public Transform ProjectileSpawnPointCenter { get => projectileSpawnPointCenter; }

    [SerializeField] private float projectileSpawnPointRadius;
    public float ProjectileSpawnPointRadius { get => projectileSpawnPointRadius; }

    private float projectileSpawnPointRadiusScaled;
    public float ProjectileSpawnPointRadiusScaled { get => projectileSpawnPointRadiusScaled; }

    private SpriteRenderer spriteRend;

    public void Setup() {
        spriteRend = GetComponentInChildren<SpriteRenderer>();

        projectileSpawnPointRadiusScaled = projectileSpawnPointRadius * transform.localScale.y;
    }

    public void Flip() {
        spriteRend.flipX = !spriteRend.flipX;
    }

    private void OnValidate() {
        projectileSpawnPointRadiusScaled = projectileSpawnPointRadius * transform.localScale.y;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (projectileSpawnPointCenter) {
            Gizmos.DrawWireSphere(projectileSpawnPointCenter.position, projectileSpawnPointRadiusScaled);
        }
    }
#endif
}
