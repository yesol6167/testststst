using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    public Transform myBox;
    public Transform Left;
    public Transform Right;
    public Transform myTank;

    Vector3 checkDir = Vector3.zero;

    bool bPass = false;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 hori = Right.position - Left.position;
        checkDir =  Vector3.Cross(Vector3.up, hori);
        checkDir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tank = (myTank.position - Left.position).normalized;
        if (Vector3.Dot(checkDir, tank) > 0.0f)
        {
            bPass = true; //Ελ°ϊ
        }
        else
        {
            bPass = false;
        }

        if (bPass)
        {
            myBox.Rotate(Vector3.up * 180.0f * Time.deltaTime);
        }
    }
}
