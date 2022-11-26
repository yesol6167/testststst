using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    public GameObject LoadingText;
    public GameObject AnyKeyText;

    // Update is called once per frame
    void Update()
    {
        if(SceneChangeManager.Inst.ao.progress >= 0.9f) // 씬 로딩이 완료 되었을 때
        {
            LoadingText.GetComponent<Animator>().SetTrigger("OnIdle");
            LoadingText.GetComponent<TMP_Text>().text = "로딩완료!";
            AnyKeyText.SetActive(true);
        }
    }
}
