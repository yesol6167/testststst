using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestInformation : MonoBehaviour
{
    public GameObject ParentOBJ;
    public GameObject ChildOBJ;
    public GameObject NewsBArea; // NewsBalloon이 자식으로 생성되는 UiCanvas안의 부모 오브젝트

    public Quest.QuestInfo myQuest;
    public TMP_Text Questname;
    public TMP_Text grade;
    public TMP_Text Name;
    public TMP_Text info;
    public TMP_Text reward;
    public TMP_Text Result;
    string result;
    bool chk; // 성공 실패 여부 체크
    public GameObject myNpc; // 퀘스트를 준 npc
    public int People;
    public bool IsQuestchk;

    GameObject QuestWindowArea; // AdDataWindow가 자식으로 생성되는 UiCanvas안의 부모 오브젝트

    public void Start()
    {
        QuestWindowArea = GameObject.Find("QuestWindowArea");
        //UI매니저
        ParentOBJ = GameObject.Find("SpawnPointQ");
        /*
        switch (this.gameObject.GetComponent<Host>().purpose)
        {
            case 0:
                ParentOBJ = GameObject.Find("SpawnPointQ");
                break;
            case 1:
                ParentOBJ = GameObject.Find("SpawnPointP");
                break;
            case 2:
                ParentOBJ = GameObject.Find("SpawnPointM");
                break;
        }
        */
        ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;

        NewsBArea = GameObject.Find("NewsBArea");
    }
    public void ShowQuest(Quest.QuestInfo npc)
    {
        grade.text = npc.questgrade.ToString();
        Name.text = "[" + npc.questname + "]";
        if (IsQuestchk)
        {
            //모험가 보고서 용
            int rnd = Random.Range(0, 10);
            if (rnd > 4)
            {
                chk = true;
            }
            else
            {
                chk = false;
            }
            //보고서 용
            Result.text = chk ? "[성공]" : "[실패]"; // 성공 실패 여부 체크후 변경
        }
        else
        {
            //퀘스트 신청용
            info.text = "\"" + npc.information + "\"";
        }
        reward.text = "[골드 : " + npc.rewardgold.ToString() + "G]" + "\n" + "[평판 : " + npc.rewardfame.ToString() + "P]";
    }


    public void AddQuest() // 승낙
    {
        if (People == 0)
        {
            //마을사람
            QuestManager.Instance.PostedQuest(myQuest);
        }
        else
        {
            //'로비를 이용하는' 모험가 // 현재 펍이나 모텔을 이용하는 모험가도 퀘스트를 가져감 -> 수정 필요
            QuestManager.Instance.ProgressQuest(myQuest, ChildOBJ);
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
        Destroy(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        QuestManager.Instance.EndQuest(myQuest);
        if (chk)
        { // 성공시 증가 연산
            GameManager.Instance.Gold += myQuest.rewardgold;
            GameManager.Instance.Fame += myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onSmile = true;
        }
        else
        { // 실패시 감소 연산
            GameManager.Instance.Gold -= myQuest.rewardgold;
            GameManager.Instance.Fame -= myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onAngry = true;
        }
        //현재 퀘스트 리스트를 완료 퀘스트 리스트에 추가
        Destroy(gameObject); // 확인시 연산 후 페이지 삭제

    }
    public void onNewsBalloon() // 뉴스말풍선 생성 "새로운 퀘스트가 추가되었습니다."
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/NewsBalloon"), NewsBArea.transform) as GameObject;
    }
    public void onAdDataWindow() // 모험가 정보창 띄우기
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/AdDataWindow"), QuestWindowArea.transform) as GameObject;
    }
}
