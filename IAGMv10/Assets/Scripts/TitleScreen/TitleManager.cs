using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject DetailSettingWindow;
    public bool IsOpen = false;
    private void Start()
    {
        SoundSet();
    }
    public void OnNewGame()
    {
        SceneManager.LoadScene("Main");
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
