using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PubMotelIcon : MonoBehaviour // Bed + Meat + Sleep + Eat 아이콘
{
    public Transform myIconZone;
    public GameObject myHost;

    // Chair&Bed Check
    ChairBedChk bedchairvalue;
    public bool Pullchk;

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myIconZone.position);
        transform.position = pos;
    }
    public void delete()
    {
        Destroy(gameObject);
        GameManager.Instance.Gold += 100;
    }

    public void ChairChk()
    {
        bedchairvalue = FindObjectOfType<ChairBedChk>();

        
        for(int i = 0; i < bedchairvalue.chairSlot.Count; i++)
        { 
            if(bedchairvalue.chairSlot[i] == ChairBedChk.ChairSlot.Check) // 자리가 전부 꽉 찼을 때
            {
                Pullchk = true;
            }
            else 
            {
                Pullchk = false;
                break;
            }
        }
        if(Pullchk == true)
        {
            myHost.GetComponent<Host>().onAngry = true;
        }
    }

    public void BedChk()
    {
        bedchairvalue = FindObjectOfType<ChairBedChk>();

        for (int i = 0; i < bedchairvalue.bedSlot.Count; i++)
        {
            if (bedchairvalue.bedSlot[i] == ChairBedChk.BedSlot.Check) 
            {
                Pullchk = true;
            }
            else 
            {
                Pullchk = false;
                break;
            }
        }
        if (Pullchk == true)
        {
            myHost.GetComponent<Host>().onAngry = true;
        }
    }
}
