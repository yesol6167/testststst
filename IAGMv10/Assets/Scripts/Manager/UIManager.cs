using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    //10����� ���
    public TMP_Text Gold;
    public TMP_Text Fame;

    public bool ChangeGold = false; // ����� ��ȭ�� ����
    public bool ChangeFame = false;
    public bool ExtendFail = false;
   
    public GameObject GoldIncrease; // ����� ����
    public GameObject FameIncrease; // ������ ����
    public GameObject NoticeWindow;

    float time = 2.0f; // �������� �ð�
    float E_time = 2.0f; // ����
    bool timecheck = false;
    float orgtime = 2.0f; // �������� ���� �ʱ�ȭ �Ǵ� ��

    //time��orgtime�� ���� ���ƾ���


    void Update()
    {
        Gold.text = GameManager.Instance.Gold.ToString();
        Fame.text = GameManager.Instance.Fame.ToString();


        if (timecheck)
        {
            time -= Time.unscaledDeltaTime;
            if(time < 0)
            {
                GoldIncrease.SetActive(false);
                FameIncrease.SetActive(false);
                time = orgtime;
            }
        }

        if (ExtendFail) // ��尡 ���ڶ� ���࿡ �������� ��
        {
            NoticeWindow.SetActive(true);

            E_time -= Time.deltaTime;
            if (E_time < 0f) // ���� ���� ���� ���� �����ְ� �����
            {
                NoticeWindow.SetActive(false);
                ExtendFail = false;
                E_time = orgtime;
            }
        }
    }
    public void UiChangeGold(int Price) // ������ ��� ǥ��
    {
        timecheck = true; // �ð����

        if (Price > 0) // ��� -> ���������� ǥ��
        {
            GoldIncrease.GetComponent<TMP_Text>().text = $"<color=red>+{Price}";
        }
        else // ���� -> �Ķ������� ǥ��
        {
            GoldIncrease.GetComponent<TMP_Text>().text = $"<color=blue>{Price}";
        }
        GoldIncrease.SetActive(true);
    }

    public void UiChangeFame(int Price) // ������ ���� ǥ��
    {
        timecheck = true; // �ð����

        if (Price > 0) // ��� -> ���������� ǥ��
        {
            FameIncrease.GetComponent<TMP_Text>().text = $"<color=red>+{Price}";
        }
        else // ���� -> �Ķ������� ǥ��
        {
            FameIncrease.GetComponent<TMP_Text>().text = $"<color=blue>{Price}";
        }
        FameIncrease.SetActive(true);
    }
}
