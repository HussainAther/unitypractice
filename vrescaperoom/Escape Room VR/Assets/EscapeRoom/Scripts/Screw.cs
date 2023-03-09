using UnityEngine;
using VRTK;

public class Screw : MonoBehaviour {

    private ScrewedPlate screwPlate;

    private float screwOffset = 0.075f;
    private bool unscrewed = false;
    private float speed = 0.1f;
    private BoxCollider screwPartCollider;

    protected void Start()
    {
        screwPlate = transform.parent.gameObject.GetComponent<ScrewedPlate>();
        var colliders = gameObject.GetComponentsInChildren<BoxCollider>();

        foreach (var collider in colliders)
        {
            if (collider.name == "ScrewedPart")
            {
                screwPartCollider = collider;
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "AerationGrid")
            Unscrewed();
    }

    public bool IsScrewed()
    {
        return !unscrewed;
    }

    public void Unscrew(float deltaTime)
    {
        if (unscrewed)
            return;

        transform.Translate(new Vector3(0f, -speed * deltaTime, 0f));
        //transform.Rotate(new Vector3(0f, 0f, 90f * deltaTime));
    }

    private void Unscrewed()
    {
        //var savedParentPosition = transform.parent.position;
        //transform.SetParent(transform.parent.transform.parent.transform, true);
        //transform.position -= savedParentPosition;

        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        rigidBody.AddTorque(Random.Range(-60f, 60f), Random.Range(-60f, 60f), Random.Range(-60f, 60f));
        rigidBody.AddForce(0f, Random.Range(0f, 10f), 0f);
        unscrewed = true;

        screwPlate.OnUnscrew(this);
    }

    private void SetParent(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
