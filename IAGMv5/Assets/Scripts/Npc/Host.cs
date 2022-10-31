using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.Jobs;
using UnityEngine.SearchService;
using static UnityEngine.GraphicsBuffer;
using System.Threading;
using UnityEngine.UI;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine.AI;

public class Host : MonoBehaviour
{
    public int ADTYPE = 0; // ADTYPE으로 성별과 직업 구분, 스폰매니저에서 생성되고 ADNPC.cs에서 검사함 by주현
    public static Host inst = null;

    public GameObject Quest; // 프리팹 둘은 연결 >> 퀘스트 완료 >> 완료되었으면 quest 프리팹을 파괴

    //NpcClock,QuestImoticon
    public Transform spawnPoints;
    public Transform myIconZone;
    public Transform OutPoint;
    ClockIcon myUIC = null;
    QuestIcon myUIQ = null;
    AngryIcon myUIA = null;
    MealIcon myUIM = null;
    SmileIcon myUIS = null;
    SleepIcon myUISleep = null;
    public GameObject obj = null;
    public GameObject objC = null;

    GameObject IconArea = null;
    public bool hostchk; // 마을사람과 모험가 구분용
    public bool Firstchk = false;
    public bool IsFinishQuest = false; // 퀘스트 완료 여부 >> 나가면 사라짐
    public bool exitchk = false;
    public bool OnLine = false;
    public bool Clockchk = false;
    public bool IsQuest = false;
    int count; //배열 체크

    public bool onAngry = false;
    public bool onSmile = false;

    public int People; // 사람 구분
    //Ai
    NavMeshAgent agent;
    NavMeshObstacle agent1; //이걸로 가만히 있을때는 장애물되도록 바꿀꺼임
    //spawnManager 에서 침대 테이블 배열값을 가져옴 @@@@@@@ 이거 보기 너무 안좋아서 따로 스크립트만들어서 거기서 관리하고싶음
    SpawnManager bedchairvalue;
    //여관 레스토랑 벡터값
    [SerializeField] Vector3 res;
    [SerializeField] Vector3 inn;
    [SerializeField] Vector3[] gotable;

    [SerializeField] Transform Exit;
    public int purpose;

    [SerializeField] Vector3[] gobed;
    //FirstChecker

    public LayerMask layerMask;
   
    public enum STATE
    {
        Create, Idle, GoSide, Moving, Quest, Eat, Eating, Sleep, Sleeping, Exit
    }
    public STATE myState = default;
    
    public CharacterStat stat;
    
    //public CharState.NPC mystat; // 캐릭터 능력치

    public Transform Line; //초기 맨 뒤쪽 값 전달 >> 앞에 좌표 확인 >> X >> 다음 위치값 전달 >> 반복

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle: // 로비데스크로 와서 아이들상태 돌입
                agent.enabled = false;//네비메시 비활성화
                agent1.enabled = true;//네비 옵스타클 활성화 //이걸로 가만히 있을때는 장애물되도록 바꿀꺼임
                StopAllCoroutines(); // 모든 코루틴 멈추고 대기
                switch (purpose)
                {
                    case 0:
                        StartCoroutine(deskLine());
                        break;
                    case 1:
                        //GetComponent<Animator>().SetBool("IsMoving", true);
                        //agent.SetDestination(res);
                        StartCoroutine(HostLine());
                        break;
                    case 2:
                        StartCoroutine(HostLine());
                        break;
                }
                //NpcClock의 생성

                if (!Clockchk)
                {
                    IconArea = GameObject.Find("IconArea");
                    objC = Instantiate(Resources.Load("IconPrefabs/ClockIcon"), IconArea.transform) as GameObject;
                    myUIC = objC.GetComponent<ClockIcon>();
                    myUIC.myTarget = myIconZone;

                    Clockchk = true;
                }

                break;

            case STATE.Moving: // - 윤섭
                StopAllCoroutines();
                agent1.enabled = false; //네비 옵스타클 비활성화
                agent.enabled = true; //네비 네비메시 활성화
                agent.ResetPath();
                switch (purpose)
                {
                    case 0:
                        ChangeState(STATE.Quest);
                        break;
                    case 1:
                        GetComponent<Animator>().SetBool("IsMoving", true);
                        agent.SetDestination(res);
                        StartCoroutine(HostLine());
                        StartCoroutine(WalkTo(STATE.Eat));
                        break;
                    case 2:
                        GetComponent<Animator>().SetBool("IsMoving", true);
                        agent.SetDestination(inn);
                        StartCoroutine(HostLine());
                        StartCoroutine(WalkTo(STATE.Sleep)); //도착하면
                        break;
                }
                break;
            case STATE.Quest: //데스크까지 이동하는 상태
                StartCoroutine(deskLine());
                StartCoroutine(deskmoving());
                break;
            case STATE.Eat:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                StopAllCoroutines();
                GetComponent<Animator>().SetBool("IsMoving", false);

                IconArea = GameObject.Find("IconArea");

                obj = Instantiate(Resources.Load("IconPrefabs/MealIcon"), IconArea.transform) as GameObject;
                myUIM = obj.GetComponent<MealIcon>();
                myUIM.myTarget = myIconZone;
                obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoTable);
                break;
            case STATE.Eating:
                StopAllCoroutines();
                GetComponent<Animator>().SetTrigger("IsSeating");
                Invoke("GoExit", 5);
                //Invoke("SeatToWalk",11);
                break;

            case STATE.Sleep:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                StopAllCoroutines();
                GetComponent<Animator>().SetBool("IsMoving", false);

                IconArea = GameObject.Find("IconArea");

                obj = Instantiate(Resources.Load("IconPrefabs/BedIcon"), IconArea.transform) as GameObject;
                myUISleep = obj.GetComponent<SleepIcon>();
                myUISleep.myTarget = myIconZone;
                obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoBed);

                break;

            case STATE.Sleeping:
                StopAllCoroutines();
                GetComponent<Animator>().SetTrigger("IsLaying");
                Invoke("GoExit", 5);
                break;

            case STATE.Exit:

                StopAllCoroutines();
                GetComponent<Animator>().SetBool("IsMoving", true);
                switch (purpose)
                {
                    case 0:
                        OnLine = false;
                        agent1.enabled = false;
                        agent.enabled = true;
                        break;
                    case 1:
                        bedchairvalue._chairSlot[count] = SpawnManager.ChairSlot.None;
                        GetComponent<Animator>().SetBool("SitToStand", true);
                        agent1.enabled = false;
                        agent.enabled = true;
                        agent.ResetPath();
                        break;
                    case 2:
                        bedchairvalue._bedSlot[count] = SpawnManager.BedSlot.None;
                        GetComponent<Animator>().SetBool("LayToStand", true);
                        agent1.enabled = false;
                        agent.enabled = true;
                        agent.ResetPath();
                        break;
                }
                agent.SetDestination(Exit.position); // outpoint로 가는 코루틴
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                if (myUIC.myTarget != null && myUIC == null && myUIC.TimeOut == false) // 시계를 제한 시간안에 누르면 퀘스트 아이콘 생성
                {
                    StartCoroutine(onQuestIcon());
                    myUIC.myTarget = null;
                }
                else if (myUIC.myTarget != null && myUIC == null && myUIC.TimeOut == true) // 시계를 제한 시간안에 안누르면 불만 아이콘 생성
                {
                    StartCoroutine(onAngryIcon());
                    myUIC.myTarget = null;
                }
                else if (onAngry == true) // 퀘스트를 거절하면 불만 아이콘 생성
                {
                    StartCoroutine(onAngryIcon());
                    onAngry = false;
                }
                else if (onSmile == true)
                {
                    StartCoroutine(onSmileIcon());
                    onSmile = false;
                }
                break;

            case STATE.Quest:
                if (IsQuest) transform.SetAsFirstSibling();
                break;
            case STATE.Eat:
                break;
            case STATE.Sleep:
                break;
            case STATE.Exit:
                transform.SetAsLastSibling();
                break;
        }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent1 = GetComponent<NavMeshObstacle>();

        bedchairvalue = FindObjectOfType<SpawnManager>();//GetComponent<SpawnManager>();
        inst = this;
    }
    void Start()
    {
        // 해당 위치에 있던 Npc의 방문 목적을 정해주는 명령어는 SpawnManger.cs로 옮기 -by주현

        ChangeState(STATE.Moving);
    }
    public void GoBed() //주문하고 침대로 이동
    {
        for (count = 0; count < 6; count++)
        {
            if (bedchairvalue._bedSlot[count] == SpawnManager.BedSlot.None)
            {
                GetComponent<Animator>().SetBool("IsMoving", true);
                agent.SetDestination(gobed[count]);
                StartCoroutine(BedToSleeping());
                bedchairvalue.bedSlot[count] = SpawnManager.BedSlot.Check;
                break;
            }
        }
    }
    public void GoTable() //주문하고 테이블로 이동
    {
        for (count = 0; count < 12; count++)
        {
            if (bedchairvalue._chairSlot[count] == SpawnManager.ChairSlot.None)
            {
                GetComponent<Animator>().SetBool("IsMoving", true);
                agent.SetDestination(gotable[count]);
                StartCoroutine(EatToEating());
                bedchairvalue._chairSlot[count] = SpawnManager.ChairSlot.Check;
                break;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    IEnumerator deskLine()//레이어로 줄세우기
    {
        while (true)
        {
            if (OnLine)
            {
                Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.black);
                if (Physics.SphereCast(transform.position, 5.0f, transform.forward , out RaycastHit hitinfo, 5.0f, layerMask))
                {
                    GetComponent<Animator>().SetBool("IsMoving", false);
                    ChangeState(STATE.Idle);
                }
                else
                {
                    ChangeState(STATE.Quest);
                }
            }
            yield return null;
        }
    }
    IEnumerator EatToEating() //도착지점에 도착하면 Eat상태에서 Eating상태로
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                Debug.Log("1");
                Debug.Log(agent != null);
                if (agent.remainingDistance <= 1.0f)
                {
                    agent.velocity = Vector3.zero;
                    ChangeState(STATE.Eating);
                    agent.ResetPath();
                    if (count % 2 == 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 270, 0);
                    }
                    else if (count % 2 == 1)
                    {
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                }
            }
            yield return null;
        }
    }
    IEnumerator deskmoving()
    {
        OnLine = true;
        Vector3 dir = Line.position - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        GetComponent<Animator>().SetBool("IsMoving", true);
        while (dist > 0.0f)
        {
            float delta = stat.moveSpeed * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
    }
    IEnumerator BedToSleeping() //도착지점에 도착하면 Eat상태에서 Eating상태로
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= 1.0f)
                {
                    agent.velocity = Vector3.zero;
                    ChangeState(STATE.Sleeping);
                    agent.ResetPath();
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                }
            }
            yield return null;
        }
    }
    IEnumerator WalkTo(STATE HostState) // 도착하면 Walk상태에서 Eat이나 여관 상태로
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance == 0)//남은거리가0이면
                {
                    Debug.Log("2");
                    Debug.Log(agent != null);
                    agent.velocity = Vector3.zero;
                    ChangeState(HostState);
                    agent.ResetPath();
                }
            }
            yield return null;
        }
    }

    IEnumerator HostLine() //줄서기
    {

        while (true)
        {
            if (Physics.SphereCast(transform.position, 5.0f, transform.forward, out RaycastHit hitinfo1, 10.0f, layerMask))
            {
                GetComponent<Animator>().SetBool("IsMoving", false);
                agent.velocity = Vector3.zero;
                ChangeState(STATE.Idle);
                //agent.ResetPath();
            }
            else
            {
                ChangeState(STATE.Moving);
            }
            yield return null;
        }
    }

    IEnumerator onQuestIcon()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject objQ = Instantiate(Resources.Load("IconPrefabs/QuestIcon"), IconArea.transform) as GameObject;
        myUIQ = objQ.GetComponent<QuestIcon>();
        myUIQ.myTarget = myIconZone;
    }
    IEnumerator onAngryIcon()
    {
        yield return new WaitForSeconds(1.0f);

        GameObject objA = Instantiate(Resources.Load("IconPrefabs/AngryIcon"), IconArea.transform) as GameObject;
        myUIA = objA.GetComponent<AngryIcon>();
        myUIA.myTarget = myIconZone;
        IsFinishQuest = true; // 불만시 나가게끔
        StopAllCoroutines();
        IsQuest = false;
        ChangeState(STATE.Exit);
    }

    IEnumerator onSmileIcon() // 스마일 아이콘
    {
        yield return new WaitForSeconds(1.0f);

        GameObject objS = Instantiate(Resources.Load("IconPrefabs/SmileIcon"), IconArea.transform) as GameObject; 
        myUIS = objS.GetComponent<SmileIcon>();
        myUIS.myTarget = myIconZone;
        if (hostchk)
        {
            IsFinishQuest = true;
            
        }
        StopCoroutine(deskLine());
        IsQuest = false;
        ChangeState(STATE.Exit);
    }
    IEnumerator FinishQuest(float t) //퀘스트 존에 가서 퀘스트를 하도록
    {
        yield return new WaitForSeconds(t);
        SpawnManager.Instance.Teleport(gameObject);
        IsFinishQuest = true;
        OnLine = false;
        Clockchk = false;
        ChangeState(STATE.Quest);
    }
    public void RemoveNotouch()
    {
        myUIC.myNotouch.SetActive(false);
    }

    /* 해당 함수를 TeleportPoint.cs로 옮김
       옮긴 이유 : Npc가 생성되는 곳에 대기 줄이 너무 길어져 다른 Npc가 서있으면 오류가 발생함 
    -> Npc 생성 위치에 콜라이더를 만들어 해당위치에 이미 생성된 Npc가 서 있을 경우를 감지하여 새로운 Npc의 생성을 막을 예정 
    -> 하지만 Host.cs의 해당 온트리거 함수가 실행되어 오류가 발생하기에 새 콜라이더를 배치가 불가능

    private void OnTriggerEnter(Collider obj)
    {
        if (!OnLine)
        {
            if (IsFinishQuest) // 완료상태에서 트리거에 닿으면 파괴 카운트 삭제
            {
                Destroy(gameObject);
                SpawnManager.Instance.hostCount--;
            }
            else
            {
                StartCoroutine(FinishQuest(3.0f)); //퀘스트 완료 시간
            }
        }
        else { IsQuest = true; }
    }
    */

    public void GoExit() //먹는상태에서 나가는 상태로
    {
        ChangeState(STATE.Exit);
        IsFinishQuest = true;
    }

    public void StartFinishQuest()
    {
        StartCoroutine(FinishQuest(3.0f));
    }
}