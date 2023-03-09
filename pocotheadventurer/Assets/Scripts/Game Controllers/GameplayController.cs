using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public bool runOnMobile = false;

    void Awake()
    {
        
    }

    private void Start()
    {
        ConfigureRuntimePlatform();
    }

    void ConfigureRuntimePlatform()
    {
        runOnMobile = ( Application.platform == RuntimePlatform.Android ||
                        Application.platform == RuntimePlatform.IPhonePlayer) ? true : false;
        runOnMobile = true;
        if (!runOnMobile)
            GameObject.Find("Fixed Joystick").gameObject.SetActive(false);
    }
}
