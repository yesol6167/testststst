using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum GRADE
{
    BRONZE, SILVER, GOLD, PLATINUM, DIA
}

public enum State
{
    HUNGRY, TIRED, HEALTH
}

[Serializable]public struct CharacterStat
{
    [SerializeField] public GRADE grade;
    [SerializeField] public State state;
    [SerializeField] public float health;
    [SerializeField] public float attack;
    [SerializeField] public float defence;


    public float Health
    {
        get => health;
        set => health = value;
    }
    public float Attack
    {
        get => attack;
        set => attack = value;
    }
    public float Defence
    {
        get => defence;
        set => defence = value;
    }

    public GRADE Grade
    {
        get => grade;
        set
        {
            float average = (attack + defence) / 2;
            if(average < 10)
            {
                grade = GRADE.BRONZE;
            }
            else if(average < 30)
            {
                grade = GRADE.SILVER;
            }
            else if (average < 50)
            {
                grade = GRADE.GOLD;
            }
            else if (average < 70)
            {
                grade = GRADE.PLATINUM;
            }
            else
            {
                grade = GRADE.DIA;
            }
        }
    }
    public State CharState
    {
        get => state;
        set => state = value;
    }






}
