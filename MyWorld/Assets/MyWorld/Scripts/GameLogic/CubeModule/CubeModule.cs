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

        CubeInfoStore = ReadCubeInfoes();//��ȡ��ͼ��Ϣ
        GenerateTerrain(SceneManager.GetActiveScene().name);//���ɵ�ͼ
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

    #region �������
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
        Destroy(cubeRoot.gameObject);//TODO ��һ֡ɾ����������������cubeRoot��Ϊnull���Ƿ������⣿
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

    #region ����

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
