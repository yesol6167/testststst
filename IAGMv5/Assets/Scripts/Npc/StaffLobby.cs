using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StaffLobby : MonoBehaviour
{
    public Transform myIconZone;
    SmileIcon myUI = null;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreateSmile()
    {
        // 스마일 생성
        GameObject iconarea = GameObject.Find("IconArea");
        GameObject obj = Instantiate(Resources.Load("IconPrefabs/SmileIcon"), iconarea.transform) as GameObject;
        myUI = obj.GetComponent<SmileIcon>();
        myUI.myTarget = myIconZone;

    }

}
