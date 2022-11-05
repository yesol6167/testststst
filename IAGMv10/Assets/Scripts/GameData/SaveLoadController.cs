using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public CharState.NPC adclass;
    public ADNpc.ADNPC npc;
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
         {
             ADNpc.ADNPC character = new ADNpc.ADNPC();
            ADNpc.ADNPC adnpc = new ADNpc.ADNPC();
             //ADNpc.ADNPC aDNPC = ADNpc.RandomNPC(1);
             
             character.myStatInfo.name = adnpc.myStatInfo.name;
             SaveSystem.Save(adnpc, "Save_001");
             Debug.Log("저장 성공");    
         }
         if (Input.GetKeyDown(KeyCode.R))
         {
             ADNpc.ADNPC LoadData = SaveSystem.Load("Save_001");
             Debug.Log(string.Format("Load 결과: ") + LoadData);
             string temp = JsonUtility.ToJson(adclass);
             string statinfo = JsonUtility.ToJson(npc);
             Debug.Log("Json 정보: " + temp);
             Debug.Log(statinfo);
            
         }
     }

}
