using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent Attack = default;
    public Transform leftFoot;
    public Transform rightFoot;
    public GameObject orgDustEff;

    public void LeftFootEvent()
    {
      Instantiate(orgDustEff, leftFoot.position, Quaternion.identity);

    }

    public void RightFootEvent()
    {
        Instantiate(orgDustEff, rightFoot.position, Quaternion.identity);

    }

    public void OnAttack()
    {
        Attack?.Invoke();
    }
}
