using UnityEngine; //  example script that implements the RRT (Rapidly-exploring Random Tree) motion planning algorithm in Unity using C#:
using System.Collections.Generic;

public class RRTMotionPlanner : MonoBehaviour
{
    public GameObject startNodePrefab;
    public GameObject goalNodePrefab;
    public GameObject obstaclePrefab;
    public GameObject pathSegmentPrefab;
    public int maxIterations = 1000;
    public float stepSize = 1f;
    public float goalBias = 0.1f;
    public float obstacleRadius = 0.5f;
    public float pathSegmentWidth = 0.2f;
    public Color pathSegmentColor = Color.blue;

    private GameObject startNode;
    private GameObject goalNode;
    private List<GameObject> obstacles;
    private List<GameObject> pathSegments;

    void Start()
    {
        // Create start and goal nodes
        startNode = Instantiate(startNodePrefab, new Vector3(-5f, 0f, 0f), Quaternion.identity);
        goalNode = Instantiate(goalNodePrefab, new Vector3(5f, 0f, 0f), Quaternion.identity);

        // Create obstacles
        obstacles = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-4f, 4f);
            float y = Random.Range(-2.5f, 2.5f);
            obstacles.Add(Instantiate(obstaclePrefab, new Vector3(x, y, 0f), Quaternion.identity));
        }

        // Initialize path segments
        pathSegments = new List<GameObject>();
    }

    void Update()
    {
        // Generate a random configuration
        Vector3 randomConfig = new Vector3(Random.Range(-5f, 5f), Random.Range(-2.5f, 2.5f), 0f);

        // Find the nearest node in the tree
        GameObject nearestNode = startNode;
        float nearestDistance = Vector3.Distance(startNode.transform.position, randomConfig);
        foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            float distance = Vector3.Distance(node.transform.position, randomConfig);
            if (distance < nearestDistance)
            {
                nearestNode = node;
                nearestDistance = distance;
            }
        }

        // Extend the tree towards the random configuration
        Vector3 direction = (randomConfig - nearestNode.transform.position).normalized;
        Vector3 newNodePosition = nearestNode.transform.position + direction * stepSize;
        if (!CollisionCheck(nearestNode.transform.position, newNodePosition))
        {
            GameObject newNode = Instantiate(startNodePrefab, newNodePosition, Quaternion.identity);
            newNode.tag = "Node";
            newNode.transform.parent = transform;
            newNode.GetComponent<Node>().parent = nearestNode;

            // Check if the goal has been reached
            if (Vector3.Distance(newNode.transform.position, goalNode.transform.position) < stepSize)
            {
                // Reconstruct the path
                GameObject currentNode = newNode;
                while (currentNode.GetComponent<Node>().parent != null)
                {
                    GameObject parent = currentNode.GetComponent<Node>().parent;
                    GameObject pathSegment = Instantiate(pathSegmentPrefab, parent.transform.position, Quaternion.identity);
                    pathSegment.transform.LookAt(currentNode.transform.position);
                    pathSegment.transform.localScale = new Vector3(pathSegmentWidth, pathSegmentWidth, Vector3.Distance(parent.transform.position, currentNode.transform.position));
                    pathSegment.GetComponent<Renderer>().material.color = pathSegmentColor;
                    pathSegments.Add(pathSegment);
                    currentNode = parent;
                }

                // Reverse the path segments so they're in order
                pathSegments.Reverse();

                // Clear the obstacles
                foreach (GameObject obstacle in obstacles)
               

