using UnityEngine;
using VRTK;

public class DigicodeButton : VRTK_InteractableObject
{
    public GameObject Digicode;

    private string labelText;
    private Digicode digicodeScript;

    protected virtual void Awake()
    {
        if (Application.isPlaying)
        {
            InitRequiredComponents();
        }
    }

    protected void Start()
    {
        labelText = GetComponentInChildren<TextMesh>().text;

        if (Digicode != null)
            digicodeScript = Digicode.GetComponent<Digicode>();
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);

        if (digicodeScript)
            digicodeScript.ButtonPressed(labelText);
    }

    protected void InitRequiredComponents()
    {
        if (!GetComponent<Collider>())
            gameObject.AddComponent<BoxCollider>();
    }
}
