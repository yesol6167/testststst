using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLodeWindow : MonoBehaviour
{
    public TMP_Text[] Slots;
    public TMP_Text SelectedSlot;
    public string SavedSlot = "저장된 슬롯";

    bool[] savefiles = new bool[3];

    private void Start()
    {
        // 저장된 파일이 있는지 검사 -> 있으면 슬롯의 이름 바꿔주기
        for(int i = 0; i < Slots.Length; i++)
        {
            if(File.Exists(DataManager.Instance.path + $"{i}")) // 같은 문자로 된 파일의 이름이 있을 경우
            {
                savefiles[i] = true;
                DataManager.Instance.nowSlot = i;
                DataManager.Instance.LoadData();
                GameManager.Instance.Gold = DataManager.Instance.nowData.Gold;
                Slots[i].text = SavedSlot;
            }
        }
        DataManager.Instance.DataClear();
        // 불러오기 구현 완료 -> 예상오류: 3개다 저장된 파일일 경우 -> 3번째 슬롯의 돈이 불러와짐 -> 해결방법 가장 마지막으로 저장된 슬롯을 구분?
    }

    public void SelecteSlot(int num)
    {
        DataManager.Instance.nowSlot = num;
        SelectedSlot = Slots[num];
    }

    public void OnSave()
    {
        DataManager.Instance.SaveData(GameManager.Instance.Gold);
        SelectedSlot.text = SavedSlot;
    }

    public void OnLoad()
    {
        SceneManager.LoadScene(1);
    }


}
