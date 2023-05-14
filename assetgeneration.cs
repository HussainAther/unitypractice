using UnityEngine;

public class AssetGenerator : MonoBehaviour
{
    void Start()
    {
        // Create a new mesh
        Mesh mesh = new Mesh();

        // Define the vertex positions for the mesh
        Vector3[] vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1)
        };

        // Define the triangles for the mesh
        int[] triangles = new int[] {
            0, 1, 2,
            1, 3, 2
        };

        // Assign the vertex positions and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Create a new asset for the mesh
        AssetDatabase.CreateAsset(mesh, "Assets/NewMesh.asset");

        // Save the asset database
        AssetDatabase.SaveAssets();
    }
}
