using UnityEngine;
using VRTK;

public class InteractableObject : VRTK_InteractableObject {
    public bool EnablePhysics;
    public bool StartPhysicsOnInteraction;
    public float RigidBodyMass;

    private Rigidbody rigidBody = null;

    protected override void Awake()
    {
        base.Awake();

        if (!StartPhysicsOnInteraction)
            AddRigidBody();
    }

    public void Start()
    {
        //ConnectInteractionSignals();
    }

    private void ConnectInteractionSignals()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += DoInteractTouch;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched += DoInteractUntouch;
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += DoInteractGrab;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += DoInteractUngrab;
    }

    private void DisconnectInteractionSignals()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectTouched -= DoInteractTouch;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched -= DoInteractUntouch;
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed -= DoInteractGrab;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed -= DoInteractUngrab;
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        AddRigidBody();

        base.Grabbed(currentGrabbingObject);
    }

    private void AddRigidBody()
    {
        if (EnablePhysics && !rigidBody)
        {
            rigidBody = gameObject.AddComponent<Rigidbody>();
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
            rigidBody.mass = RigidBodyMass;

            DisconnectInteractionSignals();
        }
    }

    private void DoInteractTouch(object sender, InteractableObjectEventArgs e)
    {
        if (!StartPhysicsOnInteraction)
            return;

        AddRigidBody();
    }

    private void DoInteractUntouch(object sender, InteractableObjectEventArgs e)
    {
        if (!StartPhysicsOnInteraction)
            return;

        AddRigidBody();
    }

    private void DoInteractGrab(object sender, InteractableObjectEventArgs e)
    {
        if (!StartPhysicsOnInteraction)
            return;

        AddRigidBody();
    }

    private void DoInteractUngrab(object sender, InteractableObjectEventArgs e)
    {
        if (!StartPhysicsOnInteraction)
            return;

        AddRigidBody();
    }
}
