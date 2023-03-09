using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour
{
    private GameObject[] pipeHolders;
    private float distance = 3.0f;
    private float lastPipeX;
    private float pipeMin = -1.5f;
    private float pipeMax = 2.4f;

    private void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");
        
        foreach(GameObject pipeHolder in pipeHolders)
        {
            Vector3 temp = pipeHolder.transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolder.transform.position = temp;
        }

        lastPipeX = pipeHolders[0].transform.position.x;
        for (int i = 1; i < pipeHolders.Length; i++)
            if (lastPipeX < pipeHolders[i].transform.position.x)
                lastPipeX = pipeHolders[i].transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "PipeHolder")
        {
            Vector3 temp = target.transform.position;
            temp.x = lastPipeX + distance;
            temp.y = Random.Range(pipeMin, pipeMax);

            target.transform.position = temp;
            lastPipeX = temp.x;
        }
    }
}
