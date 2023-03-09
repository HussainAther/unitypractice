using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Scaling the background to the camera size 
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        float width = spriteRenderer.sprite.bounds.size.x;
        float worldHeight = Camera.main.orthographicSize * 2.0f;
        float worldWidth = worldHeight / Screen.height * Screen.width;
           
        tempScale.x = worldWidth / width;
        transform.localScale = tempScale;
    }
}
