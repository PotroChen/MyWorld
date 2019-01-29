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
}
