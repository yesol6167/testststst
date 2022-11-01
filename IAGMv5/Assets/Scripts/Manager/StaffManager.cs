using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public GameObject[] Staffs;

    private void OnSmileIcon()
    {
        // 스마일 생성
        GameObject iconarea = GameObject.Find("IconArea");
        GameObject obj = Instantiate(Resources.Load("IconPrefabs/SmileIcon"), iconarea.transform) as GameObject;

        int i = obj.GetComponent<SmileIcon>().myTarget.GetComponent<Host>().purpose; // 호스트의 방문목적 검사
        obj.GetComponent<SmileIcon>().myTarget = Staffs[i].GetComponent<Staff>().myIconZone; // 해당구역의 스태프가 스마일 아이콘을 생성한다.
    }
}
