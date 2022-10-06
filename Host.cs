using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : MonoBehaviour
{
    public enum STATE
    {
        Create, Idle, Roaming, Quest, Eat, Sleep, Exit
    }
    public STATE myState = default;
    public CharacterStat stat;
    public Vector3 Line;

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
            case STATE.Roaming:
                break;
            case STATE.Quest:
                break;
            case STATE.Eat:
                break;
            case STATE.Sleep:
                break;
            case STATE.Exit:
                SpawnManager._instance.hostCount--;
                SpawnManager._instance.isSpawn[int.Parse(transform.parent.name) - 1] = false;
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
            case STATE.Roaming:
                break;
            case STATE.Quest:
                break;
            case STATE.Eat:
                break;
            case STATE.Sleep:
                break;
            case STATE.Exit:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(STATE.Roaming);
        StartCoroutine(NPCMoving());
        ChangeState(STATE.Idle);
    }
    IEnumerator NPCMoving()
    {
        Vector3 dir = Line - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        while (true)
        {
            float delta = stat.moveSpeed * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
