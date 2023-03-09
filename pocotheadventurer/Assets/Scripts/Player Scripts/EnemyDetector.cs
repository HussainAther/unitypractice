using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private List<SpriteRenderer> enemiesInRange = new List<SpriteRenderer>();
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        //playerMovement.HasEnemyTarget = (enemiesInRange.Count > 0)? true : false;
    }

    public Vector2 GetClosestEnemyPosition()
    {
        Vector2 pos = new Vector2();
        Vector2 playerPos = new Vector2(playerMovement.transform.position.x, 
                                        playerMovement.transform.position.y);

        float prevDist = 0;

        foreach (SpriteRenderer enemy in enemiesInRange)
        {
            Transform trans = enemy.GetComponent<Transform>();
            float dist = Vector2.Distance(  playerPos, 
                                            new Vector2(trans.position.x, trans.position.y));
            if (dist < prevDist)
            {
                prevDist = dist;
                pos.x = trans.position.x;
                pos.y = trans.position.y;
            }
        }
        return pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("ShooterEnemy"))
            enemiesInRange.Add(collision.GetComponent<SpriteRenderer>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("ShooterEnemy"))
        {
            enemiesInRange.Remove(collision.GetComponent<SpriteRenderer>());
            Debug.Log("Enemies in range :" + enemiesInRange.Count);
        }
    }

}
