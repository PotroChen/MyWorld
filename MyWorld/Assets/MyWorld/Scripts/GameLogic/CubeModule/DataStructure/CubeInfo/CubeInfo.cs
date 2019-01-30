using UnityEngine;

public class CubeInfo{

    public Vector3Int Position { get; set; }


    public CubeInfo()
    {
        Position = Vector3Int.zero;
    }

    public CubeInfo(int x, int y, int z)
    {
        Position = new Vector3Int(x, y, z);
    }

    public static bool operator ==(CubeInfo left, CubeInfo right)
    {
        if (left.Position == right.Position)
            return true;
        else
            return false;
    }

    public static bool operator !=(CubeInfo left, CubeInfo right)
    {
        if (left.Position == right.Position)
            return false;
        else
            return true;
    }

    public bool Equals(CubeInfo cubeInfo)
    {
        if (Position == cubeInfo.Position)
            return true;
        else
            return false;
    }
}
