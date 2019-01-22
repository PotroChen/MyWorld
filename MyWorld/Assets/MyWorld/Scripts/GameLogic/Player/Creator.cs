using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Creator : Player
{
    public float speed =1f;
    public float angleSpeed = 2f;
    private float lastSpeed;

    public static Player Born()
    {
        return Born(Vector3.zero, Quaternion.identity);
    }

    public static Player Born(Vector3 position, Quaternion rotation)
    {
        GameObject player = new GameObject("Creator");
        player.transform.position = position;
        player.transform.rotation = rotation;
        Player playerCom = player.AddComponent<Creator>();
        return playerCom;
    }

    public void Awake()
    {
        gameObject.AddComponent<Camera>();
    }

    public void Update()
    {
        #region 移动
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime *speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.X))
        {
            transform.position -= transform.up * Time.deltaTime * speed;
        }
        #endregion

        #region 转向
        //float mouseX = transform.Rotate() Input.GetAxis("Mouse X") * angleSpeed;
        float mouseX = Input.GetAxis("Mouse X") * angleSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * angleSpeed;

        Vector3 angles = transform.rotation.eulerAngles;

        // 欧拉角表示按照坐标顺序旋转，比如angles.x=30，表示按x轴旋转30°，dy改变引起x轴的变化
        angles.x = Mathf.Repeat(angles.x + 180f, 360f) - 180f;
        angles.y += mouseX;
        angles.x -= mouseY;
        //transform.forward += new Vector3(mouseX, mouseY, 0) *Time.deltaTime;
        transform.eulerAngles = angles;
        #endregion

        #region 发射射线



        #endregion

    }
}
