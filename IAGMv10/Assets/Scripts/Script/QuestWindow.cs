using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestWindow : MonoBehaviour
{
    public static bool QuestActivated = false;
    public GameObject QuestList;
    public GameObject QuestBtn;

    // Start is called before the first frame update
    void Start()
    {
        QuestList.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void TryOpenQuest()
    {
        QuestActivated = !QuestActivated;

        if (QuestActivated) OpenQuest();
        else CloseQuest();
        
    }

    public void OpenQuest()
    {
        QuestList.SetActive(true);
    }

    public void CloseQuest()
    {
        QuestList.SetActive(false);
    }
}
