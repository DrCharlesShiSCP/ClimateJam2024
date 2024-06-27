using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavesimulator : MonoBehaviour
{
    public float waveSpeed = 1.0f;
    public float waveHeight = 0.5f;
    public float waveFrequency = 0.5f;

    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            vertex.y = Mathf.Sin(Time.time * waveSpeed + vertex.x * waveFrequency) * waveHeight;
            displacedVertices[i] = vertex;
        }

        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    }

    public float GetWaveHeight(Vector3 position)
    {
        return Mathf.Sin(Time.time * waveSpeed + position.x * waveFrequency) * waveHeight;
    }

    public Vector3 GetWaveNormal(Vector3 position)
    {
        float y = Mathf.Cos(Time.time * waveSpeed + position.x * waveFrequency) * waveHeight * waveFrequency;
        return new Vector3(0, y, -1).normalized;
    }
}
