using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class FirstCheckZone : MonoBehaviour
{
    public LayerMask HostMask = default;
    GameObject myTarget = null;    

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if ((HostMask & 1 << other.gameObject.layer) != 0) 
        {
            if (myTarget != null) // 누군가 이미 콜라이더의 타겟으로 잡혀있을 때
            {
                myTarget = other.gameObject; // 마이타겟이 나중으로 들어온 호스트의 오브젝트로 덮어 씌워지게 됨 
                StartCoroutine(CheckClock());
            }
            else // 아무도 타겟에 잡혀있지 않을 때 = 가장 처음 왔을 때
            {
                 myTarget = other.gameObject; // 타겟을 발견했을때 해당 타겟은 마이 타겟이 됨
                 StartCoroutine(CheckClock());
            }
            myTarget.GetComponent<Host>().LineChk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Host>().LineChk = false;
    }

    IEnumerator CheckClock()
    {
        while (true)
        {
            if (myTarget != null)
            {
                if (myTarget.GetComponent<Host>().objC != null) // Npc의 아이콘 오브젝트가 널이 아닐 때 = 시계가 있다면
                {
                    RemoveNotouch(); // 노터치가 비활성화되고 시계가 터치 가능해짐

                    //StopCoroutine(CheckClock());
                }
            }
            yield return null;
        }
    }

    public void RemoveNotouch()
    {
        myTarget.GetComponent<Host>().objC.GetComponent<ClockIcon>().myNotouch.SetActive(false);
    }
}
