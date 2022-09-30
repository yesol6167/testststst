using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterMovement, IBattle
{
    Transform myTarget = null;
    Vector3 startPos = Vector3.zero;
    public enum STATE
    {
        Create, Idle, Roaming, Battle, Dead
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
                StartCoroutine(DelayRoaming(2.0f));
                break;
            case STATE.Roaming:
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-3.0f, 3.0f);
                pos.z = Random.Range(-3.0f, 3.0f);
                pos = startPos + pos;
                MoveToPosition(pos,()=>ChangeState(STATE.Idle)); //람다식 . 도착하면 idle 상태
                break;
            case STATE.Battle:
                AttackTarget(myTarget);
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
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
                break;
            case STATE.Roaming:
                break;
            case STATE.Battle:
                break;
            case STATE.Dead:
                break;
        }
    }

    IEnumerator DelayRoaming(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(STATE.Roaming);
    }


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        ChangeState(STATE.Idle);
    }


    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public void FindTarget(Transform target) //외부에서 findtarget 함수가 호출되면 battle 상태로 바뀌도록
    {
        myTarget = target;
        StopAllCoroutines();
        ChangeState(STATE.Battle);
    }
   
    public void LostTarget()
    {
        myTarget = null;
        StopAllCoroutines();
        myAnim.SetBool("IsMoving", false);
        ChangeState(STATE.Idle);
    }

    public void AttackTarget()
    {
        if (myTarget.GetComponent<IBattle>().IsLive())
        {
            myTarget.GetComponent<IBattle>()?.OnDamage(myStat.AP);
        }
        
    }

    public void OnDamage(float dmg) //인터페이스는 무조건 public
    {
        myStat.HP -= dmg;
        if (Mathf.Approximately(myStat.HP, 0.0f))
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }

    }

    public bool IsLive()
    {
        return myState != STATE.Dead;
    }
}
