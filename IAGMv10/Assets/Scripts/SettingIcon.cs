using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingIcon : MonoBehaviour
{
    public GameObject ESCWindow; //ESC 누르면 나오는 창
    public GameObject CheckWindow; // 그만하기 누르면 나오는 창
    public GameObject SettingWindow; // ESC Window 안의 설정 아이콘 누르면 나오는 설정 창
    public GameObject NoTouch;
    public bool IsOpen = false;
    public bool ESCClick = false;
    TimeBtns Time;
   
    // Start is called before the first frame update
    void Start()
    {
        ESCWindow.SetActive(false);
        SettingWindow.SetActive(false);
        CheckWindow.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape) && ESCClick == true)
         {
            ESCWindow.SetActive(true);
            ESCClick = !ESCClick;
         }
        else if(Input.GetKeyDown(KeyCode.Escape) && ESCClick == false)
        {
            ESCWindow.SetActive(false);
            ESCClick = !ESCClick;
        }
       
    }
   
    public void OpenESCWindow() // 메인 ui의 설정아이콘 누르면 창 뜨게하기
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            ESCWindow.SetActive(true);
        }
        else
        {
            ESCWindow.SetActive(false);
        }
    }

    public void OpenSettingWindow()  // ESC Window 안의 설정 아이콘 누르면 창 뜨게하기
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            SettingWindow.SetActive(true);
        }
        else
        {
            SettingWindow.SetActive(false);
        }
    }

    public void OpenReCheckWindow() // 그만하기 버튼 누르면 다시 물어보는 창 뜨게
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            CheckWindow.SetActive(true);
            NoTouch.SetActive(true);
            Time = FindObjectOfType<TimeBtns>();
            Time.OnPause(); //창 뜨면 일시정지

        }
        else
        {
            CheckWindow.SetActive(false);
        }

    }
    public void SettingExit() //설정 창 나가기
    {
        SettingWindow.SetActive(false);
    }

    public void ESCExit() //저장하기 창 나가기
    {
        ESCWindow.SetActive(false);
    }

    public void ClickNo()
    {
        CheckWindow.SetActive(false);
        Time.OnPlay();
        NoTouch.SetActive(false);
    }
}
