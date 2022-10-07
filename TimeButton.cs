using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TimeButton : ArrowRotate
{
    public GameObject Play;
    public GameObject Pause;
    public GameObject FastPlay;
    
    public enum STATE
    {
        PLAY, PAUSE, FAST_PLAY
    }
    public STATE ButtonState = STATE.PLAY;
    void ChangeState(STATE button)
    {
        if (ButtonState == button) return;
        ButtonState = button;
        switch (ButtonState)
        {
            case STATE.PLAY:
                StopAllCoroutines();
                StartCoroutine(OnPlay(GoTime));
                break;
            case STATE.PAUSE:
                StopAllCoroutines();
                break;
            case STATE.FAST_PLAY:
                StopAllCoroutines();
                StartCoroutine(OnFast(GoTime));
                break;

        }
    }
    public void OnButtonPlay()
    {
        ChangeState(STATE.PLAY);
    }
    public void OnButtonPause()
    {
        ChangeState(STATE.PAUSE);
    }

    public void OnButtonFast()
    {
        ChangeState(STATE.FAST_PLAY);
    }

}
