using UnityEngine;

public class LightMapsManager : MonoBehaviour {
    void Start () {
        TurnLightMapsOff();
    }

    private void TurnLightMapsOff()
    {
        var children = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in children)
        {
            var originalLightMapIndex = child.lightmapIndex;
            Debug.Log("COUCOU: " + child.lightmapIndex);
            child.lightmapIndex = 255;
        }
    }
}
