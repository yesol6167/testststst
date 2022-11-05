using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class TeleportPoint : MonoBehaviour
{

    public GameObject teleportPos;

    private void OnTriggerEnter(Collider obj)
    {
        GameObject Obj = obj.gameObject;

        if (Obj.GetComponent<Host>().Questing == true)
        // 퀘스트 진행중인 모험가이므로 텔레포트 시킴
        {
            obj.transform.Rotate(Vector3.up * 180);
            this.Teleport(obj.gameObject);
            obj.transform.parent = teleportPos.transform;

            Obj.GetComponent<Host>().StartFinishQuest();

        }
        else // 나머지 경우는 Npc의 오브젝트를 파괴 시킴
        {
            Destroy(Obj);
            SpawnManager.Instance.hostCount--;
        }
    }

    void Teleport(GameObject host)
    {
        host.GetComponent<NavMeshAgent>().Warp(teleportPos.transform.position);
    }
}