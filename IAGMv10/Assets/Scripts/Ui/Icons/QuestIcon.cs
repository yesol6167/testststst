using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestIcon : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Transform myIconZone;
    public GameObject myButton;
    public Transform QuestWindow;
    Vector2 dragOffset = Vector2.zero;
    Quest.QuestInfo npcQuest;
    public GameObject hostobj;
    bool NPCchk; // 마을사람 or 모험가 체크용


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (myIconZone != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myIconZone.position);
            //-나중에 이부분 가독성 좋게 수정할 방법 찾기-
            //myTarget.parent.parent.parent.parent.parent.parent.parent.parent.gameObject = host;
            hostobj = myIconZone.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            NPCchk = hostobj.GetComponent<Host>().hostchk;// 호스트 말고 스폰매니저에서 바로 받아오기
            npcQuest = NPCchk ? hostobj.GetComponent<VLNpc>().myQuest : hostobj.GetComponent<QuestInformation>().myQuest; // 삼항식으로 마을사람 참, 모험가 거짓임
        }
    }
    public void ShowRequestWindow() //퀘스트 버튼 누르면 퀘스트 요청서 생성
    { // 퀘스트 요청서가 가운데에 보이게 위치 설정
        //퀘스트 완료시 if문으로 정리 >> 퀘스트를 완료했다는 것을 체크
        /*GameObject RQwindow = NPCchk ? Instantiate(Resources.Load("Prefabs/RequestWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject : // 마을사람
            //모험가
            hostobj.GetComponent<Host>().IsFinishQuest ? Instantiate(Resources.Load("Prefabs/QuestReportWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject :
            Instantiate(Resources.Load("Prefabs/QuestWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject; // 참이면 마을사람 거짓이면 모험가 >> 모험가에서 참이면 보고서, 거짓이면 신청서*/
        GameObject RQwindow;
        if (NPCchk)
        {
            RQwindow = Instantiate(Resources.Load("Prefabs/RequestWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject;
        }
        else
        {
            if(hostobj.GetComponent<Host>().IsFinishQuest == true)
            {
                RQwindow = Instantiate(Resources.Load("Prefabs/QuestReportWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject;
            }
            else
            {
                RQwindow = Instantiate(Resources.Load("Prefabs/QuestWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject;
            }
        }

        RQwindow.GetComponent<QuestInformation>().People = hostobj.GetComponent<Host>().People;
        RQwindow.GetComponent<QuestInformation>().IsQuestchk = hostobj.GetComponent<Host>().IsFinishQuest;
        RQwindow.GetComponent<QuestInformation>().myQuest = npcQuest;
        //NPCchk= tru or false / 트루일경우 창생성영역의 자식으로 요청서 생성 or 펄스일 경우  IsFinishQuest의 bool값 체크 후 조건에 맞는 실행문을 실행

        if (NPCchk)
        {
            //RQwindow 오브젝트의 QuestInformation 스크립트의 myQuest 변수에 npcQuest를 넣는다.
            RQwindow.GetComponent<QuestInformation>().myNpc = hostobj;
        }
        RQwindow.GetComponent<QuestInformation>().ShowQuest(npcQuest);
        //UiCanvas 밑에 생성
        RQwindow.transform.SetAsFirstSibling();
        RQwindow.SetActive(true);
        myButton.SetActive(false);
    }

    public void onDestroyIcon()
    {
        Destroy(gameObject);
    }

    //퀘스트창 드래그 앤 드롭
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)QuestWindow.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        QuestWindow.position = eventData.position + dragOffset;
    }
}
