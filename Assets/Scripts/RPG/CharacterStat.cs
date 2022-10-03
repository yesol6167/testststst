using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] //직렬화
public struct CharacterStat
{
    //SerializeField를 사용하면 private으로 되어있는 것도 인스펙터에서 보여질 수 있다.
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] float ap;
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;

    public float HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value,0.0f,maxHp); // 최솟값 최댓값
        } 
    }
    public float MoveSpeed
    {
        get => moveSpeed;
    }
    public float RotSpeed
    {
        get => rotSpeed;
    }
    public float AP
    {
        get => ap;
    }
    public float AttackRange
    {
        get => attackRange;
    }
    public float AttackDelay
    {
        get => attackDelay;
    }
}
