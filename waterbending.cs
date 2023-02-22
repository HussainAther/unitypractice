using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterBending : MonoBehaviour
{ 
    [SerializeField] private XRController controller;

    private ParticleSystem waterParticleSystem;

    private void Start()
    {
        waterParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        Vector3 handPosition = controller.transform.position;
        Quaternion handRotation = controller.transform.rotation;
    
        bool isHandClosed = CheckIfHandIsClosed(controller.inputDevice);
       
        if (isHandClosed)
        {
            Vector3 waveDirection = handRotation * Vector3.forward;
            float waveForce = 10f;
           
            StartCoroutine(CreateWave(waveDirection, waveForce, 0.5f));
        }
    }

    private bool CheckIfHandISClosedBool(InputDevice inputDevice)
    {
         throw new NotImplementedException();
    }
}  
