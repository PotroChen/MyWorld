using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

[QMonoSingletonPath("[GameLogic]/GameMgr")]
public class GameMgr : QMgrBehaviour, ISingleton
{

    public CubeInfo3DArray cubeInfoStore = new CubeInfo3DArray();

    public override int ManagerId
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public void OnSingletonInit()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        ResMgr.Init();
        UIManager.Instance.Show();



        Cursor.visible = false;
        UIManager.Instance.OpenUI("Aim", UILevel.Common);

        Player creator = Creator.Born(new Vector3(0f,0f,-10f),Quaternion.identity);
        if (cubeInfoStore.Count == 0)
        {
            cubeInfoStore.Add(new CubeInfo2DArray());
            cubeInfoStore[0].Add(new CubeInfoArray());
            cubeInfoStore[0][0].Add(Cube.Create().CubeInfo);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}