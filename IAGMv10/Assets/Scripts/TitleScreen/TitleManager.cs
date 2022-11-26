using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TitleManager : MonoBehaviour
{
    public GameObject DetailSettingWindow;
    public GameObject SaveLoadWindow;
    public bool IsOpen = false;
    public Color UnActiveColor;

    //로드게임 버튼 관련 변수
    public Image LodeBtn;
    public TMP_Text LodeBtnText;
    public GameObject LodeNotouch;

    private void Start()
    {
        if (!File.Exists(DataManager.Inst.pathSave)) // save 파일이 없으면 로드버튼 비활성화
        {
            LodeBtn.color = UnActiveColor;
            LodeBtnText.color = UnActiveColor;
            LodeNotouch.SetActive(true);
        }

        SoundSet();
    }

    public void NewGame()
    {
        SceneChangeManager.Inst.ChangeScene("Tutorial1");
    }

    public void LoadGame()
    {
        SaveLoadWindow.SetActive(true);
    }

    public void OpenOption()  // 세부설정 창 뜨게하기
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            DetailSettingWindow.SetActive(true);
        }
        else
        {
            SoundSet();
            DetailSettingWindow.SetActive(false);
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }

    void SoundSet()
    {
        DetailSettingWindow.GetComponent<SettingWindow>().BGM.volume = PlayerPrefs.GetFloat("volume");
        DetailSettingWindow.GetComponent<SettingWindow>().BGMvolume.value = PlayerPrefs.GetFloat("volume");
        foreach (AudioSource source in DetailSettingWindow.GetComponent<SettingWindow>().Eff)
        {
            source.volume = PlayerPrefs.GetFloat("eff");
        }
        DetailSettingWindow.GetComponent<SettingWindow>().Effvolume.value = PlayerPrefs.GetFloat("eff");
        DetailSettingWindow.transform.localPosition = DetailSettingWindow.GetComponent<SettingWindow>().orgPos;
    }
}
