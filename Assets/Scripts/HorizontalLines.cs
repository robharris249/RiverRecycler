using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLines : MonoBehaviour {
    public void DrawHorizontalLines(int gridWidth, int gridHeight, float cellSize) {

        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        var mesh = new Mesh();
        var verticies = new List<Vector3>();

        var indicies = new List<int>();

        for (int y = 0; y <= gridHeight; y++) {

            verticies.Add(new Vector3(0, y * cellSize, 0));
            verticies.Add(new Vector3(gridWidth * cellSize, y * cellSize, 0));

            indicies.Add((2 * y + 0));
            Debug.Log((2 * y + 0));
            indicies.Add((2 * y + 1));
            Debug.Log((2 * y + 0));
        }

        mesh.vertices = verticies.ToArray();
        mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = Color.white;
    }
}
