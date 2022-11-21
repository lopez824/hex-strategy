using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, z;

    public int X { get { return x; } }
    public int Z { get { return z; } }
    public int Y { get { return -X - Z; } }     // used for cube coordinates
    public HexCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    // Offsets coordinate system to align along a hexagon's axis
    public static HexCoordinates FromOffset(int x, int z)
    {
        return new HexCoordinates(x - z/2, z);
    }

    // Override to nicely format coordinate data
    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringNewLine()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}
