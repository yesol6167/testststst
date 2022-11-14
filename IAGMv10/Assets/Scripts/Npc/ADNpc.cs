using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ADNpc : MonoBehaviour
{
    public LayerMask enemyMask = default;

    public GameObject AI_Per;

    public int adtype;

    public Sprite[] NpcProfiles;

    public CharState.NPC myStat;
    public struct ADNPC
    {
        //public string Name; // CharState에서 이름이 두번 나옴 없앨건지 다른 두개의 이름을 사용할지 정해야함 - 주석처리 by 주현
        public CharState.NPC myStatInfo; // 생성될 때 명성에 따른 랜덤 퀘스트 분배
    }
    public static ADNPC RandomNPC(int a, Sprite[] np)
    {
        ADNPC adnpc = new ADNPC();
        adnpc.myStatInfo.profile = np[a]; // 프로필 이미지는 변수 a에 따라 바뀌므로 해당 문장으로 통일
        switch (a) // 수정전 n by주현
        {
            case 0: // 여자 궁수
                adnpc.myStatInfo.profile = np[0];
                adnpc.myStatInfo.name = "셀리나";
                adnpc.myStatInfo.npcJob = CharState.NPCJOB.ACHER;
                adnpc.myStatInfo.agility = 30 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 2)), 0, 99999999); // 10당 10씩증가
                adnpc.myStatInfo.intellect = 20 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 5)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.strong = 10 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 10)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.dexterity = 40 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 1)), 0, 99999999); // 10당 10씩 증가
                adnpc.myStatInfo.health = 20 + (adnpc.myStatInfo.strong / 5); // 10당 2씩 증가
                adnpc.myStatInfo.attack = 15 + (adnpc.myStatInfo.dexterity / 2); // 10당 3씩 증가
                adnpc.myStatInfo.defence = 15 + (adnpc.myStatInfo.strong / 10); // 10당 1씩 증가
                adnpc.myStatInfo.charGrade = Grade(adnpc); // 궁수 공격력
                break;
            case 1: // 여자 도적
                adnpc.myStatInfo.profile = np[1];
                adnpc.myStatInfo.name = "사나";
                adnpc.myStatInfo.npcJob = CharState.NPCJOB.THIEF;
                adnpc.myStatInfo.agility = 40 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 1)), 0, 99999999); // 10당 10씩증가
                adnpc.myStatInfo.intellect = 10 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 10)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.strong = 20 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 5)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.dexterity = 30 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 2)), 0, 99999999); // 10당 10씩 증가
                adnpc.myStatInfo.health = 15 + (adnpc.myStatInfo.strong / 5);
                adnpc.myStatInfo.attack = 20 + (adnpc.myStatInfo.agility / 2);
                adnpc.myStatInfo.defence = 15 + (adnpc.myStatInfo.strong / 10);
                adnpc.myStatInfo.charGrade = Grade(adnpc);
                break;
            case 2: // 여자 마법사
                adnpc.myStatInfo.profile = np[2];
                adnpc.myStatInfo.name = "코리나";
                adnpc.myStatInfo.npcJob = CharState.NPCJOB.WIZARD;
                adnpc.myStatInfo.agility = 20 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 5)), 0, 99999999); // 10당 10씩증가
                adnpc.myStatInfo.intellect = 40 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 1)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.strong = 10 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 10)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.dexterity = 30 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 2)), 0, 99999999); // 10당 10씩 증가
                adnpc.myStatInfo.health = 10 + (adnpc.myStatInfo.strong / 10);
                adnpc.myStatInfo.attack = 30 + (adnpc.myStatInfo.intellect / 2);
                adnpc.myStatInfo.defence = 10 + (adnpc.myStatInfo.strong / 10);
                adnpc.myStatInfo.charGrade = Grade(adnpc);
                break;
            case 3: // 남자 궁수
                adnpc.myStatInfo.profile = np[3];
                adnpc.myStatInfo.name = "브리오";
                adnpc.myStatInfo.npcJob = CharState.NPCJOB.ACHER;
                adnpc.myStatInfo.agility = 30 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 2)), 0, 99999999); // 10당 10씩증가
                adnpc.myStatInfo.intellect = 20 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 5)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.strong = 10 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 10)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.dexterity = 40 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 1)), 0, 99999999); // 10당 10씩 증가
                adnpc.myStatInfo.health = 20 + (adnpc.myStatInfo.strong / 5);
                adnpc.myStatInfo.attack = 15 + (adnpc.myStatInfo.dexterity / 2);
                adnpc.myStatInfo.defence = 15 + (adnpc.myStatInfo.strong / 10);
                adnpc.myStatInfo.charGrade = Grade(adnpc);
                break;
            case 4: // 남자 도적
                adnpc.myStatInfo.profile = np[4];
                adnpc.myStatInfo.name = "단테";
                adnpc.myStatInfo.npcJob = CharState.NPCJOB.THIEF;
                adnpc.myStatInfo.agility = 40 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 1)), 0, 99999999); // 10당 10씩증가
                adnpc.myStatInfo.intellect = 10 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 10)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.strong = 20 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 5)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.dexterity = 30 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 2)), 0, 99999999); // 10당 10씩 증가
                adnpc.myStatInfo.health = 20 + (adnpc.myStatInfo.strong / 5);
                adnpc.myStatInfo.attack = 20 + (adnpc.myStatInfo.agility / 2);
                adnpc.myStatInfo.defence = 10 + (adnpc.myStatInfo.strong / 10);
                adnpc.myStatInfo.charGrade = Grade(adnpc);
                break;
            case 5: // 남자 마법사
                adnpc.myStatInfo.profile = np[5];
                adnpc.myStatInfo.name = "클락";
                adnpc.myStatInfo.npcJob = CharState.NPCJOB.WIZARD;
                adnpc.myStatInfo.agility = 20 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 5)), 0, 99999999); // 10당 10씩증가
                adnpc.myStatInfo.intellect = 40 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 1)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.strong = 10 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 10)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.dexterity = 30 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 2)), 0, 99999999); // 10당 10씩 증가
                adnpc.myStatInfo.health = 10 + (adnpc.myStatInfo.strong / 10);
                adnpc.myStatInfo.attack = 30 + (adnpc.myStatInfo.intellect / 2);
                adnpc.myStatInfo.defence = 10 + (adnpc.myStatInfo.strong / 10);
                adnpc.myStatInfo.charGrade = Grade(adnpc);
                break;
            case 6: // 남자 전사
                adnpc.myStatInfo.profile = np[6];
                adnpc.myStatInfo.name = "레오";
                adnpc.myStatInfo.npcJob = CharState.NPCJOB.WARRIOR;
                adnpc.myStatInfo.agility = 30 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 2)), 0, 99999999); // 10당 10씩증가
                adnpc.myStatInfo.intellect = 10 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 10)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.strong = 40 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 1)), 0, 99999999); // 10당 1씩 증가
                adnpc.myStatInfo.dexterity = 20 + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / 5)), 0, 99999999); // 10당 10씩 증가
                adnpc.myStatInfo.health = 30 + (adnpc.myStatInfo.strong / 5);
                adnpc.myStatInfo.attack = 10 + (adnpc.myStatInfo.strong / 5);
                adnpc.myStatInfo.defence = 10 + (adnpc.myStatInfo.strong / 5);
                adnpc.myStatInfo.charGrade = Grade(adnpc);
                break;
        }
        return adnpc;
    }
    void Start()
    {
        NpcProfiles = Resources.LoadAll<Sprite>("Images/ProfileImages");
        myStat = RandomNPC(adtype, NpcProfiles).myStatInfo; // 스타트에서 캐릭터에서 스탯이 주어짐
        GetComponent<Host>().myStat.RotSpeed = 360.0f;
        GetComponent<Host>().myStat.MoveSpeed = 15.0f;

        switch (adtype)
        {
            case 0: // 여자 궁수
                GetComponent<Host>().myStat.AttackRange = 40.0f;
                GetComponent<Host>().myStat.AttackDelay = 1.0f;
                break;
            case 1: // 여자 도적
                GetComponent<Host>().myStat.AttackRange = 15.0f;
                GetComponent<Host>().myStat.AttackDelay = 2.0f;
                break;
            case 2: // 여자 마법사
                GetComponent<Host>().myStat.AttackRange = 30.0f;
                GetComponent<Host>().myStat.AttackDelay = 1.0f;
                break;
            case 3: // 남자 궁수
                GetComponent<Host>().myStat.AttackRange = 40.0f;
                GetComponent<Host>().myStat.AttackDelay = 1.0f;
                break;
            case 4: // 남자 도적
                GetComponent<Host>().myStat.AttackRange = 15.0f;
                GetComponent<Host>().myStat.AttackDelay = 2.0f;
                break;
            case 5: // 남자 마법사
                GetComponent<Host>().myStat.AttackRange = 30.0f;
                GetComponent<Host>().myStat.AttackDelay = 1.0f;
                break;
            case 6: // 남자 전사
                GetComponent<Host>().myStat.AttackRange = 15.0f;
                GetComponent<Host>().myStat.AttackDelay = 2.0f;
                break;
        }
        GetComponent<Host>().myStat.AP = myStat.attack;
        GetComponent<Host>().myStat.MaxHp = myStat.health;
        GetComponent<Host>().myStat.HP = myStat.health;
        AI_Per.SetActive(false);
    }

    public static CharState.GRADE Grade(ADNPC npc)
    {
        CharState.GRADE grade = new CharState.GRADE();
        // 직업을 확인
        // 직업에 따라 능력치 확인
        // 능력치에 따라 등급 분배

        switch (npc.myStatInfo.npcJob)
        {
            case CharState.NPCJOB.WARRIOR:
                //F~A 등급
                // 명성 : F -> 10, E -> 50, D -> 100, C -> 300, B -> 500, A -> 1000
                if (npc.myStatInfo.strong <= 140) // 기본스텟 40
                {
                    grade = CharState.GRADE.F;
                }
                else if (npc.myStatInfo.strong <= 640 && npc.myStatInfo.strong > 140) // 명성이 600
                {
                    grade = CharState.GRADE.E;
                }
                else if (npc.myStatInfo.strong <= 1640 && npc.myStatInfo.strong > 640)
                {
                    grade = CharState.GRADE.D;
                }
                else if (npc.myStatInfo.strong <= 4640 && npc.myStatInfo.strong > 1640)
                {
                    grade = CharState.GRADE.C;
                }
                else if (npc.myStatInfo.strong <= 9640 && npc.myStatInfo.strong > 4640)
                {
                    grade = CharState.GRADE.B;
                }
                else if (npc.myStatInfo.strong > 9640)
                {
                    grade = CharState.GRADE.A;
                }
                break;
            case CharState.NPCJOB.ACHER:
                if (npc.myStatInfo.dexterity <= 140) // 기본스텟 40
                {
                    grade = CharState.GRADE.F;
                }
                else if (npc.myStatInfo.dexterity <= 640 && npc.myStatInfo.dexterity > 140) // 명성이 600
                {
                    grade = CharState.GRADE.E;
                }
                else if (npc.myStatInfo.dexterity <= 1640 && npc.myStatInfo.dexterity > 640)
                {
                    grade = CharState.GRADE.D;
                }
                else if (npc.myStatInfo.dexterity <= 4640 && npc.myStatInfo.dexterity > 1640)
                {
                    grade = CharState.GRADE.C;
                }
                else if (npc.myStatInfo.dexterity <= 9640 && npc.myStatInfo.dexterity > 4640)
                {
                    grade = CharState.GRADE.B;
                }
                else if (npc.myStatInfo.dexterity > 9640)
                {
                    grade = CharState.GRADE.A;
                }

                //npc.myStatInfo.dexterity

                break;
            case CharState.NPCJOB.WIZARD:
                if (npc.myStatInfo.intellect <= 140) // 기본스텟 40
                {
                    grade = CharState.GRADE.F;
                }
                else if (npc.myStatInfo.intellect <= 640 && npc.myStatInfo.intellect > 140) // 명성이 600
                {
                    grade = CharState.GRADE.E;
                }
                else if (npc.myStatInfo.intellect <= 1640 && npc.myStatInfo.intellect > 640)
                {
                    grade = CharState.GRADE.D;
                }
                else if (npc.myStatInfo.intellect <= 4640 && npc.myStatInfo.intellect > 1640)
                {
                    grade = CharState.GRADE.C;
                }
                else if (npc.myStatInfo.intellect <= 9640 && npc.myStatInfo.intellect > 4640)
                {
                    grade = CharState.GRADE.B;
                }
                else if (npc.myStatInfo.intellect > 9640)
                {
                    grade = CharState.GRADE.A;
                }
                //npc.myStatInfo.intellect

                break;
            case CharState.NPCJOB.THIEF:
                if (npc.myStatInfo.agility <= 140) // 기본스텟 40
                {
                    grade = CharState.GRADE.F;
                }
                else if (npc.myStatInfo.agility <= 640 && npc.myStatInfo.agility > 140) // 명성이 600
                {
                    grade = CharState.GRADE.E;
                }
                else if (npc.myStatInfo.agility <= 1640 && npc.myStatInfo.agility > 640)
                {
                    grade = CharState.GRADE.D;
                }
                else if (npc.myStatInfo.agility <= 4640 && npc.myStatInfo.agility > 1640)
                {
                    grade = CharState.GRADE.C;
                }
                else if (npc.myStatInfo.agility <= 9640 && npc.myStatInfo.agility > 4640)
                {
                    grade = CharState.GRADE.B;
                }
                else if (npc.myStatInfo.agility > 9640)
                {
                    grade = CharState.GRADE.A;
                }
                //npc.myStatInfo.agility

                break;
        }
        return grade;
    }
}
