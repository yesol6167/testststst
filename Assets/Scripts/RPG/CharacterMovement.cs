using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//public delegate void MyAction();

public class CharacterMovement : CharacterProperty, IBattle
{

    Coroutine moveCo = null;
    Coroutine rotCo = null;
    Coroutine attackCo = null;

   protected void AttackTarget(Transform target)
    {
        StopAllCoroutines();
        attackCo = StartCoroutine(AttackingTarget(target,myStat.AttackRange,myStat.AttackDelay));
    }
    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot=true)
    {
        if (attackCo != null)
        {
            StopCoroutine(attackCo);
            attackCo = null;
        }
        if (moveCo != null)
        {
            StopCoroutine(moveCo);
            moveCo = null;
        }
        moveCo = StartCoroutine(MovingToPosition(pos,done));
        if (Rot)
        {
            if (rotCo != null)
            {
                StopCoroutine(rotCo);
                rotCo = null;
            }
            rotCo = StartCoroutine(RotatingToPosition(pos));
        }
    }
    IEnumerator RotatingToPosition(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized; //길이가 1인 벡터로 저장
        float Angle = Vector3.Angle(transform.forward, dir); // 각도 구하기
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f) //right 벡터와 dir 내접
        {
            rotDir = -rotDir;
        }

        while (Angle > 0.0f)
        {
            float delta = myStat.RotSpeed * Time.deltaTime;
            if (delta > Angle)
            {
                delta = Angle;
            }
            Angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta, Space.World);
            yield return null;
        }
    }

    IEnumerator MovingToPosition(Vector3 pos, UnityAction done) //포지션을 전달하면 해당 포지션으로 이동
    {
        Vector3 dir = pos - transform.position; //방향
        float dist = dir.magnitude; //거리
        dir.Normalize(); //정규화

        //달리기 시작
        myAnim.SetBool("IsMoving", true);
        //자기가 속해있는 오브젝트에서 원하는 컴포넌트 가져오기 getcomponent
        while (dist > 0.0f) //이동할 거리가 있으면 계속 반복
        {
            if (!myAnim.GetBool("IsAttacking"))
            {
                float delta = myStat.MoveSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
        //달리기 끝 - 도착
        myAnim.SetBool("IsMoving", false);
        done?.Invoke(); //done이 null이 아닐때에만 저장되어 있는 함수를 실행하는 Invoke
    }

    IEnumerator AttackingTarget(Transform target, float AttackRange, float AttackDelay) //값이 아니라 참조형으로 전달
    {
        float playTime = 0.0f;
        float delta = 0.0f;
        while (target != null)
        {
           
            if(!myAnim.GetBool("IsAttacking")) playTime += Time.deltaTime;

            //이동
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();

            if (dist > AttackRange)
            {
                myAnim.SetBool("IsMoving", true);
                delta = myStat.MoveSpeed * Time.deltaTime;
                if(delta > dist)
                 {
                     delta = dist;
                 }
                transform.Translate(dir * delta, Space.World);
            }
            //매번 새로 구하는 것이라서 빼줄 필요는 없음
            else
            {
                myAnim.SetBool("IsMoving", false);
                if(playTime >= AttackDelay)
                {
                    //공격
                    playTime = 0.0f;
                    myAnim.SetTrigger("Attack");
                }
            }

            //회전
            delta = myStat.RotSpeed * Time.deltaTime;
            float Angle = Vector3.Angle(dir, transform.forward);
            float rotDir = 1.0f;
            if(Vector3.Dot(transform.right, dir) < 0.0f)
            {
                rotDir = -rotDir;
            }
            if(delta > Angle)
            {
                delta = Angle;
            }
            transform.Rotate(Vector3.up * rotDir * delta, Space.World);

            yield return null;
        }
        myAnim.SetBool("IsMoving", false);
    }

    public void OnDamage(float dmg)
    {

    }
}
