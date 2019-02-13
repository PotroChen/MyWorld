using QFramework;
using UnityEngine;
using CubeModule;

public class GameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ResMgr.Init();
        CubeMgr.Instance.Init();
        
        GameMgr.Instance.Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
