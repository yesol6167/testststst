using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum GRADE
{
    BRONZE, SILVER, GOLD, PLATINUM, DIA
}

[Serializable]public struct CharacterStat
{
    [SerializeField] public GRADE grade;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotSpeed;
    [SerializeField] public float money;




    public float MoveSpeed
    {
        get => moveSpeed;
    }
}
