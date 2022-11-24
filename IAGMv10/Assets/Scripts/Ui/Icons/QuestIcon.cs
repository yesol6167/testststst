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
    bool VLchk; // 마을사람 or 모험가 체크용

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetOut());
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
            VLchk = hostobj.GetComponent<Host>().VLchk;// 호스트 말고 스폰매니저에서 바로 받아오기
            npcQuest = VLchk ? hostobj.GetComponent<VLNpc>().myQuest : hostobj.GetComponent<QuestInformation>().myQuest; // 삼항식으로 마을사람 참, 모험가 거짓임
        }
    }

    IEnumerator GetOut()
    {
        yield return new WaitForSeconds(15.0f);
        hostobj.GetComponent<Host>().onAngry = true;
        Destroy(gameObject);
    }

    public void ShowRequestWindow() //퀘스트 버튼 누르면 퀘스트 요청서 생성
    {
        //GameManager.Instance.GetComponent<AudioSource>().Play();
        GameObject RQwindow;
        if (VLchk)
        {
            RQwindow = Instantiate(Resources.Load("Prefabs/RequestWindow"), GameObject.Find("WindowArea").transform) as GameObject;
        }
        else // 모험가라면
        {
            if (hostobj.GetComponent<Host>().Questing == true)
            {
                RQwindow = Instantiate(Resources.Load("Prefabs/QuestReportWindow"), GameObject.Find("WindowArea").transform) as GameObject;
            }
            else
            {
                RQwindow = Instantiate(Resources.Load("Prefabs/QuestWindow"), GameObject.Find("WindowArea").transform) as GameObject;
            }
        }

        RQwindow.GetComponent<QuestInformation>().People = hostobj.GetComponent<Host>().People;
        RQwindow.GetComponent<QuestInformation>().IsQuestchk = hostobj.GetComponent<Host>().Questing;
        RQwindow.GetComponent<QuestInformation>().myQuest = npcQuest;
        //NPCchk= tru or false / 트루일경우 창생성영역의 자식으로 요청서 생성 or 펄스일 경우  IsFinishQuest의 bool값 체크 후 조건에 맞는 실행문을 실행

        if (VLchk)
        {
            //RQwindow 오브젝트의 QuestInformation 스크립트의 myQuest 변수에 npcQuest를 넣는다.
            RQwindow.GetComponent<QuestInformation>().myNpc = hostobj;
        }
        RQwindow.GetComponent<QuestInformation>().ShowQuest(npcQuest, hostobj);
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
