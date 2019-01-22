using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public CubeInfo3DArray cubeInfoStore = new CubeInfo3DArray(); 

    private void Awake()
    {
        Cursor.visible = false;

        Player creator = Creator.Born();
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