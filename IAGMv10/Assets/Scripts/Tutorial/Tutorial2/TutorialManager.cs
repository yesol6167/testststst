using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialDialogues;
    public GameObject Mask;
    public bool isTutorialEnd = false;
    //public QuestWindow QuestBtnChk;
    public GameObject ReQuestWindow;
    public GameObject IconArea;
    public GameObject Notouch;
    public Button MenuIcon;
    public Transform PQ_Content;
    public Animator CamManager;
    public bool QuestChk = true;
    public GameObject FadeOut;
    public GameObject WindowArea;
    public GameObject ADMonsterZone;
    public GameObject ADFarmZone;

    void Start()
    {
        StartCoroutine(starttutorial());
        Notouch.SetActive(true);
    }

    IEnumerator starttutorial()
    {
        Time.timeScale = 0.0f;
        yield return new WaitWhile(() => tutorialDialogues[0].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[0].GetComponent<Tutorial2Dialogue>().dialogue.Length); // dialogue1 대화 종료시 다음꺼 실행
        yield return new WaitForSeconds(1.0f);
        SpawnNpc("VL");//마을사람 생성
        //Notouch.SetActive(true);
        yield return new WaitForSeconds(5.8f);
        
        tutorialDialogues[1].SetActive(true);
        Time.timeScale = 0.0f;
        yield return new WaitWhile(() => tutorialDialogues[1].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[1].GetComponent<Tutorial2Dialogue>().dialogue.Length); //dialogue2 대화 종료시 다음꺼 실행
        Notouch.SetActive(false); //
        //IconArea.GetComponentInChildren<ClockIcon>().myButton.onClick.AddListener(C_ClockIcon);
        IconArea.GetComponentInChildren<ClockIcon>().myButton.onClick.AddListener(ClockIconNoTouch);

        yield return new WaitWhile(() => tutorialDialogues[2].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[2].GetComponent<Tutorial2Dialogue>().dialogue.Length);
        Time.timeScale = 0.0f;
        Notouch.SetActive(false);// 추가

        yield return new WaitWhile(() => QuestManager.Instance.RQlist.Count < 1);
        Time.timeScale = 0.0f;
        
        tutorialDialogues[3].SetActive(true);
        Notouch.SetActive(false);
        MenuIcon.onClick.AddListener(C_MenuIcon);
        //yield return new WaitWhile(() => QuestBtnChk.tutorialchk < 1);
        yield return new WaitWhile(() => tutorialDialogues[4].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[4].GetComponent<Tutorial2Dialogue>().dialogue.Length);
        yield return new WaitForSeconds(8.0f);
        //모험가 생성
        SpawnNpc("Q_AD");
        yield return new WaitForSeconds(6.5f);
        Time.timeScale = 0.0f;
       
        tutorialDialogues[5].SetActive(true);
        Notouch.SetActive(true);
        yield return new WaitWhile(() => tutorialDialogues[5].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[5].GetComponent<Tutorial2Dialogue>().dialogue.Length);       
        Notouch.SetActive(false);

        //WindowArea.GetComponentInChildren<QuestInformation>().acception.GetComponent<Button>().onClick.AddListener(AcceptionBtn);


        yield return new WaitWhile(() => ADFarmZone.transform.childCount < 1);
        Notouch.SetActive(false);
        Time.timeScale = 0.0f;
        Mask.GetComponent<Animator>().SetTrigger("M_QuestList");
        tutorialDialogues[6].SetActive(true);

        yield return new WaitWhile(() => ADMonsterZone.transform.childCount < 1);
        Notouch.SetActive(false);
        Time.timeScale = 0.0f;
        Mask.GetComponent<Animator>().SetTrigger("M_QuestList");
        tutorialDialogues[6].SetActive(true);
        //yield return new WaitForSeconds(9.0f); // 모험가 퇴장 -> 전투하러 나갔을 때
        /*Mask.GetComponent<Animator>().SetTrigger("M_QuestList");
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(1.5f); // 마스크 이동
        
        tutorialDialogues[6].SetActive(true);*/





        yield return new WaitWhile(() => tutorialDialogues[6].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[6].GetComponent<Tutorial2Dialogue>().dialogue.Length); //dialogue7 대화 종료시 다음꺼 실행
        Notouch.SetActive(false);
        PQ_Content.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Mask_AD);
        yield return new WaitWhile(() => tutorialDialogues[7].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[7].GetComponent<Tutorial2Dialogue>().dialogue.Length);
        Time.timeScale = 0.0f;
        Mask.GetComponent<Animator>().SetTrigger("M_QuestList");
        PQ_Content.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveListener(Mask_AD);
        PQ_Content.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(BackGuild);
        yield return new WaitWhile(() => QuestManager.Instance.FQlist.Count < 1); // 종료된 퀘스트가 추가되면
        Mask.GetComponent<Animator>().SetTrigger("M_QuestList");
        yield return new WaitWhile(() => SpawnManager.Instance.spawnPoints[0].transform.childCount != 0);
        Time.timeScale = 0.0f;
        
        tutorialDialogues[9].SetActive(true);
        yield return new WaitWhile(() => tutorialDialogues[9].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[9].GetComponent<Tutorial2Dialogue>().dialogue.Length);
        Mask.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SpawnNpc("P_AD"); //펍 모험가 생성
        yield return new WaitForSeconds(7.0f);
        Time.timeScale = 0.0f;
        CamManager.SetTrigger("PubCam");
        Mask.GetComponent<Animator>().SetTrigger("M_AD");
        Mask.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        
        tutorialDialogues[10].SetActive(true);
        yield return new WaitWhile(() => tutorialDialogues[10].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[10].GetComponent<Tutorial2Dialogue>().dialogue.Length);
        yield return new WaitForSeconds(9.0f);
        
        tutorialDialogues[11].SetActive(true);
        yield return new WaitWhile(() => tutorialDialogues[11].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[11].GetComponent<Tutorial2Dialogue>().dialogue.Length);
        CamManager.SetTrigger("MotelCam");
        SpawnNpc("M_AD"); //모텔 모험가 생성
        yield return new WaitForSeconds(17.0f); 
        
        tutorialDialogues[12].SetActive(true);
        yield return new WaitWhile(() => tutorialDialogues[12].GetComponent<Tutorial2Dialogue>().count <= tutorialDialogues[12].GetComponent<Tutorial2Dialogue>().dialogue.Length);
        Mask.SetActive(false);
        CamManager.SetTrigger("LobbyCam");
        yield return new WaitForSecondsRealtime(1.5f);
        FadeOut.SetActive(true);
    }

    public void C_ClockIcon()
    {
        StartCoroutine(CoNextTalk(0.0f, 2));

    }
    public void AcceptionBtn()
    {
        Invoke("Dialogue6", 5.8f);
    }
    public void Dialogue6()
    {
        Mask.GetComponent<Animator>().SetTrigger("M_QuestList");
        Time.timeScale = 0.0f;
        tutorialDialogues[6].SetActive(true);
    }
    public void ClockIconNoTouch()
    {
        Notouch.SetActive(true);
        Time.timeScale = 1.0f;
        StartCoroutine(CoNextTalk(1.0f,2));
    }
    public void C_MenuIcon()
    {
        Notouch.SetActive(true);
        Mask.GetComponent<Animator>().SetTrigger("M_QuestList");
       
        StartCoroutine(CoNextTalk(2.0f, 4));
    }

    public void Mask_AD()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(CoNextTalk(5.0f, 7));
        Mask.GetComponent<Animator>().SetTrigger("M_AD");
    }

    public void BackGuild()
    {
        Mask.GetComponent<Animator>().SetTrigger("M_AD");
        Time.timeScale = 1.0f;
        StartCoroutine(CoNextTalk(1.5f, 8));
    }

    public void SpawnNpc(string npc)
    {
        switch(npc)
        {
            case "VL":
                SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.VL[0], SpawnManager.Instance.spawnPoints[0]) as GameObject;
                SpawnManager.Instance.Host.GetComponent<VLNpc>().job = VLNpc.NPCJOB.COMMONS;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = true;
                SpawnManager.Instance.Host.GetComponent<Host>().purpose = 0;
                break;
            case "Q_AD":
                SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.AD[0], SpawnManager.Instance.spawnPoints[0]) as GameObject;
                int j = UnityEngine.Random.Range(0, QuestManager.Instance.RQlist.Count); //RQ리스트에 있는 퀘스트 중에서 랜덤으로 모험가에게 부여
                SpawnManager.Instance.Host.GetComponent<QuestInformation>().myQuest = QuestManager.Instance.RQlist[j];
                QuestManager.Instance.RQlist.RemoveAt(j);//퀘스트 줬으니깐 제거
                Destroy(QuestManager.Instance.RQuest.transform.GetChild(j).gameObject);
                SpawnManager.Instance.Host.GetComponent<ADNpc>().adtype = 0;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = false;
                SpawnManager.Instance.Host.GetComponent<Host>().People = 1;
                break;
            case "P_AD":
                SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.AD[0], SpawnManager.Instance.spawnPoints[1]) as GameObject;
                SpawnManager.Instance.Host.GetComponent<ADNpc>().adtype = 0;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = false;
                SpawnManager.Instance.Host.GetComponent<Host>().People = 1;
                SpawnManager.Instance.Host.GetComponent<Host>().purpose = 1;
                break;
            case "M_AD":
                SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.AD[0], SpawnManager.Instance.spawnPoints[2]) as GameObject;
                SpawnManager.Instance.Host.GetComponent<ADNpc>().adtype = 0;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = false;
                SpawnManager.Instance.Host.GetComponent<Host>().People = 1;
                SpawnManager.Instance.Host.GetComponent<Host>().purpose = 2;
                break;
        }
    }


    IEnumerator CoNextTalk(float time,int D_num)
    {
        //Notouch.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        tutorialDialogues[D_num].SetActive(true);
       
    }


}
