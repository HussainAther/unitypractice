using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponManager[] playerWeapons;

    private int weaponIndex;

    [SerializeField] private GameObject[] weaponBullets;
    private Vector2 targetPos;
    private Vector2 direction;
    private Camera mainCam;
    private Vector2 bulletSpawnPosition;
    private Quaternion bulletRotation;
    private CameraShake cameraShake;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float cameraShakeCooldown = 0.1f;

    //[SerializeField] private GameObject bullet;

    private void Awake()
    {
        weaponIndex = 0;
        playerWeapons[weaponIndex].gameObject.SetActive(true);
        mainCam = Camera.main;
        cameraShake = mainCam.GetComponent<CameraShake>();
    }

    public void ActivateGun(int gunIndex)
    {
        playerWeapons[weaponIndex].ActivateGun(gunIndex); 
    }

    public void ChangeWeapon()
    {
        playerWeapons[weaponIndex].gameObject.SetActive(false);
        weaponIndex++;
        if (weaponIndex == playerWeapons.Length)
            weaponIndex = 0;
        playerWeapons[weaponIndex].gameObject.SetActive(true);
    }

    public Vector2 GetTargetPos()
    {
        return targetPos;
    }

    public void Shoot(Vector3 spawnPos, bool isJoystickTouched)
    {
        if(!isJoystickTouched)
            targetPos = mainCam.ScreenToWorldPoint(Input.touches[0].position);
        else
            targetPos = mainCam.ScreenToWorldPoint(Input.touches[1].position);

        bulletSpawnPosition = new Vector2(spawnPos.x, spawnPos.y);
        direction = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        //direction = playerMovement.GetFacingDirection();

        bulletRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        /*
        GameObject newBullet = Instantiate( weaponBullets[weaponIndex], 
                                            spawnPos, 
                                            bulletRotation);

        newBullet.GetComponent<Bullet>().MoveInDirection(direction);
        */

        BulletPool.Instance.FireBullet( weaponIndex,spawnPos,bulletRotation,direction);

        cameraShake.ShakeCamera(cameraShakeCooldown);
    }
}
