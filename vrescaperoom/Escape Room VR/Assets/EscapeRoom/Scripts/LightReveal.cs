using UnityEngine;

[ExecuteInEditMode]
public class LightReveal : MonoBehaviour {

    public Material lightReveal;
    public Light light;

	// Use this for initialization
	void Start ()
    {
	}

	// Update is called once per frame
	void Update ()
    {
        lightReveal.SetVector("_LightPosition", light.transform.position);
        lightReveal.SetVector("_LightDirection", -light.transform.forward);
        lightReveal.SetFloat("_LightAngle", light.spotAngle);
    }
}
