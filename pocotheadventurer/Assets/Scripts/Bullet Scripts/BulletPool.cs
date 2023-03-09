using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance = null;
    public static BulletPool Instance { get { return instance; } }

    [SerializeField] private GameObject[] bullets;

    private List<Bullet> pistolBullets = new List<Bullet>();
    private List<Bullet> matterBullets = new List<Bullet>();
    private List<Bullet> laserBullets = new List<Bullet>();
    private List<Bullet> flameBullets = new List<Bullet>();
    
    private bool bulletSpawned;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        
    }
    
    public void FireBullet(int bulletIndex, Vector3 spawnPosition, Quaternion bulletRotation, Vector2 bulletDirection)
    {
        bulletSpawned = false;
        TakeBulletFromPool(bulletIndex, spawnPosition, bulletRotation, bulletDirection);
    }

    private List<Bullet> GetBulletListByIndex(int bulletIndex)
    {
        switch (bulletIndex)
        {
            case 0: return pistolBullets; break;
            case 1: return matterBullets; break;
            case 2: return laserBullets; break;
            case 3: return flameBullets; break;
        }
        return null;
    }

    void TakeBulletFromPool(int bulletIndex, Vector3 spawnPosition, Quaternion bulletRotation, Vector2 bulletDirection)
    {
        List<Bullet> currentBulletList = GetBulletListByIndex(bulletIndex);

        foreach ( Bullet bullet in currentBulletList)
        {
            if(!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                bullet.gameObject.transform.position = spawnPosition;
                bullet.gameObject.transform.rotation = bulletRotation;
                bullet.MoveInDirection(bulletDirection);

                bulletSpawned = true;
                break;
            }
        }
        
        if(!bulletSpawned)
        {
            CreateNewBullet(bulletIndex, spawnPosition, bulletRotation, bulletDirection);
        }
    }

    private void CreateNewBullet(int bulletIndex, Vector3 spawnPosition, Quaternion bulletRotation, Vector2 bulletDirection)
    {
        GameObject newBullet = Instantiate( bullets[bulletIndex],
                                            spawnPosition,
                                            bulletRotation);
        newBullet.transform.SetParent(transform);
        newBullet.GetComponent<Bullet>().MoveInDirection(bulletDirection);

        List<Bullet> bulletList = GetBulletListByIndex(bulletIndex);
        bulletList.Add(newBullet.GetComponent<Bullet>());
    }
}
