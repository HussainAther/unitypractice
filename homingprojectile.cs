using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the projectile
    public float homingStrength = 1.0f; // How strongly the projectile homes in on the target
    public GameObject target; // The target GameObject for the projectile to home in on

    private Rigidbody rb; // Rigidbody component of the projectile

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component of the projectile
    }

    void FixedUpdate()
    {
        // Calculate the direction to the target
        Vector3 targetDirection = target.transform.position - transform.position;

        // Calculate the rotation required to face the target
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Use a mathematical matrix to interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, homingStrength * Time.fixedDeltaTime);

        // Move the projectile forward
        rb.velocity = transform.forward * speed;
    }
}
