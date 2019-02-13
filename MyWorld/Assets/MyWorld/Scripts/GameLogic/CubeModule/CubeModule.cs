using Newtonsoft.Json;
using QFramework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CubeModule
{
    [QMonoSingletonPath("[GameLogic]/CubeModule")]
    public class CubeMgr : QMgrBehaviour, ISingleton
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

        private Dictionary<string, CubeInfo> CubeInfoStore;//<Key,Value> = <X-Y-Z,CubeInfo>
        private static readonly string fileStorePath = FilePath.PersistentDataPath + "/TerrainInfo/";


        public override void Init()
        {
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;

            CubeInfoStore = ReadCubeInfoes(SceneManager.GetActiveScene().name);//读取地图信息

            GenerateTerrain(SceneManager.GetActiveScene().name);//生成地图
        }

        #region OnSceneLoaded/OnSceneUnLoaded
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            cubeRoot = null;
            ReadCubeInfoes(scene.name);
            GenerateTerrain(scene.name);
        }

        private void OnSceneUnLoaded(Scene scene)
        {
            SaveCubeInfoes(scene.name);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                SaveCubeInfoes(SceneManager.GetActiveScene().name);
        }

        private void OnApplicationQuit()
        {
            SaveCubeInfoes(SceneManager.GetActiveScene().name);
        }
        #endregion

        #region 地形相关
        private void GenerateTerrain(string sceneName)
        {
            bool infoEmpty = false;

            if (CubeInfoStore == null)
                infoEmpty = true;

            if (infoEmpty)
                Cube.Create();
            else
            {
                foreach (var kvp in CubeInfoStore)
                {
                    Cube.Create(kvp.Value);
                }
            }
        }

        private void DestroyCurrentTerrain()
        {
            Destroy(cubeRoot.gameObject);//TODO 下一帧删除，但是我马上让cubeRoot变为null，是否又问题？
            cubeRoot = null;
        }

        public void GenerateCube(Vector3Int position)
        {
            Cube cube = Cube.Create(position);
            CubeMgr.Instance.RegisterCubeInfo(cube);
        }

        public void DeleteCube(Cube cube)
        {
            if (CubeInfoStore.Count == 1)
            {
                Debug.LogWarning("Can't delete all");
                return;
            }
            Cube.Destroy(cube);
            CubeMgr.Instance.UnRegisterCubeInfo(cube);
        }
        #endregion

        private Dictionary<string, CubeInfo> ReadCubeInfoes(string sceneName)
        {
            Dictionary<string, CubeInfo> cubeInfoes = null;
            string readPath = fileStorePath + sceneName + ".json";

            try
            {
                using (StreamReader file = File.OpenText(readPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    cubeInfoes = (Dictionary<string, CubeInfo>)serializer.Deserialize(file, typeof(Dictionary<string, CubeInfo>));
                }
                Debug.Log("ReadCubeInfo Succeed!SceneName:" + sceneName);
            }
            catch (System.Exception e)
            {
                Debug.LogError("ReadCubeInfo Failed,Exception:" + e.ToString());
            }

            return cubeInfoes;
        }

        private void SaveCubeInfoes(string sceneName)
        {
            string outputPath = fileStorePath + sceneName + ".json";

            if (!Directory.Exists(fileStorePath))
                Directory.CreateDirectory(fileStorePath);

            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(outputPath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, CubeInfoStore);
                }
                Debug.Log("SaveCubeInfoes Succeed!SceneName:" + sceneName);
            }
            catch (System.Exception e)
            {
                Debug.LogError("SaveCubeInfoes Failed,Exception:" + e.ToString());
            }

        }

        private void RegisterCubeInfo(Cube cube)
        {
            CubeInfo cubeInfo = new CubeInfo();
            cubeInfo.Position = cube.Position;

            if (CubeInfoStore == null)
                CubeInfoStore = new Dictionary<string, CubeInfo>();

            string key = string.Format("{0}-{1}-{2}", cubeInfo.Position.x, cubeInfo.Position.y, cubeInfo.Position.z);

            if (CubeInfoStore.ContainsKey(key))
                CubeInfoStore[key] = cubeInfo;
            else
                CubeInfoStore.Add(key, cubeInfo);
        }

        private void UnRegisterCubeInfo(Cube cube)
        {
            CubeInfo cubeInfo = new CubeInfo();
            cubeInfo.Position = cube.Position;

            if (CubeInfoStore == null)
                return;

            string key = string.Format("{0}-{1}-{2}", cubeInfo.Position.x, cubeInfo.Position.y, cubeInfo.Position.z);

            if (CubeInfoStore.ContainsKey(key))
            {
                CubeInfoStore.Remove(key);
            }
        }

        #region 单例

        private static CubeMgr mInstance;

        public static CubeMgr Instance
        {
            get
            {
                if (null == mInstance)
                {
                    mInstance = FindObjectOfType<CubeMgr>();
                }

                if (null == mInstance)
                {
                    mInstance = MonoSingletonProperty<CubeMgr>.Instance;
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
}

