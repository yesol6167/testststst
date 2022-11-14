using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheckZone : MonoBehaviour
{
    public bool BlockChk = false;

    private void OnTriggerStay(Collider obj) // 해당 구역에 Npc가 서있으면 새로운 Npc 생성 중단
    {
        BlockChk = true;
    }

    private void OnTriggerExit(Collider obj)
    {
        BlockChk = false;
    }
}
