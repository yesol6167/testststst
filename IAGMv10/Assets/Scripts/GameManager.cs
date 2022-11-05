using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public class GameManager : Singleton<GameManager>
{
  
    public int Gold;
    public int Fame;
    // Start is called before the first frame update


    public void Update()
    {
        UIManager.Instance.Gold.text = Gold.ToString();
        UIManager.Instance.Fame.text = Fame.ToString();
    }

    public void GoTitle() //타이틀 화면으로 가기
    {
        SceneManager.LoadScene(0);
    }

    public void GoMain() //메인화면으로 가기
    {
        SceneManager.LoadScene(1);
    }
}
