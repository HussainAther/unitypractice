using UnityEngine;
using VRTK;

public class ChangePhysicsOnInteraction : MonoBehaviour
{
    public bool Enable = true;
    public bool OnTouch;
    public bool OnUntouch;
    public bool OnGrab;
    public bool OnUngrab;

    private Rigidbody rigidBody = null;

	void Start ()
    {
        if (GetComponent<VRTK_InteractableObject>() == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "ChangePhysicsOnInteraction", "VRTK_InteractableObject"));
            return;
        }

        // Setup object event listeners
        GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += DoInteractTouch;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched += DoInteractUntouch;
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += DoInteractGrab;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += DoInteractUngrab;

        rigidBody = GetComponent<Rigidbody>();

        if (!rigidBody)
            VRTK_Logger.Error("No RigidBody found on this GameObject!");
    }

    private void ChangePhysics()
    {
        if (rigidBody)
            rigidBody.isKinematic = !Enable;
    }

    private void DoInteractTouch(object sender, InteractableObjectEventArgs e)
    {
        if (!OnTouch)
            return;

        ChangePhysics();
    }

    private void DoInteractUntouch(object sender, InteractableObjectEventArgs e)
    {
        if (!OnUntouch)
            return;

        ChangePhysics();
    }

    private void DoInteractGrab(object sender, InteractableObjectEventArgs e)
    {
        if (!OnGrab)
            return;

        ChangePhysics();
    }

    private void DoInteractUngrab(object sender, InteractableObjectEventArgs e)
    {
        if (!OnUngrab)
            return;

        ChangePhysics();
    }
}
