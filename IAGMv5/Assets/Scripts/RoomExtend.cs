using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomExtend : MonoBehaviour
{
    SpawnManager Furniture; //가구
    public GameObject RoomExtendBtn; // 방 증축 버튼
    public GameObject PubChairBtn; // 식당 의자 증축 버튼
    public GameObject[] Rooms;
    public GameObject[] PubChairs;
    public int RoomsCount = 0;
    public int PubCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Furniture = GameObject.FindObjectOfType<SpawnManager>();
    }
    public void RExtend() //방 증축
    {
        if (Furniture._bedSlot.Count < 6)
        {
            Furniture._bedSlot.Add(SpawnManager.BedSlot.None);
            Rooms[RoomsCount].SetActive(true);
            RoomsCount++;
        }


    }

    public void PubExtend() //식당 증축
    {
        if (Furniture._chairSlot.Count < 11)
        {
            for (int i = 0; i < 2; i++)
            {
                Furniture._chairSlot.Add(SpawnManager.ChairSlot.None);
            }
            PubChairs[PubCount].SetActive(true);
            PubCount++;
        }
    }

}