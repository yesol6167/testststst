using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]public struct CharacterStat
{
    [SerializeField] public float moveSpeed;

    public float MoveSpeed
    {
        get => moveSpeed = 15;
    }
}
