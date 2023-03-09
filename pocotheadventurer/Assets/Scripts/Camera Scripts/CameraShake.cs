using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount = 0.007f;

    private Vector3 camPos;
    private float cameraShakingOffset_X, cameraShakingOffset_Y;
    
    public void ShakeCamera(float shakeTime)
    {
        InvokeRepeating("StartCameraShaking", 0f, 0.01f);
        Invoke("StopCameraShaking", shakeTime);
    }

    private void StartCameraShaking()
    {
        if(shakeAmount > 0)
        {
            camPos = transform.position;
            cameraShakingOffset_X = (Random.value * shakeAmount * 2 - shakeAmount)/ 1.5f;
            cameraShakingOffset_Y = (Random.value * shakeAmount * 2 - shakeAmount)/ 1.5f;

            camPos.x += cameraShakingOffset_X;
            camPos.y += cameraShakingOffset_Y;

            transform.position = camPos;
        }
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        //transform.localPosition = Vector3.zero;
    }
}
