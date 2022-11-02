using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PubMotelIcon : MonoBehaviour // Bed + Meat + Sleep + Eat æ∆¿Ãƒ‹
{
    public Transform myIconZone;

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myIconZone.position);
        transform.position = pos;
    }

    public void delete()
    {
        Destroy(gameObject);
        GameManager.Instance.Gold += 100;
    }
}
