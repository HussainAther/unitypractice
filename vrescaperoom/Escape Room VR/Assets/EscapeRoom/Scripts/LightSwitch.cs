using UnityEngine;
using VRTK;
public class LightSwitch : VRTK_InteractableObject {
    public GameObject LightsParent;

    private Light[] lights;

    private void Start()
    {
        if (LightsParent)
            lights = LightsParent.GetComponentsInChildren<Light>();
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);

        // TODO: Play animation
        toggleLights();
    }

    private void toggleLights()
    {
        foreach(var light in lights)
            light.enabled = !light.enabled;
    }
}