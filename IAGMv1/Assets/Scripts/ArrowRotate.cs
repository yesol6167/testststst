using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotate : MonoBehaviour
{
    public Transform myRotateArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myRotateArea.Rotate(Vector3.back * (360.0f/600.0f) * Time.deltaTime);
    }
}
