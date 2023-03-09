using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float speed = 1f;
    private float acceleration = 0.05f;
    private float maxSpeed = 3.2f;

    private readonly float easySpeed = 3.4f;
    private readonly float mediumSpeed = 3.8f;
    private readonly float hardSpeed = 4.2f;

    [HideInInspector]
    public bool moveCamera;

    void Start()
    {
        if(GamePreferences.GetEasyDifficulty() == 1){ maxSpeed = easySpeed; }
        else if(GamePreferences.GetEasyDifficulty() == 1){ maxSpeed = mediumSpeed; }
        else if(GamePreferences.GetEasyDifficulty() == 1){ maxSpeed = hardSpeed; }
        moveCamera = true;
    }

    void Update()
    {
        if (moveCamera)
        {
            MoveCamera();
        }
    }
    private void MoveCamera()
    {
        Vector3 temp = transform.position;
        float oldY = temp.y;
        float newY = temp.y - (speed * Time.deltaTime);
        temp.y = Mathf.Clamp(temp.y, oldY, newY);
        transform.position = temp;

        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed)
            speed = maxSpeed;
    }
}
