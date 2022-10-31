using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

public class Extend : MonoBehaviour
{
    SpawnManager Furniture; //가구
    public TMP_Text ChairCountTxt;
    public TMP_Text BedCountTxt;
    int ChairCount;
    int RoomCount;
    public GameObject ExtendIcon;
    public GameObject ExtendWindow;
    public bool ExtendActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        Furniture = GameObject.FindObjectOfType<SpawnManager>();
        ExtendWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ChairCount = Furniture._chairSlot.Count; //의자 개수 가져오기
        RoomCount = Furniture._bedSlot.Count; //침대 개수 가져오기
        ChairCountTxt.text = "현재 테이블 개수 : " + (ChairCount * 0.5).ToString() + "개";
        BedCountTxt.text = "현재 방 개수 : " + RoomCount.ToString() + "개";
    }

    //증축 아이콘 누르면 증축 창 뜨게
    public void TryOpenExtendWindow()
    {
        ExtendActivated = !ExtendActivated;

        if (ExtendActivated) OpenExtendWindow();
        else CloseExtendWindow();
    }

    public void OpenExtendWindow()
    {
        ExtendWindow.SetActive(true);
    }

    public void CloseExtendWindow()
    {
        ExtendWindow.SetActive(false);
    }
}
