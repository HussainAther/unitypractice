using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform reflectionSurface; // the reflective surface
    private Camera mirrorCamera;
    private RenderTexture renderTexture;

    private void Start()
    {
        mirrorCamera = GetComponent<Camera>();
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mirrorCamera.targetTexture = renderTexture;
        reflectionSurface.GetComponent<Renderer>().material.mainTexture = renderTexture;
    }

    private void Update()
    {
        // Mirror the camera's position and rotation
        Vector3 cameraPositionInMirrorSpace = reflectionSurface.InverseTransformPoint(mirrorCamera.transform.position);
        cameraPositionInMirrorSpace.y *= -1;
        mirrorCamera.transform.position = reflectionSurface.TransformPoint(cameraPositionInMirrorSpace);
        Quaternion cameraRotationInMirrorSpace = Quaternion.Inverse(reflectionSurface.rotation) * mirrorCamera.transform.rotation;
        cameraRotationInMirrorSpace.z *= -1;
        cameraRotationInMirrorSpace.w *= -1;
        mirrorCamera.transform.rotation = reflectionSurface.rotation * cameraRotationInMirrorSpace;
    }
}
