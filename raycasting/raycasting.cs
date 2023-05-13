using UnityEngine;

public class ShootIntoGround : MonoBehaviour
{
    public float raycastDistance = 10.0f;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Cast a ray from the camera into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, raycastDistance))
            {
                // Instantiate the bullet at the hit point
                GameObject bullet = Instantiate(bulletPrefab, hitInfo.point, Quaternion.identity);

                // Make the bullet face upwards
                bullet.transform.up = hitInfo.normal;

                // Destroy the bullet after a delay
                Destroy(bullet, 5.0f);
            }
        }
    }
}

