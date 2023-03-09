using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float offsetX;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(BirdScript.instance != null)
        {
            if (BirdScript.instance.isAlive)
                MoveTheCamera();
        }
    }

    private void MoveTheCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetXPosition() + offsetX;
        transform.position = temp;
    }
}
