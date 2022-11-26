using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Dialogues : MonoBehaviour
{
    public GameObject[] TutoDialogues;
    public GameObject FadeOut;
    public Animator EyeMask;
    public int TalkerNum = 0;

    private void Start()
    {
        TutoDialogues[0].GetComponent<Tutorial1Dialogue>().PlayDialogue();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().TalkEnd)
            {
                if (TalkerNum == 1 && TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().ContentNum == 4)
                {
                    FadeOut.SetActive(true);
                    StartCoroutine(FadeOutAnim());
                }
                else
                {
                    //다음대화로 넘어감
                    ++(TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().ContentNum);
                    TutoDialogues[TalkerNum].SetActive(false);
                    if (TalkerNum == 0)
                    {
                        if (TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().ContentNum == 1)
                        {
                            EyeMask.SetTrigger("IsEyeOpen2");
                        }
                        TutoDialogues[TalkerNum + 1].SetActive(true);
                        TutoDialogues[TalkerNum + 1].GetComponent<Tutorial1Dialogue>().PlayDialogue();
                        TalkerNum = 1;
                    }
                    else
                    {

                        TutoDialogues[TalkerNum - 1].SetActive(true);
                        TutoDialogues[TalkerNum - 1].GetComponent<Tutorial1Dialogue>().PlayDialogue();
                        TalkerNum = 0;
                    }
                }
            }
            else // TalkEnd가 false일 때
            {
                //대화 스킵
                if (TalkerNum == 0)
                {
                    TutoDialogues[0].GetComponent<Tutorial1Dialogue>().ImmediatelyShow();
                }
                else
                {
                    TutoDialogues[1].GetComponent<Tutorial1Dialogue>().ImmediatelyShow();
                }
            }
        }
    }

    IEnumerator FadeOutAnim()
    {
        while (FadeOut.GetComponent<Image>().fillAmount < 1.0f)
        {
            FadeOut.GetComponent<Image>().fillAmount += 0.5f * Time.deltaTime;
            if (FadeOut.GetComponent<Image>().fillAmount == 1.0f)
            {
                SceneChangeManager.Inst.ChangeScene("Tutorial2");
            }
            yield return null;
        }
    }
}
