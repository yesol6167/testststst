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
            if (myTarget != null) // ������ �̹� �ݶ��̴��� Ÿ������ �������� ��
            {
                myTarget = other.gameObject; // ����Ÿ���� �������� ���� ȣ��Ʈ�� ������Ʈ�� ���� �������� �� 
                StartCoroutine(CheckClock());
            }
            else // �ƹ��� Ÿ�ٿ� �������� ���� �� = ���� ó�� ���� ��
            {
                 myTarget = other.gameObject; // Ÿ���� �߰������� �ش� Ÿ���� ���� Ÿ���� ��
                 StartCoroutine(CheckClock());
            }
        }
    }
    IEnumerator CheckClock()
    {
        while (true)
        {
            if (myTarget != null)
            {
                if (myTarget.GetComponent<Host>().objC != null) // Npc�� ������ ������Ʈ�� ���� �ƴ� �� = �ð谡 �ִٸ�
                {
                    myTarget.GetComponent<Host>().RemoveNotouch(); // ����ġ�� ��Ȱ��ȭ�ǰ� �ð谡 ��ġ ��������

                    //StopCoroutine(CheckClock());
                }
            }
            yield return null;
        }
    }
}