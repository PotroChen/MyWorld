using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ResMgr.Init();
        CubeModule.Instance.Init();
        
        GameMgr.Instance.Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
