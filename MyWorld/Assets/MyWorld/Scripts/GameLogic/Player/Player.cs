using CubeModule;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float TranslateSpeed = 1f;
    public float AngleSpeed = 2f;

    protected GameObject target;
    protected new Camera camera;

    protected virtual void Init()
    {
        camera = gameObject.AddComponent<Camera>();
        ResLoader resLoader = new ResLoader();

        GameObject targetPre = resLoader.LoadSync<GameObject>("Target");
        target = GameObject.Instantiate(targetPre);

        target.SetActive(false);
    }

    private void Update()
    {
        Translate();
        Rotate();
        Interact();
    }

    //移动
    protected virtual void Translate()
    {

    }

    //旋转
    protected virtual void Rotate()
    {

    }

    //互动键 使用道具，攻击对话等
    protected virtual void Interact()
    {
        
    }
}
