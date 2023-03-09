using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] private float bound_X = 0.3f, bound_Y = 0.15f;
    
    private Transform playerTarget;
    private Vector3 deltaPos;
    private float delta_X, delta_Y;

    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void LateUpdate()
    {
        if (!playerTarget)
            return;

        deltaPos = Vector3.zero;

        UpdateCameraAxis(ref deltaPos.x, ref delta_X, ref bound_X, playerTarget.position.x, transform.position.x);
        UpdateCameraAxis(ref deltaPos.y, ref delta_Y, ref bound_Y, playerTarget.position.y, transform.position.y);

        deltaPos.z = 0f;
        transform.position += deltaPos;
    }

    private void UpdateCameraAxis(  ref float deltaPos,
                                    ref float delta, 
                                    ref float bound,
                                    float playerTargetPosition,
                                    float cameraPosition)
    {
        delta = playerTargetPosition - cameraPosition;
        if (delta > bound || delta < -bound)
        {
            if (cameraPosition < playerTargetPosition)
                deltaPos = delta - bound;
            else
                deltaPos = delta + bound;
        }
    }
}
