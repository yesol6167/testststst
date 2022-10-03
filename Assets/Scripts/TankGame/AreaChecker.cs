using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    public Transform myBox;
    public Transform myTank;

    public Transform[] PoleList; //public ¹è¿­
    Vector3[] checkDirs = null;



    // Start is called before the first frame update
    void Start()
    {
        checkDirs = new Vector3[PoleList.Length];
        for(int i = 0; i<PoleList.Length; ++i)
        {
            checkDirs[i] = Vector3.Cross(Vector3.up, PoleList[(i + 1) % PoleList.Length].position - PoleList[i].position).normalized;
        }
      


    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        for(int i = 0; i < checkDirs.Length; ++i)
        {
            Vector3 dir = (myTank.position - PoleList[i].position).normalized;
            if (Vector3.Dot(dir,checkDirs[i]) > 0.0f)
            {
                ++count;
            }
            else
            {
                break;
            }

        }

        if(count == checkDirs.Length)
        {
            myBox.Rotate(Vector3.up * 360.0f * Time.deltaTime);
        }
    }
}
