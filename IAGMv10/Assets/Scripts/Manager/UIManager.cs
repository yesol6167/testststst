using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    //10억부터 벗어남
    public TMP_Text Gold;
    public TMP_Text Fame;

    public bool ChangeGold = false; // 골드의 변화를 감지
    public bool ChangeFame = false;
    public GameObject GoldiIncrease; // 골드의 증감
    public GameObject FameIncrease; // 명성의 증감

    float time = 2.0f; // 보여지는 시간
    float orgtime = 2.0f; // 보여지고 나서 초기화 되는 값
    //time과orgtime은 값이 같아야함


    void Update()
    {
        if(ChangeGold) // 골드의 값이 변하면 변한 값을 몇초 동안 보여주고 사라짐
        {
            GoldiIncrease.SetActive(true);

            time -= Time.deltaTime;
            if (time < 0f) // 변한 값을 몇초 동안 보여주고 사라짐
            {
                GoldiIncrease.SetActive(false);
                ChangeGold = false;
                time = orgtime;
            }
        }

        if (ChangeFame) // 골드의 값이 변하면 변한 값을 몇초 동안 보여주고 사라짐
        {
            FameIncrease.SetActive(true);

            time -= Time.deltaTime;
            if (time < 0f) // 변한 값을 몇초 동안 보여주고 사라짐
            {
                GoldiIncrease.SetActive(false);
                ChangeFame = false;
                time = orgtime;
            }
        }
    }
}
