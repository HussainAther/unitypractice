using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectZone : MonoBehaviour
{
    private EnemyShooter enemyShooter;

    private void Awake()
    {
        enemyShooter = GetComponentInParent<EnemyShooter>();
        //if (!enemyShooter)
           // Debug.Log("Enemy shooter is null!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.PLAYER_TAG))
            enemyShooter.SetPlayerInRange(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.PLAYER_TAG))
            enemyShooter.SetPlayerInRange(false);
    }

}
