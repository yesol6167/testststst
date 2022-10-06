using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform QuestPos;
    public float MoveSpeed = 3.0f;
    Animator myAnim;
    public CharacterStat myStat;
    public enum STATE
    {
        Create, Idle, Quest, Sleep, Eat, Exit
    }
    public STATE myState = STATE.Create;
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Quest:
                break;
            case STATE.Sleep:
                break;
            case STATE.Eat:
                break;
            case STATE.Exit:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                StartCoroutine(CharCreate(1, 2));
                break;
            case STATE.Idle:
                StopCoroutine(CharCreate(1, 2));
                break;
            case STATE.Quest:
                Vector3 dir = QuestPos.position - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();

                //달리기 시작
                myAnim.SetBool("OnWalk", true);
                if (dist > 0.0f)
                {
                    float delta = MoveSpeed * Time.deltaTime;
                    if (delta > dist)
                    {
                        delta = dist;
                    }
                    dist -= delta;
                    transform.Translate(dir * delta, Space.World);
                }
                if (transform.position == QuestPos.position)
                {
                    myAnim.SetBool("OnWalk", false);
                    ChangeState(STATE.Idle);
                }
                break;
            case STATE.Sleep:
                break;
            case STATE.Eat:
                break;
            case STATE.Exit:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        ChangeState(STATE.Create);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    IEnumerator CharCreate(float MinV, float MaxV)
    {
        int i = 0;
        while (i == 0) {
            myStat.Health = Random.Range(MinV * 100, MaxV * 100);
            myStat.Attack = Random.Range(MinV * 10, MaxV * 10);
            myStat.Defence = Random.Range(MinV * 10, MaxV * 10);
            myStat.CharState = (State)Random.Range(0, 3);
            i++;
            yield return null;
        }
        i = 0;
        ChangeState(STATE.Idle);
    }

    IEnumerator ExitChar()
    {
        while (true)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
