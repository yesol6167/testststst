using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomExtend : MonoBehaviour
{
    ChairBedChk Furniture; //가구
    public GameObject RoomExtendBtn; // 방 증축 버튼
    public GameObject PubChairBtn; // 식당 의자 증축 버튼
    public GameObject[] Rooms;
    public GameObject[] PubChairs;
    public int RoomsCount = 0;
    public int PubCount = 0;
    int Price = -1000; // 가격

    void Start()
    {
        Furniture = GameObject.FindObjectOfType<ChairBedChk>();
    }
    public void RExtend() //방 증축
    {
        
        if(GameManager.Instance.Gold >= -Price)
        {
            if (Furniture._bedSlot.Count < 5)
            {
                Furniture._bedSlot.Add(ChairBedChk.BedSlot.None);
                Rooms[RoomsCount].SetActive(true);
                RoomsCount++;
                GameManager.Instance.ChangeGold(Price);
            }
        }
        else
        {
            UIManager.Instance.ExtendFail = true;
        }
    }

    public void PubExtend() //식당 증축
    {
        if (GameManager.Instance.Gold >= -Price)
        {
            if (Furniture._chairSlot.Count < 11)
            {
                for (int i = 0; i < 2; i++)
                {
                    Furniture._chairSlot.Add(ChairBedChk.ChairSlot.None);
                }
                PubChairs[PubCount].SetActive(true);
                PubCount++;
                GameManager.Instance.ChangeGold(Price);
            }
        }
        else
        {
            UIManager.Instance.ExtendFail = true;
        }
    }
}