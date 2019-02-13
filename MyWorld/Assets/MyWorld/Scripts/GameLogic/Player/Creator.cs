using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeModule;

public class Creator : Player
{

    public static Player Born()
    {
        return Born(Vector3.zero, Quaternion.identity);
    }

    public static Player Born(Vector3 position, Quaternion rotation)
    {
        GameObject player = new GameObject("Creator");
        player.transform.position = position;
        player.transform.rotation = rotation;

        Creator playerCom = player.AddComponent<Creator>();
        playerCom.Init();

        return playerCom;
    }

    protected override void Translate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * TranslateSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * TranslateSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * TranslateSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * TranslateSpeed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * Time.deltaTime * TranslateSpeed;
        }

        if (Input.GetKey(KeyCode.X))
        {
            transform.position -= transform.up * Time.deltaTime * TranslateSpeed;
        }
    }
    //旋转
    protected override void Rotate()
    {
        //float mouseX = transform.Rotate() Input.GetAxis("Mouse X") * angleSpeed;
        float mouseX = Input.GetAxis("Mouse X") * AngleSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * AngleSpeed;

        Vector3 angles = transform.rotation.eulerAngles;

        // 欧拉角表示按照坐标顺序旋转，比如angles.x=30，表示按x轴旋转30°，dy改变引起x轴的变化
        angles.x = Mathf.Repeat(angles.x + 180f, 360f) - 180f;
        angles.y += mouseX;
        angles.x -= mouseY;
        //transform.forward += new Vector3(mouseX, mouseY, 0) *Time.deltaTime;
        transform.eulerAngles = angles;
    }

    protected override void Interact()
    {
        Ray ray = (camera as Camera).ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 10f, 1 << LayerMask.NameToLayer("Cube")))
        {
            Debug.DrawLine(ray.origin, raycastHit.point, Color.green);

            target.transform.position = raycastHit.transform.position + raycastHit.normal * 1f;
            target.SetActive(true);

            if (Input.GetMouseButtonDown(0))//TODO 鼠标左键放置方块或者物品（集体操作应该由手中得物品决定）
            {
                Vector3 pos = target.transform.position;
                CubeMgr.Instance.GenerateCube(new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z)));
            }

            if (Input.GetMouseButtonDown(1))//TODO 鼠标右键删除物品（可能需要去掉）
            {
                Cube cube = raycastHit.collider.GetComponent<Cube>();//TODO 是否改成根据坐标删除呢
                CubeMgr.Instance.DeleteCube(cube);
            }
        }
        else
        {
            target.SetActive(false);
        }
    }
}
