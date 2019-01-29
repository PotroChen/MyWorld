﻿using UnityEngine;
using QFramework;

//Cubu应该数据和行为在一起吗？
class Cube:MonoBehaviour
{
    public Vector3Int Position
    {
        get
        {
            Vector3Int position = Vector3Int.RoundToInt(transform.position);
            return position;
        }
        set
        {
            transform.position = value;
        }
    }

    public static Cube Create()
    {
        return Create(Vector3Int.zero);
    }

    public static Cube Create(CubeInfo cubeInfo)
    {
        return Create(cubeInfo.Position);
    }

    public static Cube Create(Vector3Int position)
    {
        ResLoader resLoader = new ResLoader();

        GameObject cubePrefab = resLoader.LoadSync("Cube") as GameObject;
        GameObject cubeObj = GameObject.Instantiate(cubePrefab, CubeModule.Instance.CubeRoot);

        Cube cube = cubeObj.AddComponent<Cube>();
        cube.Position = position;

        return cube;
    }
    
}
