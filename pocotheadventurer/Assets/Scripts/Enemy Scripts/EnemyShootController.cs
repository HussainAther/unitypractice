using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType
{
    Normal,
    Spread,
    SlowSpread
}

public class EnemyShootController : MonoBehaviour
{
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private int numberOfBullets = 3;
    [SerializeField] private EnemyBulletType bulletType = EnemyBulletType.Spread;
    [SerializeField] private float bulletSpeed = 1f;

    public void ShootOnRandom(Vector3 direction, Vector3 origin)
    {
        int randomShoot = Random.Range(0, 3);

        switch(randomShoot)
        {
            case 0: SpreadShoot(direction, origin, false);  break;
            case 1: SpreadShoot(direction, origin, true);   break;
            case 2: NormalShoot(direction, origin);         break;
        }
    }

    public void Shoot(Vector3 direction, Vector3 origin)
    {
        if (bulletType == EnemyBulletType.Spread)
            SpreadShoot(direction, origin, false);
        else if (bulletType == EnemyBulletType.SlowSpread)
            SpreadShoot(direction, origin, true);
        else if (bulletType == EnemyBulletType.Normal)
            NormalShoot(direction, origin);
    }

    private void SpreadShoot(Vector3 direction, Vector3 origin, bool isBulletSlow)
    {
        float offset = 0.5f;

        for(int i = 0; i < numberOfBullets; i++)
        {
            Quaternion rot = Quaternion.Euler(  0, 0, 
                                                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            GameObject bullet = Instantiate(enemyBullet, origin, rot);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed / 7f;
            bullet.GetComponent<DevilBullet>().SetSlow(isBulletSlow);

            direction.x += Random.Range(-offset, offset);
            direction.y += Random.Range(-offset, offset);
        }
    }

    private void NormalShoot(Vector3 direction, Vector3 origin)
    {
        float offset = 0.5f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 0,
                                                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            GameObject bullet = Instantiate(enemyBullet, origin, rot);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed /4f;

            direction.x += Random.Range(-offset, offset);
            direction.y += Random.Range(-offset, offset);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
