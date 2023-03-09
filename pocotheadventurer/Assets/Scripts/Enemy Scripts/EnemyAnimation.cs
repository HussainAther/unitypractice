using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private CharacterMovement enemyMovement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<CharacterMovement>();    
    }

    private void Update()
    {
        EnemyMovementAnimation();
    }

    private void EnemyMovementAnimation()
    {
        if(enemyMovement.GetMoveDelta().magnitude > 0.0f)
            anim.SetBool(TagManager.WALK_ANIMATION_PARAMETER, true);
        else
            anim.SetBool(TagManager.WALK_ANIMATION_PARAMETER, false);
    }

    public void DeathAnimation()
    {
        anim.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);
    }
}
