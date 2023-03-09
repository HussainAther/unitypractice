using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private float shootingTimerLimit = 0.2f;
    private float shootingTimer;

    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private Joystick joystick;

    private Animator shootingAnimator;
    private PlayerWeaponManager playerWeaponManager;

    [SerializeField] private Button switchWeaponButton;
    public bool IsShooting { get; set; }

    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        shootingAnimator = bulletSpawnPos.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        HandleShooting();
    }

    public void HandleShooting()
    {
        if (Input.touchCount == 1 && joystick.IsTouched)
            return;

        else if (Input.touchCount == 1 && !joystick.IsTouched)
        {
            if (IsTouchOnButton(false))
                return;
            IsShooting = true;
            if (Time.time > shootingTimer)
            {
                shootingTimer = Time.time + shootingTimerLimit;
                shootingAnimator.SetTrigger(TagManager.SHOOT_ANIMATION_PARAMETER);
                CreateBullet(false);
            }

        }
        else if (Input.touchCount == 2 && joystick.IsTouched)
        {
            if (IsTouchOnButton(true))
                return;
            IsShooting = true;
            if (Time.time > shootingTimer)
            {
                shootingTimer = Time.time + shootingTimerLimit;
                shootingAnimator.SetTrigger(TagManager.SHOOT_ANIMATION_PARAMETER);
                CreateBullet(true);
            }

        }
        else IsShooting = false;
    }

    private bool IsTouchOnButton(bool isJoystickTouched)
    {
        if(isJoystickTouched)
        {
            if (Input.touches[1].position.x > switchWeaponButton.transform.position.x &&
                Input.touches[1].position.x < switchWeaponButton.transform.position.x + 170 &&
                Input.touches[1].position.y > switchWeaponButton.transform.position.y &&
                Input.touches[1].position.y < switchWeaponButton.transform.position.y + 170)
            {
                return true;
            }
        }
        else
        {
            if( Input.touches[0].position.x > switchWeaponButton.transform.position.x &&
                Input.touches[0].position.x < switchWeaponButton.transform.position.x + 170 &&
                Input.touches[0].position.y > switchWeaponButton.transform.position.y &&
                Input.touches[0].position.y < switchWeaponButton.transform.position.y + 170 )
            {
                return true;
            }
        }
        return false;
    }

    private void CreateBullet(bool isJoystickTouched)
    {
        playerWeaponManager.Shoot(bulletSpawnPos.position, isJoystickTouched);
    }
}
