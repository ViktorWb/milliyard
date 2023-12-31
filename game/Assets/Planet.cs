using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int LevelOfDetail;

    public MeshFilter MeshFilter;

    void Update()
    {
        MeshFilter.mesh.Clear();

        MeshFilter.mesh = GenerateIcosahedron();
    }

    private Mesh GenerateIcosahedron()
    {
        var phi = (float)(1.0f + Math.Sqrt(5.0f)) * 0.5f;
        var a = 1.0f;
        var b = 1.0f / phi;

        var vertices = new List<Vector3>();
        var triangles = new List<(int P1, int P2, int P3)>();

        vertices.Add(new(0, b, -a));
        vertices.Add(new(b, a, 0));
        vertices.Add(new(-b, a, 0));
        vertices.Add(new(0, b, a));
        vertices.Add(new(0, -b, a));
        vertices.Add(new(-a, 0, b));
        vertices.Add(new(0, -b, -a));
        vertices.Add(new(a, 0, -b));
        vertices.Add(new(a, 0, b));
        vertices.Add(new(-a, 0, -b));
        vertices.Add(new(b, -a, 0));
        vertices.Add(new(-b, -a, 0));

        triangles.Add((2, 1, 0));
        triangles.Add((1, 2, 3));
        triangles.Add((5, 4, 3));
        triangles.Add((4, 8, 3));
        triangles.Add((7, 6, 0));
        triangles.Add((6, 9, 0));
        triangles.Add((11, 10, 4));
        triangles.Add((10, 11, 6));
        triangles.Add((9, 5, 2));
        triangles.Add((5, 9, 11));
        triangles.Add((8, 7, 1));
        triangles.Add((7, 8, 10));
        triangles.Add((2, 5, 3));
        triangles.Add((8, 1, 3));
        triangles.Add((9, 2, 0));
        triangles.Add((1, 7, 0));
        triangles.Add((11, 9, 6));
        triangles.Add((7, 10, 6));
        triangles.Add((5, 11, 4));
        triangles.Add((10, 8, 4));

        return new Mesh()
        {
            vertices = vertices.ToArray(),
            triangles = triangles.SelectMany((triangle) => new[] { triangle.P1, triangle.P2, triangle.P3 }).ToArray()
        };
    }
}
