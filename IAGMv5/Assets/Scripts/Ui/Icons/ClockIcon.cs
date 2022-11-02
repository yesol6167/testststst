using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ClockIcon : MonoBehaviour
{
    delegate void Delegate(); // 기본 함수 대행 
    delegate void DelegateS(string IconName); // 문자열 함수 대행

    Delegate deQuestIcon;
    Delegate deAngryIcon;
    Delegate deStaffIcon;
    DelegateS deMandBIcon;

    public GameObject myNotouch;

    public GameObject myHost;
    public Transform myTarget;
    public Button myButton;

    public Image myTimeArea;
    public float LimitTime = 30.0f; // 제한시간 30초

    public bool TimeOut = false;
    public GameObject myAngryIcon;

    // Start is called before the first frame update
    void Start()
    {
        deQuestIcon = myHost.GetComponent<Host>().StartCoQi;
        deAngryIcon = myHost.GetComponent<Host>().StartCoAi;
        deMandBIcon = myHost.GetComponent<Host>().StartCoMandB;

        StartCoroutine(TimeChecking());
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        transform.position = pos;
    }
    IEnumerator TimeChecking()
    {
        myTimeArea.fillAmount = 0.0f;
        float speed = 1.0f / LimitTime;
        while (myTimeArea.fillAmount < 1.0f)
        {
            myTimeArea.fillAmount += speed * Time.deltaTime;
            if (myTimeArea.fillAmount == 1.0f)
            {
                TimeOut = true;
                deAngryIcon();
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
    public void OnRemveClock()
    {
        Destroy(gameObject);
    }
    public void OnIcon() 
    {
        switch(myHost.GetComponent<Host>().purpose)
        {
            case 0: // 방문 목적이 로비일때
                deQuestIcon();
                break;
            case 1: // 방문 목적이 식당일때
                deMandBIcon("MeatIcon");
                break;
            case 2: // 방문 목적이 여관일때
                deMandBIcon("BedIcon");
                break;
        }
    }
    public void OnStaffSmile()
    {
        Debug.Log(myHost.GetComponent<Host>().myStaff);
        deStaffIcon = myHost.GetComponent<Host>().myStaff.GetComponent<Staff>().OnSmileIcon;
        deStaffIcon();
    }
}