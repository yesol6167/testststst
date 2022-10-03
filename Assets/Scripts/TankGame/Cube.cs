using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject orgEft = null;
    Vector3 dir;
    float totalDist = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        dir = this.transform.right;
        
        //dir = new Vector3(1, 0, 0);
        totalDist = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        return;
        if (totalDist > 0.0f) 
        { 
        float delta = 5.0f * Time.deltaTime;
        
            if (totalDist < delta)

            {
                delta = totalDist;
            }

            totalDist -= delta;
            transform.Translate(delta * dir, Space.World);
            //transform.position += dir * delta;

            if (Mathf.Approximately(totalDist, 0.0f))
            {
                dir = -dir;
                totalDist = 18.0f;
               
            }
        }

    }

    public void OnDelete()
    {
        Instantiate(orgEft, transform.position, Quaternion.identity);
        // Quaternion.identity 회전이 없다
        Destroy(gameObject);
    }

    private void OnDestroy() //OnDestroy 함수는 프로그램을 중지할때도 실행됨
    {
        
    }
}
