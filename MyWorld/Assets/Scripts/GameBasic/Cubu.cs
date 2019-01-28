﻿using UnityEngine;

//Cubu应该数据和行为在一起吗？
class Cube:MonoBehaviour
{
    private CubeInfo m_CubeInfo;
    public CubeInfo CubeInfo
    {
        get { return m_CubeInfo; }
        set { m_CubeInfo = value; }
    }

    public static Cube Create()
    {
        return Create(new CubeInfo());
    }

    public static Cube Create(CubeInfo cubeInfo)
    {
        GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube cube = cubeObj.AddComponent<Cube>();
        cube.m_CubeInfo = cubeInfo;
        return cube;
    }

    public void UpdateState()
    {
        transform.position = new Vector3(m_CubeInfo.X, m_CubeInfo.Y, m_CubeInfo.Z);
    }
}
