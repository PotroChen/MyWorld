using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Creator : Player
{
    public float speed =1f;
    public float angleSpeed = 2f;

    private Camera camera;
    private GameObject target;

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

        Creator playerCom = player.AddComponent<Creator>();
        playerCom.Init();

        return playerCom;
    }

    protected override void Init()
    {
        camera = gameObject.AddComponent<Camera>();
        ResLoader resLoader = new ResLoader();

        GameObject targetPre =  resLoader.LoadSync<GameObject>("Target");
        target = GameObject.Instantiate(targetPre);

        target.SetActive(false);
    }

    private void Update()
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
        Ray ray =camera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 10f, 1 << LayerMask.NameToLayer("Cube")))
        {
            Debug.DrawLine(ray.origin, raycastHit.point, Color.green);

            target.transform.position = raycastHit.transform.position + raycastHit.normal * 1f;
            target.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = target.transform.position;
                Cube.Create(new CubeInfo(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z)));

            }


        }
        else
        {
            target.SetActive(false);
        }

        #endregion

    }
}
