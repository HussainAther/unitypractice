using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    public GameObject waterObject; // The GameObject representing the water

    private bool isInteracting; // Flag to indicate if the user is interacting with the water
    private Vector3 previousMousePosition; // The previous position of the mouse

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over the water object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == waterObject)
            {
                // Start interacting with the water
                isInteracting = true;
                previousMousePosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Stop interacting with the water
            isInteracting = false;
        }

        if (isInteracting)
        {
            // Calculate the displacement of the mouse
            Vector3 displacement = Input.mousePosition - previousMousePosition;

            // Move the vertices of the water mesh based on the displacement
            MeshFilter meshFilter = waterObject.GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += new Vector3(displacement.x, displacement.y, 0.0f) * 0.01f;
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();

            // Save the current mouse position as the previous position
            previousMousePosition = Input.mousePosition;
        }
    }
}
