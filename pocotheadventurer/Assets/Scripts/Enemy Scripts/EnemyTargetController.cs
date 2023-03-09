using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTargetType
{
    EnableEnemyTarget,
    DisableEnemyTarget
}

public class EnemyTargetController : MonoBehaviour
{
    [SerializeField] private EnemyTargetType enemyTargetType;
    [SerializeField] private EnemyBatchHandler enemyBatch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.PLAYER_TAG))
        {
            if (enemyTargetType == EnemyTargetType.EnableEnemyTarget)
                enemyBatch.EnablePlayerTarget();
            else
                enemyBatch.DisablePlayerTarget();
        }
    }
}
