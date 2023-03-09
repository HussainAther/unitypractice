using UnityEngine;
using VRTK;

public class UnlockDoorKey : MonoBehaviour {

    public GameObject Door;
    public float AnimationSpeed;
    public float Angle;

    private VRTK_SnapDropZone keyDropZone;
    private Vector3 defaultRotation;
    private Vector3 openRotation;
    private bool playingAnimation = false;

    void Start ()
    {
        //PlayKeyAnimation();

        if (Door)
        {
            keyDropZone = Door.GetComponentInChildren<VRTK_SnapDropZone>();
            keyDropZone.ObjectSnappedToDropZone += KeySnappedToDropZone;
        }
	}

    private void PlayKeyAnimation()
    {
        // TODO: Play sound
        defaultRotation = transform.eulerAngles;
        openRotation = new Vector3(defaultRotation.x, defaultRotation.y, defaultRotation.z) + Vector3.forward * Angle;

        playingAnimation = true;
    }

    private void OpenDoor()
    {
        var openableDoorScript = Door.GetComponent<OpenableDoor>();

        if (openableDoorScript)
            openableDoorScript.Open();
    }

    private void KeySnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        PlayKeyAnimation();
        // TODO: Not grabbable anymore
    }

    void Update ()
    {
        if (playingAnimation)
        {
            var openQuat = Quaternion.Euler(openRotation);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, openQuat, Time.deltaTime * AnimationSpeed);

            if (Quaternion.Dot(transform.rotation, openQuat) >= 1f)
            {
                playingAnimation = false;
                OpenDoor();
            }
        }
    }
}
