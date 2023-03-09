using UnityEngine;
using VRTK;

public class ScrewedPlate : MonoBehaviour {

    private Screw[] Screws;
    private bool free = false;

	protected void Start ()
    {
        Screws = GetComponentsInChildren<Screw>();
	}

    public void OnUnscrew(Screw screw)
    {
        foreach (var s in Screws)
        {
            if (s.IsScrewed())
                return;
        }

        // All screws have been unscrewed
        Free();
    }

    private void Free()
    {
        if (free)
            return;

        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
        rigidBody.mass = 50f;

        var boxCollider = gameObject.GetComponent<BoxCollider>();
        boxCollider.isTrigger = false;

        var interactableObject = gameObject.AddComponent<VRTK_InteractableObject>();
        interactableObject.isGrabbable = true;
        interactableObject.touchHighlightColor = Color.yellow;

        free = true;
    }
}
