using UnityEngine;
using VRTK;

public class Screwdriver : VRTK_InteractableObject
{
    float spinSpeed = 0f;
    Transform rotator;
    private GameObject screwTip;
    private Screw currentScrewComponent;

    protected void Start()
    {
        screwTip = GameObject.Find("ScrewTip");
        rotator = screwTip.transform;
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        spinSpeed = 360f;
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {
        base.StopUsing(usingObject);
        spinSpeed = 0f;
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Screw>())
        {
            currentScrewComponent = collider.gameObject.GetComponent<Screw>();
            screwTip.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Screw")
        {
            currentScrewComponent = null;
            screwTip.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    protected override void Update()
    {
        base.Update();

        rotator.transform.Rotate(new Vector3(0f, spinSpeed * Time.deltaTime, 0f));

        if (currentScrewComponent && IsUsing())
            currentScrewComponent.Unscrew(Time.deltaTime);
    }
}
