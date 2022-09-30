using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : MonoBehaviour
{
    Vector3 dir;
    float totalDist = 0.0f;
    public LayerMask pickMask;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
            {
                dir = hit.point - transform.position;
                totalDist = dir.magnitude;
                dir.Normalize();
            }
        }

        if (totalDist > 0.0f)
        {
            float delta = 1.0f * Time.deltaTime;
            if (totalDist < delta)
            {
                delta = totalDist;
            }
            totalDist -= delta;
            this.transform.Translate(dir * delta, Space.World);
        }
    }
}
