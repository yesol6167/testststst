using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Vector3 QuestPos;
    public float MoveSpeed = 3.0f;
    public Animator myAnim;
    public enum STATE
    {
        Create, Idle, Quest, Sleep, Eat, Exit
    }
    public STATE myState = STATE.Create;
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Quest:
                break;
            case STATE.Sleep:
                break;
            case STATE.Eat:
                break;
            case STATE.Exit:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Quest:
                break;
            case STATE.Sleep:
                break;
            case STATE.Eat:
                break;
            case STATE.Exit:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(STATE.Quest);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
