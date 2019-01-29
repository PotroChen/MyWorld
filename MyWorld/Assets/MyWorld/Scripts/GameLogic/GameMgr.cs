using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

[QMonoSingletonPath("[GameLogic]/GameMgr")]
public class GameMgr : QMgrBehaviour, ISingleton
{
    //public CubeInfo3DArray cubeInfoStore = new CubeInfo3DArray();

    public override int ManagerId
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public override void Init()
    {
        base.Init();
        Cursor.visible = false;
        UIManager.Instance.OpenUI("Aim", UILevel.Common);
        Player creator = Creator.Born(new Vector3(0f, 0f, -10f), Quaternion.identity);
    }

    #region 单例

    private static GameMgr mInstance;

    public static GameMgr Instance
    {
        get
        {
            if (null == mInstance)
            {
                mInstance = FindObjectOfType<GameMgr>();
            }

            if (null == mInstance)
            {
                mInstance = MonoSingletonProperty<GameMgr>.Instance;
                mInstance.name = "GameMgr";
                DontDestroyOnLoad(mInstance);
            }

            return mInstance;
        }
    }

    public void OnSingletonInit()
    {
        Log.I("GameMgr.OnSingletonInit");
    }
    #endregion
}