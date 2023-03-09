using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyCollectable", 6f);        
    }

    private void DestroyCollectable()
    {
        gameObject.SetActive(false);
    }
}
