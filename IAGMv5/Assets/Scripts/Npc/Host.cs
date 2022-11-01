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
    public bool LineChk = false; // 퇴장하는 Npc와 입장하는 Npc의 충돌시 시계아이콘 생성되는 오류 수정
    public static Host inst = null;

    public GameObject Quest; // 프리팹 둘은 연결 >> 퀘스트 완료 >> 완료되었으면 quest 프리팹을 파괴

    //NpcClock,QuestImoticon
    public Transform spawnPoints;
    public Transform myIconZone;
    public Transform OutPoint;
    //public GameObject obj = null;
    public GameObject objC = null;
    public GameObject myStaff;

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
                StaffCheck();
                agent.enabled = false;//네비메시 비활성화
                agent1.enabled = true;//네비 옵스타클 활성화 //이걸로 가만히 있을때는 장애물되도록 바꿀꺼임
                StopAllCoroutines(); // 모든 코루틴 멈추고 대기
                StartCoroutine(deskLine());
                //NpcClock의 생성

                if (!Clockchk)
                {
                    if (LineChk == true)
                    {
                        IconArea = GameObject.Find("IconArea");
                        objC = Instantiate(Resources.Load("IconPrefabs/ClockIcon"), IconArea.transform) as GameObject;
                        objC.GetComponent<ClockIcon>().myTarget = myIconZone;
                        objC.GetComponent<ClockIcon>().myHost = this.gameObject;
                        Clockchk = true;
                    }
                }

                break;

            case STATE.Moving: // 윤섭
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
                        StartCoroutine(deskLine());
                        StartCoroutine(WalkTo(STATE.Eat));
                        break;
                    case 2:
                        GetComponent<Animator>().SetBool("IsMoving", true);
                        agent.SetDestination(inn);
                        StartCoroutine(deskLine());
                        StartCoroutine(WalkTo(STATE.Sleep)); //도착하면
                        break;
                }
                break;
            case STATE.Quest: //데스크까지 이동하는 상태
                StartCoroutine(deskLine());
                StartCoroutine(deskmoving());
                break;
            case STATE.Eat:
                StaffCheck();
                transform.rotation = Quaternion.Euler(0, 180, 0);
                StopAllCoroutines();
                GetComponent<Animator>().SetBool("IsMoving", false);
                IconArea = GameObject.Find("IconArea");

                if (!Clockchk)
                {
                        IconArea = GameObject.Find("IconArea");
                        objC = Instantiate(Resources.Load("IconPrefabs/ClockIcon"), IconArea.transform) as GameObject;
                        objC.GetComponent<ClockIcon>().myTarget = myIconZone;
                        objC.GetComponent<ClockIcon>().myHost = this.gameObject;
                        Clockchk = true;
                }

                //OnIcon("MealIcon");
                break;
            case STATE.Eating:
                StopAllCoroutines();
                GetComponent<Animator>().SetTrigger("IsSeating");
                Invoke("GoExit", 5);
                //Invoke("SeatToWalk",11);
                break;
            case STATE.Sleep:
                StaffCheck();
                transform.rotation = Quaternion.Euler(0, 180, 0);
                StopAllCoroutines();
                GetComponent<Animator>().SetBool("IsMoving", false);
                IconArea = GameObject.Find("IconArea");

                if (!Clockchk)
                {
                        IconArea = GameObject.Find("IconArea");
                        objC = Instantiate(Resources.Load("IconPrefabs/ClockIcon"), IconArea.transform) as GameObject;
                        objC.GetComponent<ClockIcon>().myTarget = myIconZone;
                        objC.GetComponent<ClockIcon>().myHost = this.gameObject;
                        Clockchk = true;
                }
                //OnIcon("BedIcon");
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
                if (onAngry == true) // 퀘스트를 거절하면 불만 아이콘 생성
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

    IEnumerator deskLine()// 줄세우기
    {
        while (true)
        {
            if (OnLine)
            {
                Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.black);
                if (Physics.SphereCast(transform.position, 7.0f, transform.forward , out RaycastHit hitinfo, 7.0f, layerMask))
                {
                    if (hitinfo.collider.gameObject.layer == 6) // layer 6 = Host, layer 3 = Staff
                    {
                        if (hitinfo.collider.GetComponent<Host>().LineChk == true) //  줄서있는 사람과 부딪쳤을 경우 
                        {
                            LineChk = true;
                        }
                    }
                    GetComponent<Animator>().SetBool("IsMoving", false);
                    if(purpose == 1 || purpose == 2)
                    {
                        agent.velocity = Vector3.zero;
                    }
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
                    agent.velocity = Vector3.zero;
                    ChangeState(HostState);
                    agent.ResetPath();
                }
            }
            yield return null;
        }
    }

    IEnumerator onQuestIcon()
    {
        yield return new WaitForSeconds(1.5f);

        OnIcon("QuestIcon");
    }

    IEnumerator onAngryIcon()
    {
        yield return new WaitForSeconds(1.0f);

        OnIcon("AngryIcon");

        IsFinishQuest = true; // 불만시 나가게끔
        StopAllCoroutines();
        IsQuest = false;
        ChangeState(STATE.Exit);
    }

    IEnumerator onSmileIcon() // 스마일 아이콘
    {
        yield return new WaitForSeconds(1.0f);

        OnIcon("SmileIcon");

        if (hostchk)
        {
            IsFinishQuest = true;
        }
        StopCoroutine(deskLine());
        IsQuest = false;
        ChangeState(STATE.Exit);
    }

    IEnumerator onMandBIcon(string IconName) // 식사 & 침대 아이콘
    {
        yield return new WaitForSeconds(1.0f);

        OnIcon(IconName);
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
    public void GoExit() //먹는상태에서 나가는 상태로
    {
        ChangeState(STATE.Exit);
        IsFinishQuest = true;
    }

    public void StartFinishQuest()
    {
        StartCoroutine(FinishQuest(3.0f));
    }

    public void StartCoQi() // = StartCoroutineQuestIcon
    {
        StartCoroutine(onQuestIcon());
    }

    public void StartCoAi() // = StartCoroutineAngryIcon
    {
        StartCoroutine(onAngryIcon());
    }

    public void StartCoMandB(string IconName)
    {
        StartCoroutine(onMandBIcon(IconName));
    }

    public void OnIcon(string IconName)
    {
        GameObject Obj = Instantiate(Resources.Load($"IconPrefabs/{IconName}"), IconArea.transform) as GameObject;

        switch (IconName)
        {
            case "SmileIcon":
                Obj.GetComponent<SmileIcon>().myTarget = myIconZone;
                break;
            case "AngryIcon":
                Obj.GetComponent<AngryIcon>().myTarget = myIconZone;
                break;
            case "QuestIcon":
                Obj.GetComponent<QuestIcon>().myTarget = myIconZone;
                break;
            case "BedIcon":
                Obj.GetComponent<SleepIcon>().myTarget = myIconZone;
                Obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoBed);
                break;
            case "MealIcon":
                Obj.GetComponent<MealIcon>().myTarget = myIconZone;
                Obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoTable);
                break;
        }
    }
    public void StaffCheck()
    {
        if (Physics.SphereCast(transform.position, 7.0f, transform.forward, out RaycastHit hitinfo, 7.0f, layerMask))
        {
            myStaff = hitinfo.collider.gameObject;
        }
    }
}