using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestInformation : MonoBehaviour 
{
    public GameObject ParentOBJ;
    public GameObject ChildOBJ;
    public GameObject NewsBArea;

    public Quest.QuestInfo myQuest;
    public TMP_Text Questname;
    public TMP_Text grade;
    public TMP_Text Name;
    public TMP_Text info;
    public TMP_Text reward;
    public TMP_Text Result;
    //string result;
    bool chk;
    public GameObject myNpc;
    public int People;
    public bool IsQuestchk;
    public bool NpcChk = true; 

    GameObject WindowArea;

    public void Start()
    {
        WindowArea = GameObject.Find("WindowArea");
        //UI�Ŵ���
        if(NpcChk == false)
        {
            ParentOBJ = GameObject.Find("SpawnPointQ");
            if(ParentOBJ.transform.childCount > 0)
            {
                ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;
            }
        }
        else // ���谡�� ���
        {
            if(this.gameObject.GetComponent<Host>().purpose == 0)
            {
                ParentOBJ = GameObject.Find("SpawnPointQ");
                ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;
            }
        }
        NewsBArea = GameObject.Find("NewsBArea");
    }
    public void ShowQuest(Quest.QuestInfo npc)
    {
        grade.text = npc.questgrade.ToString();
        Name.text = "[" + npc.questname + "]";
        if (IsQuestchk)
        {
            int rnd = Random.Range(0, 10);
            if (rnd > 4)
            {
                chk = true;
            }
            else
            {
                chk = false;
            }
            Result.text = chk ? "[성공]" : "[실패]"; 
        }
        else
        {
            info.text = "\"" + npc.information + "\"";
        }
        reward.text = "[골드 : " + npc.rewardgold.ToString() + "G]" + "\n" + "[평판 : " + npc.rewardfame.ToString() + "P]";
    }


    public void AddQuest() 
    {
        if (People == 0) 
        {
            QuestManager.Instance.PostedQuest(myQuest);
        }
        else 
        {
            QuestManager.Instance.ProgressQuest(myQuest, ChildOBJ);
            ChildOBJ.GetComponent<Host>().Questing = true;
        }
        ChildOBJ.GetComponent<Host>().onSmile = true;
    }

    public void onDestroy()
    {
        Destroy(gameObject);
    }

    public void onQuestRfuse()
    {
        ChildOBJ.GetComponent<Host>().onAngry = true;
    }
    public void AddReward()
    {
        ChildOBJ.GetComponent<Host>().Questing = false;
        Destroy(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        QuestManager.Instance.EndQuest(myQuest);
        if (chk)
        {
            GameManager.Instance.ChangeGold(myQuest.rewardgold);
            GameManager.Instance.ChangeFame(myQuest.rewardfame);
            ChildOBJ.GetComponent<Host>().onSmile = true;
        }
        else
        { 
            GameManager.Instance.ChangeGold(-myQuest.rewardgold);
            GameManager.Instance.ChangeFame(-myQuest.rewardfame);
            ChildOBJ.GetComponent<Host>().onAngry = true;
        }
        Destroy(gameObject); 

    }
    public void onNewsBalloon() 
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/NewsBalloon"), NewsBArea.transform) as GameObject;
    }
    public void onAdDataWindow() 
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/AdDataWindow"), WindowArea.transform) as GameObject;
    }
}
