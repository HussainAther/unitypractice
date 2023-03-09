using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private float moveX, moveY;
    private Camera mainCam;
    private Animator anim;

    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector2 facingDirection;
    private Vector3 tempScale;

    private PlayerWeaponManager playerWeaponManager;
    private PlayerShootingManager playerShootingManager;
    private CharacterHealth playerHealth;

    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private Joystick joystick;
    //[SerializeField] private EnemyDetector enemyDetector;

    protected override void Awake()
    {
        base.Awake();

        mainCam = Camera.main;
        anim = GetComponent<Animator>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
        facingDirection = new Vector2(0, -1);
    }

    private void Start()
    {
        playerHealth = GetComponent<CharacterHealth>();    
    }

    private void FixedUpdate()
    {
        if (!playerHealth.IsAlive())
            return;

        if (gameplayController.runOnMobile)
        {
            if (joystick.Horizontal < 0)
                moveX = (joystick.Horizontal < -0.1) ? -1 : 0;
            else
                moveX = (joystick.Horizontal > 0.1) ? 1 : 0;

            if (joystick.Vertical < 0)
                moveY = (joystick.Vertical < -0.1) ? -1 : 0;
            else
                moveY = (joystick.Vertical > 0.1) ? 1 : 0;
        }
        else
        {
            moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
            moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);
        }
        if(!(moveX == 0 && moveY == 0))
            UpdateFacingDirection();
        HandlePlayerTurning();
        HandleMovement(moveX, moveY);
    }

    private void UpdateFacingDirection()
    {
        if (!playerShootingManager.IsShooting)
        {
            Vector2 fDir = new Vector2();
            if (moveX < 0.0f) fDir.x = -1;
            else if (moveX > 0.0f) fDir.x = 1;
            else if (moveX == 0.0f) fDir.x = 0;

            if (moveY < 0.0f) fDir.y = -1;
            else if (moveY > 0.0f) fDir.y = 1;
            else if (moveY == 0.0f) fDir.y = 0;

            facingDirection = fDir.normalized;
        }
    }

    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }

    private void HandlePlayerTurning()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (!playerShootingManager.IsShooting)
            direction = GetFacingDirection();
        else
        {
            direction = new Vector2(playerWeaponManager.GetTargetPos().x - transform.position.x,
                                playerWeaponManager.GetTargetPos().y - transform.position.y).normalized;
        }

        HandlePlayerAnimation(direction.x,direction.y);
    }

    private void HandlePlayerAnimation(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;
        if (x > 0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (x < 0)
            tempScale.x =  -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;

        x = Mathf.Abs(x);

        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, y);

        ActivateWeaponForSide(x, y);
    }

    private void ActivateWeaponForSide(float x, float y)
    {
        if (x == 1f && y == 0f)
            playerWeaponManager.ActivateGun(0);
        else if (x == 0f && y == 1f)
            playerWeaponManager.ActivateGun(1);
        else if (x == 0f && y == -1f)
            playerWeaponManager.ActivateGun(2);
        else if (x == 1f && y == 1f)
            playerWeaponManager.ActivateGun(3);
        else if (x == 1f && y == -1f)
            playerWeaponManager.ActivateGun(4);
    }
}
