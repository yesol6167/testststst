using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour //C#은 다중상속이 불가
{
    public CharacterStat myStat;
    Animator _anim = null;
    protected Animator myAnim //프로퍼티를 편하게 사용하는 법
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            return _anim;
        }
    }

    Rigidbody _rigid = null;
    protected Rigidbody myRigid
    {
        get
        {
            if (_rigid == null)
            {
                _rigid = GetComponent<Rigidbody>();
            }
            return _rigid;
        }
    }
}
