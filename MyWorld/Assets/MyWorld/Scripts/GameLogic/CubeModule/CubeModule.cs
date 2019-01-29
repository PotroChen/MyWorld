using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[QMonoSingletonPath("[GameLogic]/CubeModule")]
public class CubeModule : QMgrBehaviour, ISingleton
{
    public override int ManagerId
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    private Transform cubeRoot;
    public Transform CubeRoot
    {
        get
        {
            if (cubeRoot == null)
            {
                cubeRoot = new GameObject("CubeRoot").transform;
                cubeRoot.transform.SetParent(this.transform);
            }
            return cubeRoot;
        }
    }

    private Dictionary<string, List<CubeInfo>> CubeInfoStore;

    public override void Init()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;

        CubeInfoStore = ReadCubeInfoes();//读取地图信息
        GenerateTerrain(SceneManager.GetActiveScene().name);//生成地图
    }

    #region OnSceneLoaded/OnSceneUnLoaded
    private void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        cubeRoot = null;
        GenerateTerrain(scene.name);
    }

    private void OnSceneUnLoaded(Scene scene)
    {
        SaveCubeInfoes();
    }
    #endregion

    #region 地形相关
    private void GenerateTerrain(string sceneName)
    {
        bool infoEmpty = false;

        if (CubeInfoStore == null)
            infoEmpty = true;
        else if (!CubeInfoStore.ContainsKey(sceneName))
            infoEmpty = true;

        if (infoEmpty)
            Cube.Create();
        else
        {
            List<CubeInfo> terrainInfo = CubeInfoStore[sceneName];
            foreach (var cubeinfo in terrainInfo)
            {
                Cube.Create(cubeinfo);
            }
        }


    }

    private void DestroyCurrentTerrain()
    {
        Destroy(cubeRoot.gameObject);//TODO 下一帧删除，但是我马上让cubeRoot变为null，是否又问题？
        cubeRoot = null;
    }
    #endregion

    private Dictionary<string, List<CubeInfo>> ReadCubeInfoes()
    {
        return null;
    }

    private void SaveCubeInfoes()
    {

    }

    public void RegisterCubeInfo(Cube cube)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        CubeInfo cubeInfo = new CubeInfo();
        cubeInfo.Position = cube.Position;

        if (!CubeInfoStore.ContainsKey(sceneName))
            CubeInfoStore.Add(sceneName, new List<CubeInfo>());

        CubeInfoStore[sceneName].Add(cubeInfo);
    }

    public void UnRegisterCubeInfo(Cube cube)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        CubeInfo cubeInfo = new CubeInfo();
        cubeInfo.Position = cube.Position;

        if (!CubeInfoStore.ContainsKey(sceneName))
            return;

        if (!CubeInfoStore[sceneName].Contains(cubeInfo))
            return;

        CubeInfoStore[sceneName].Remove(cubeInfo);
    }

    #region 单例

    private static CubeModule mInstance;

    public static CubeModule Instance
    {
        get
        {
            if (null == mInstance)
            {
                mInstance = FindObjectOfType<CubeModule>();
            }

            if (null == mInstance)
            {
                mInstance = MonoSingletonProperty<CubeModule>.Instance;
                mInstance.name = "CubeModule";
                DontDestroyOnLoad(mInstance);
            }

            return mInstance;
        }
    }

    public void OnSingletonInit()
    {
        Log.I("CubeModule.OnSingletonInit");
    }
    #endregion


}
