using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public int Gold;
}

public class DataManager : Singleton<DataManager>
{
    public PlayerData nowData = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        path = Application.persistentDataPath + "/Save"; // 저장 경로
    }

    public void SaveData(int i)
    {
        nowData.Gold = i;
        string data = JsonUtility.ToJson(nowData); // 제이슨으로 변환
        File.WriteAllText(path + nowSlot.ToString(), data); // 제이슨을 저장
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString()); // 제이슨을 불러오기
        nowData = JsonUtility.FromJson<PlayerData>(data); // 제이슨 -> PlayerData 형식으로 변환
    }

    public void DataClear()
    {
        nowSlot = -1;
    }
}
