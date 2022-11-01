using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class SmileIcon : MonoBehaviour
{
    public Transform myTarget;
    public GameObject myIcon;

    float SmileTime;

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        // mySmileCreateZone의 3차원 위치값을 pos라고 한다.

        transform.position = pos; // pos는 나의 위치값이 된다.(2차원으로 변형됨)

        // 스마일 1.5초후 제거
        SmileTime += Time.deltaTime;
        if (SmileTime > 1.5f)
        {
            Destroy(myIcon);
        }
    }
}
