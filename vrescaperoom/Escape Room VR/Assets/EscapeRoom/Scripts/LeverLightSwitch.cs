using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class LeverLightSwitch : MonoBehaviour {

    private VRTK_Control_UnityEvents controlEvents;
    public Light light;

    private void Start()
    {
        controlEvents = GetComponent<VRTK_Control_UnityEvents>();
        if (controlEvents == null)
            controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();

        controlEvents.OnValueChanged.AddListener(HandleChange);
    }

    private void HandleChange(object sender, Control3DEventArgs e)
    {
        if (e.normalizedValue == 100)
            light.enabled = true;
        else if (e.normalizedValue == 0)
            light.enabled = false;
    }
}
