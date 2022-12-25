using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue2
{
    [TextArea]
    public string dialogue;
    public Sprite cg;
    public string name;
}
public class Tutorial2Dialogue : MonoBehaviour
{
    [SerializeField] public TMP_Text Ttdialogue;
    [SerializeField] public TMP_Text Charname;
    [SerializeField] public Image chara;
    [SerializeField] public GameObject Me;
    public GameObject NextBtn;
    //public GameObject NoTouch;
    public TutorialManager T_Manager;
    public Animator Mask;
    public AudioSource TypingSound;

    public int count = 0;
    public bool TalkEnd = false;

    [SerializeField] public Dialogue2[] dialogue;

    void Start()
    {
        ShowDialogue();
        
    }

    public void ShowDialogue()
    {
        Me.gameObject.SetActive(true);
        StartCoroutine(NextDialogue());
    }

    IEnumerator NextDialogue()
    {
        //NoTouch.SetActive(true);
        TalkEnd = true;
        chara.sprite = dialogue[count].cg;
        Charname.text = dialogue[count].name;
        for (int i = 0; i < dialogue[count].dialogue.Length; ++i)
        {
            TypingSound.Play();
            Ttdialogue.text = dialogue[count].dialogue.Substring(0, i + 1);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        count++;
        TalkEnd = false;
        //NoTouch.SetActive(false);
    }

    private void ImmediatelyShow()
    {
        Ttdialogue.text = dialogue[count].dialogue;
        chara.sprite = dialogue[count].cg;
        Charname.text = dialogue[count].name;
        count++;

        TalkEnd = false;
        //NoTouch.SetActive(false);
    }

    private void HideDialogue()
    {
        Me.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.GetComponent<AudioSource>().Play();
            if (TalkEnd == false)
            {
                if (T_Manager.tutorialDialogues[0] == gameObject && count == 2)
                {
                    Mask.gameObject.SetActive(true);
                }
                else if (T_Manager.tutorialDialogues[3] == gameObject && count == 0)
                {
                    Mask.SetTrigger("M_MenuIcon");
                }
                else if (T_Manager.tutorialDialogues[4] == gameObject && count == 3)
                {
                    Mask.SetTrigger("M_Lobby");
                }

                if (count < dialogue.Length)
                {
                    StartCoroutine(NextDialogue());
                }
                else
                {
                    HideDialogue();
                    count++;
                    Time.timeScale = 1.0f;
                }
            }
            else if (TalkEnd == true)
            {
                if (T_Manager.tutorialDialogues[0] == gameObject && count == 2)
                {
                    Mask.gameObject.SetActive(true);
                }
                else if (T_Manager.tutorialDialogues[3] == gameObject && count == 1)
                {
                    Mask.SetTrigger("M_MenuIcon");
                }
                else if (T_Manager.tutorialDialogues[4] == gameObject && count == 3)
                {
                    Mask.SetTrigger("M_Lobby");
                }

                if (count < dialogue.Length)
                {
                    StopAllCoroutines();
                    Ttdialogue.text = null;
                    ImmediatelyShow();
                }
                else
                {
                    HideDialogue();
                    count++;
                    Time.timeScale = 1.0f;
                }
            }
        }
    }
}