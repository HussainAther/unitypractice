using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatchHandler : MonoBehaviour
{
    [SerializeField] private bool hasShooterEnemies;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private Transform shooterEnemyHolder;
    [SerializeField] private List<EnemyShooter> shooterEnemies;

    private void Awake()
    {
        foreach(Transform trans in GetComponentInChildren<Transform>())
        {
            if (trans != this)
                enemies.Add(trans.GetComponent<Enemy>());
        }

        if(hasShooterEnemies)
        {
            foreach (Transform trans in shooterEnemyHolder.GetComponentInChildren<Transform>())
            {
                if (trans != this)
                    shooterEnemies.Add(trans.GetComponent<EnemyShooter>());
            }

        }
    }

    public void EnablePlayerTarget()
    {
        foreach(Enemy characterMovement in enemies)
            characterMovement.HasPlayerTarget = true;
    }

    public void DisablePlayerTarget()
    {
        foreach (Enemy characterMovement in enemies)
            characterMovement.HasPlayerTarget = false;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);

        //CheckToUnlockGate();
    }

    public void RemoveShooterEnemy(EnemyShooter shooterEnemy)
    {
        if(shooterEnemies != null)
            shooterEnemies.Remove(shooterEnemy);
        
        //CheckToUnlockGate();
    }

    private void CheckToUnlockGate()
    {

    }
}
