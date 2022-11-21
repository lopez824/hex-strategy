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

    public static HexCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / (HexData.innerRadius * 2f);
        float y = -x;

        float offset = position.z / (HexData.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int i_x = Mathf.RoundToInt(x);
        int i_y = Mathf.RoundToInt(y);
        int i_z = Mathf.RoundToInt(-x - y);

        // handle rounding errors
        if (i_x + i_y + i_z != 0)
        {
            float dX = Mathf.Abs(x - i_x);
            float dY = Mathf.Abs(y - i_y);
            float dZ = Mathf.Abs(-x - y - i_z);

            if (dX > dY && dX > dZ)
                i_x = -i_y - i_z;

            else if (dZ > dY)
                i_z = -i_x - i_y;
        }

        return new HexCoordinates(i_x, i_z);
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
