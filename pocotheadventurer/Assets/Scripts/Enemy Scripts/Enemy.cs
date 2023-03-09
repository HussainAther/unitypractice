using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement
{
    private Transform playerTarget;
    private Vector3 playerLastTrackedPosition;

    private Vector3 startingPosition;
    private Vector3 enemyMovementMotion;
    private bool dealthDamageToPlayer;

    [SerializeField] private float damageCooldownTreshold = 1f;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float chaseSpeed = 0.8f;

    private float damageCooldownTimer;
    private float lastFollowTime;
    private float turningTimeDelay = 1f;

    [SerializeField] private float turningDelayRate = 1f;
    private Vector3 myScale;
    private CharacterHealth enemyHealth;

    [SerializeField] private EnemyBatchHandler batchHandler;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        playerLastTrackedPosition = playerTarget.position;

        startingPosition = transform.position;
        lastFollowTime = Time.time;
        turningTimeDelay = ((float)1f - (float)xSpeed);
        turningTimeDelay += 1f * turningDelayRate;

        enemyHealth = GetComponent<CharacterHealth>();
        batchHandler = GetComponentInParent<EnemyBatchHandler>();
    }

    private void Update()
    {
        if (!playerTarget || !enemyHealth.IsAlive())
            return;

        HandleFacingDirection();
    }

    private void OnDestroy()
    {
        if (!enemyHealth.IsAlive())
            batchHandler.RemoveEnemy(this);
    }

    private void FixedUpdate()
    {
        if (!enemyHealth.IsAlive())
            return;

        HandleChasingPlayer();
    }


    private void HandleChasingPlayer()
    {
        if(HasPlayerTarget)
        {
            if(!dealthDamageToPlayer)
            {
                ChasePlayer();
            }
            else
            {
                if (Time.time < damageCooldownTimer)
                {
                    enemyMovementMotion = startingPosition - transform.position;
                }
                else
                    dealthDamageToPlayer = false;
            }
        }
        else
        {
            enemyMovementMotion = startingPosition - transform.position;
            
            if(Vector3.Distance(transform.position,startingPosition) < 0.1f)
            {
                enemyMovementMotion = Vector3.zero;
            }
        }
        HandleMovement(enemyMovementMotion.x, enemyMovementMotion.y);
    }

    private void ChasePlayer()
    {
        if(Time.time - lastFollowTime > turningTimeDelay)
        {
            playerLastTrackedPosition = playerTarget.position;
            lastFollowTime = Time.time;
        }

        if(Vector3.Distance(transform.position,playerLastTrackedPosition) > 0.016f)
        {
            enemyMovementMotion = (playerLastTrackedPosition - transform.position).normalized * chaseSpeed;
        }
        else
            enemyMovementMotion = Vector3.zero;
    }

    private void HandleFacingDirection()
    {
        myScale = transform.localScale;

        if(HasPlayerTarget)
        {
            if(playerTarget.position.x > transform.position.x)
                myScale.x = Mathf.Abs(myScale.x);
            else if(playerTarget.position.x < transform.position.x)
                myScale.x = -Mathf.Abs(myScale.x);
        }
        else
        {
            // face the direction where initial position is
            if (startingPosition.x > transform.position.x)
                myScale.x = Mathf.Abs(myScale.x);
            else if (startingPosition.x < transform.position.x)
                myScale.x = -Mathf.Abs(myScale.x);

        }
        transform.localScale = myScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.PLAYER_TAG))
        {
            // deal damage to the player
            damageCooldownTimer = Time.time + damageCooldownTreshold;
            dealthDamageToPlayer = true;
            collision.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
        }
    } 
}
