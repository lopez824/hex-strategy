using UnityEngine;

public static class HexData
{
    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;    // Value derived using Pythagorean theorem (height of one of six triangles)

    // positions of the six corners of a hexagon, starting with pointy side up, first corner is wrapped for simpler array indexing
    public static Vector3[] corners =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };
}
