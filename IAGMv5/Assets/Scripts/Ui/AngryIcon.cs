using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryIcon : MonoBehaviour
{
    public Transform myTarget;
    public GameObject myIcon;
    float AngryTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        transform.position = pos;

        AngryTime += Time.deltaTime;
        if (AngryTime > 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
