using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] clouds;

    private float distanceBetweenClouds = 3f;
    private float minX, maxX;
    private float lastCloudPositionY;
    private int controlX;

    private GameObject player;

    [SerializeField]
    private GameObject[] collectables;


    private void Awake()
    {
        controlX = 0;
        SetMinAndMaxX();
        CreateClouds();
        player = GameObject.Find("Player");

        for(int i = 0; i <collectables.Length; i++)
        {
            collectables[i].SetActive(false);
        }
    }
 
    private void Start()
    {
        PositionThePlayer();
    }

    private void SetMinAndMaxX() 
    {
        Vector3 sceneBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        maxX = sceneBounds.x - 0.5f;
        minX = -sceneBounds.x + 0.5f;

    }

    private void Shuffle(GameObject[] arrayToShuffle)
    {
        for (int i = 0; i < arrayToShuffle.Length; i++)
        {
            GameObject temp = arrayToShuffle[i];
            int random = Random.Range(i, arrayToShuffle.Length);
            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = temp;
        }
    }

    private void ShuffleClouds(GameObject[] arrayToShuffle)
    {
        Shuffle(arrayToShuffle);

        // preventing from the occur of two dark clouds in a row
        for (int i = 1; i < arrayToShuffle.Length; i++)
        {
            if(arrayToShuffle[i].tag == "Deadly")
            {
                if(arrayToShuffle[i-1].tag == "Deadly")
                {
                    if (i < arrayToShuffle.Length - 2)
                    {
                        GameObject temp = arrayToShuffle[i];
                        arrayToShuffle[i] = arrayToShuffle[arrayToShuffle.Length - 1];
                        arrayToShuffle[arrayToShuffle.Length - 1] = temp;
                    }
                    else
                    {
                        GameObject temp = arrayToShuffle[i];
                        arrayToShuffle[i] = arrayToShuffle[1];
                        arrayToShuffle[1] = temp;
                    }
                }
            }
        }
    }

    private void CreateClouds()
    {
        ShuffleClouds(clouds);

        float positionY = 0;

        for(int i = 0; i < clouds.Length; i++)
        {
            Vector3 temp = clouds[i].transform.position;
            temp.y = positionY;
            
            if(controlX == 0)
            {
                temp.x = Random.Range(0.0f, maxX);
                controlX = 1;
            }
            else if(controlX == 1)
            {
                temp.x = Random.Range(0.0f, minX);
                controlX = 2;
            }
            else if (controlX == 2)
            {
                temp.x = Random.Range(1.0f, maxX);
                controlX = 3;
            }
            else if (controlX == 3)
            {
                temp.x = Random.Range(-1.0f, minX);
                controlX = 0;
            }

            lastCloudPositionY = positionY;
            clouds[i].transform.position = temp;
            positionY -= distanceBetweenClouds;
        }
    }

    private void PositionThePlayer()
    {
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag("Cloud");

        for (int i = 0; i < darkClouds.Length; i++)
        {
            if(darkClouds[i].transform.position.y == 0f)
            {
                // if the first shuffled cloud is dark then it has to be swapped with some clean cloud
                Vector3 tempPos = darkClouds[i].transform.position;
                darkClouds[i].transform.position = new Vector3( cloudsInGame[0].transform.position.x,
                                                                cloudsInGame[0].transform.position.y,
                                                                cloudsInGame[0].transform.position.z);
                cloudsInGame[0].transform.position = tempPos;
            } 
        }

        // looking for the cloud with the highest Y value
        Vector3 temp = cloudsInGame[0].transform.position;
        for(int i = 0; i < cloudsInGame.Length; i++)
        {
            if(temp.y < cloudsInGame[i].transform.position.y)
                temp = cloudsInGame[i].transform.position;
        }
        // positioning the player on top of it
        temp.y += 0.8f;
        player.transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Cloud" || target.tag == "Deadly")
        {
            if (target.transform.position.y == lastCloudPositionY)
            {
                ShuffleClouds(clouds);
                Shuffle(collectables);

                Vector3 temp = target.transform.position;

                for (int i = 0; i < clouds.Length; i++)
                {
                    if (!clouds[i].activeInHierarchy)
                    {
                        if (controlX == 0)
                        {
                            temp.x = Random.Range(0.0f, maxX);
                            controlX = 1;
                        }
                        else if (controlX == 1)
                        {
                            temp.x = Random.Range(0.0f, minX);
                            controlX = 2;
                        }
                        else if (controlX == 2)
                        {
                            temp.x = Random.Range(1.0f, maxX);
                            controlX = 3;
                        }
                        else if (controlX == 3)
                        {
                            temp.x = Random.Range(-1.0f, minX);
                            controlX = 0;
                        }
                        temp.y -= distanceBetweenClouds;
                        lastCloudPositionY = temp.y;
                        clouds[i].transform.position = temp;
                        clouds[i].SetActive(true);

                        int random = Random.Range(0, collectables.Length);
                        if(clouds[i].tag != "Deadly")
                        {
                            if(!collectables[random].activeInHierarchy)
                            {
                                Vector3 temp2 = clouds[i].transform.position;
                                temp2.y += 0.7f;
                                if(collectables[random].tag == "Life" && PlayerScore.lifeCount < 2)
                                {
                                    collectables[random].transform.position = temp2;
                                    collectables[random].SetActive(true);
                                }
                                else if(collectables[random].tag == "Coin")
                                {
                                    collectables[random].transform.position = temp2;
                                    collectables[random].SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
