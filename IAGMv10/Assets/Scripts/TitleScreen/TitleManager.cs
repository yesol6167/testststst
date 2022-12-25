using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class TitleManager : MonoBehaviour
{
    public GameObject DetailSettingWindow;
    public GameObject SaveLoadWindow;
    public GameObject Tuto_YorN;
    public bool IsOpen = false;
    public Color ActiveColor;

    //로드게임 버튼 관련 변수
    public Image LodeBtn;
    public TMP_Text LodeBtnText;
    public GameObject LodeNotouch;
    bool[] isSave = new bool[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(Path.Combine(Application.dataPath, "Save", $"Save{i}.bin")))
            {
                isSave[i] = true;
            }
            else
            {
                isSave[i] = false;
            }
        }
        

        if (isSave[0] == true || isSave[1] == true || isSave[2] == true)
        {
            LodeBtn.color = ActiveColor;
            LodeBtnText.color = ActiveColor;
            LodeNotouch.SetActive(false);
        }

        SoundSet();
    }

    public void NewGame()
    {
        //SceneChangeManager.Inst.ChangeScene("Tutorial1");
        Tuto_YorN.SetActive(true);
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

    public void Tuto_Yes()
    {
        //로드 파일 삭제?
        SceneChangeManager.Instance.ChangeScene("Tutorial1");
    }

    public void Tuto_NO()
    {
        //로드 파일 삭제?
        SceneChangeManager.Instance.ChangeScene("Main");
    }
}
