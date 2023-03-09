using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyShooterType
{
    Horizontal,
    Vectical,
    Stationary
}

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyShooterType enemyType = EnemyShooterType.Horizontal;
    [SerializeField] private float moveSpeed = 0.75f;
    [SerializeField] private float changingPositionDelay = 2f;

    private float min_XY_Pos, max_XY_Pos;
    private float changingPositionTimer;

    private Vector3 minPos, maxPos;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private Vector3 myScale;

    private bool changedPosition;
    private bool playerInRange;
    private EnemyShootController shootController;

    [SerializeField] private float shootTimeDelay = 2f;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private EnemyBatchHandler enemyBatch;

    private float shootTimer;
    private Transform playerTransform;
    private CharacterHealth enemyHealth;
    
    private void Awake()
    {
        startingPosition = transform.position;

        if (enemyType == EnemyShooterType.Horizontal)
        {
            min_XY_Pos = transform.GetChild(0).transform.localPosition.x;
            max_XY_Pos = transform.GetChild(1).transform.localPosition.x;
        }
        else if (enemyType == EnemyShooterType.Vectical)
        { 
            min_XY_Pos = transform.GetChild(0).transform.localPosition.y;
            max_XY_Pos = transform.GetChild(1).transform.localPosition.y;
        }   
        else
        {
            minPos = transform.GetChild(0).transform.position;
            maxPos = transform.GetChild(1).transform.position;
            targetPosition = maxPos;
        }

        changingPositionTimer = Time.time + changingPositionDelay;

        shootController = GetComponent<EnemyShootController>();
        enemyHealth = GetComponent<CharacterHealth>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void Update()
    {
        if (!enemyHealth.IsAlive() || !playerTransform)
            return;

        CheckToShootPlayer();    
    }

    private void OnDisable()
    {
        if(!enemyHealth.IsAlive())
            enemyBatch.RemoveShooterEnemy(this);
    }
    private void FixedUpdate()
    {
        if (!enemyHealth.IsAlive() || !playerTransform)
            return;

        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (!changedPosition)
        {
            if (enemyType == EnemyShooterType.Horizontal)
            {
                float xPos = Random.Range(min_XY_Pos, max_XY_Pos);
                targetPosition = startingPosition + Vector3.right * xPos;
                changedPosition = true;
            }
            else if(enemyType == EnemyShooterType.Vectical)
            {
                float yPos = Random.Range(min_XY_Pos, max_XY_Pos);
                targetPosition = startingPosition + Vector3.up * yPos;
                changedPosition = true;
            }
            else
            {
                targetPosition = (maxPos == targetPosition) ? minPos : maxPos;
                changedPosition = true;
            }
        }

        if(Vector3.Distance(transform.position,targetPosition) <= 0.05f &&
            Time.time > changingPositionTimer)
        {
            changedPosition = false;
            changingPositionTimer = Time.time + changingPositionDelay;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        HandleFacingDirection();
    }

    private void HandleFacingDirection()
    {
        myScale = transform.localScale;

        if (targetPosition.x > transform.position.x)
            myScale.x = -Mathf.Abs(myScale.x);
        else
            myScale.x = Mathf.Abs(myScale.x);
    
        transform.localScale = myScale;
    }

    private void CheckToShootPlayer()
    {
        if(playerInRange)
        {
            if(Time.time > shootTimer)
            {
                shootTimer = Time.time + shootTimeDelay;
                Vector2 direction = (playerTransform.position - bulletSpawnPosition.position).normalized;
                shootController.Shoot(direction, bulletSpawnPosition.position);
            }
        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        playerInRange = inRange;
    }
}
