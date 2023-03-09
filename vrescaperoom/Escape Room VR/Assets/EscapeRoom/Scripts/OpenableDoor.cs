using UnityEngine;
using VRTK;

public class OpenableDoor : VRTK_InteractableObject
{
    public bool flipped = false;
    public bool rotated = false;
    public GameObject doorObject = null;

    private float sideFlip = -1;
    private float side = -1;
    private float smooth = 270.0f;
    private float doorOpenAngle = -90f;
    private bool open = false;

    private Vector3 defaultRotation;
    private Vector3 openRotation;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        SetDoorRotation(usingObject.transform.position);
        SetRotation();
        open = !open;
    }

    protected void Start()
    {
        if (doorObject)
            defaultRotation = doorObject.transform.eulerAngles;
        else
            defaultRotation = transform.eulerAngles;

        SetRotation();
        sideFlip = (flipped ? 1 : -1);
    }

    protected override void Update()
    {
        base.Update();

        if (open)
        {
            if (doorObject)
                doorObject.transform.rotation = Quaternion.RotateTowards(doorObject.transform.rotation, Quaternion.Euler(openRotation), Time.deltaTime * smooth);
            else
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(openRotation), Time.deltaTime * smooth);
        }
        else
        {
            if (doorObject)
                doorObject.transform.rotation = Quaternion.RotateTowards(doorObject.transform.rotation, Quaternion.Euler(defaultRotation), Time.deltaTime * smooth);
            else
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(defaultRotation), Time.deltaTime * smooth);
        }
    }

    public void Open()
    {
        open = !open;
    }

    private void SetRotation()
    {
        openRotation = new Vector3(defaultRotation.x, defaultRotation.y + (doorOpenAngle * (sideFlip * side)), defaultRotation.z);
    }

    private void SetDoorRotation(Vector3 interacterPosition)
    {
        if (doorObject)
            side = ((rotated == false && interacterPosition.z > doorObject.transform.position.z) || (rotated == true && interacterPosition.x > doorObject.transform.position.x) ? -1 : 1);
        else
            side = ((rotated == false && interacterPosition.z > transform.position.z) || (rotated == true && interacterPosition.x > transform.position.x) ? -1 : 1);
    }
}