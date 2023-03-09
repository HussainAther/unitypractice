using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myBody;

    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float damageAmount = 25f;

    private bool dealthDamage;

    [SerializeField] private float deactivateTimer = 3f;
    [SerializeField] private bool destroyObj;

    private Animator anim;
    private SpriteRenderer sr;
    private Sprite bulletSprite;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, false);

        sr = GetComponent<SpriteRenderer>();
        bulletSprite = sr.sprite;
    }

    private void OnEnable()
    {
        anim.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, false);
        anim.enabled = false;
        
        dealthDamage = false;
        sr.sprite = bulletSprite;
        Invoke("DeactivateBullet", deactivateTimer);
    }

    private void OnDisable()
    {
        myBody.velocity = Vector2.zero;
    }

    public void MoveInDirection(Vector3 direction)
    {
        myBody.velocity = direction * moveSpeed;
    }

    private void DeactivateBullet()
    {
        if (destroyObj)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag(TagManager.ENEMY_TAG) ||
            collision.CompareTag(TagManager.SHOOTER_ENEMY_TAG) ||
            collision.CompareTag(TagManager.BOSS_TAG))
        {
            StopBulletAndPlayExplodeAnim();

            if(!dealthDamage)
            {
                dealthDamage = true;

                // deal damage to enemy
                collision.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
            }
        }
        else if(collision.CompareTag(TagManager.BLOCKING_TAG))
        {
            StopBulletAndPlayExplodeAnim();
        }
    }

    private void StopBulletAndPlayExplodeAnim()
    {
        myBody.velocity = Vector2.zero;
        CancelInvoke("DeactivateBullet");
        anim.enabled = true;
        anim.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);
    }
}
