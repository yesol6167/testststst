using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ClockIcon : MonoBehaviour
{
    public GameObject myNotouch;

    public Transform myTarget;
    public Button myButton;

    public Image myTimeArea;
    public float LimitTime = 30.0f; // 제한시간 30초

    public bool TimeOut = false;
    public GameObject myAngryIcon;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeChecking());
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        transform.position = pos;
    }
    IEnumerator TimeChecking()
    {
        myTimeArea.fillAmount = 0.0f;
        float speed = 1.0f / LimitTime;
        while (myTimeArea.fillAmount < 1.0f)
        {
            myTimeArea.fillAmount += speed * Time.deltaTime;
            if (myTimeArea.fillAmount == 1.0f)
            {
                TimeOut = true;
                OnRemveClock();
            }
            yield return null;
        }
    }
    public void OnRemveClock()
    {
        Destroy(gameObject);
    }
}