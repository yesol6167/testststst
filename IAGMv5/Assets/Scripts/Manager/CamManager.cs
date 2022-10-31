using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CamManager : Singleton<CamManager>
{
    public Transform myAxis;
    public Transform myCam;

    public float MoveSpeedK = 1.0f; // 키보드 이동 스피드
    public float MoveSpeedM = 1.0f; // 마우스 이동 스피드
    public float ZoomSpeed = 1.0f;
    public float RotSpeed = 1.0f;

    //줌
    Vector3 zoom = Vector3.zero;
    float SetDist = 70f; // 초기 줌 거리는 70
    public Vector2 ZoomRange = new Vector2(10, 10); // 줌 제한

    //화면 회전
    Vector3 Rot = Vector3.zero;

    

    // Start is called before the first frame update
    void Start()
    {
        zoom = (myCam.position - myAxis.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //화면이동
        if(true)
        {
            // 키보드로 이동
            if (Input.GetKey(KeyCode.W))
            {
                myAxis.transform.Translate(Vector3.back * MoveSpeedK * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                myAxis.transform.Translate(Vector3.forward * MoveSpeedK * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                myAxis.transform.Translate(Vector3.right * MoveSpeedK * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myAxis.transform.Translate(Vector3.left * MoveSpeedK * Time.unscaledDeltaTime);
            }

            // 마우스로 이동
            if(Input.GetMouseButton(1))
            {
                Vector3 dir = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
                myAxis.transform.Translate(dir * MoveSpeedM * Time.unscaledDeltaTime);
            }
            
            //이동제한(맵에서 벗어나지 않게)
            float x = Mathf.Clamp(myAxis.transform.position.x, -68, 88); // 가로 제한 값
            float z = Mathf.Clamp(myAxis.transform.position.z, -47, 51); // 세로 제한 값

            myAxis.transform.position = new Vector3(x, myAxis.transform.position.y, z); // 가로,세로 제한 적용
        }

        //화면회전
        if (Input.GetKeyDown(KeyCode.Q))
        {
            myAxis.transform.Rotate(Vector3.up * RotSpeed); // 인스펙터에서 수정하려고 스피드는 다 //  넵            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            myAxis.transform.Rotate(Vector3.down * RotSpeed);            
        }


        //줌
        SetDist -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        SetDist = Mathf.Clamp(SetDist, ZoomRange.x, ZoomRange.y);
        myCam.position = myAxis.position + myAxis.rotation * zoom * SetDist;
    }
}
