using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class StudyCoroutine : MonoBehaviour
{
    IEnumerator moving = null;
    // Start is called before the first frame update
    void Start()
    {
        moving = MovingUp(3.0f);
        //StartCoroutine(MovingUp(3.0f)); 
        //주의) 절대 업데이트 문에 사용하면 안된다
    }

    // Update is called once per frame
    void Update() //업데이트 문에 있는것은 전부 코루틴으로 만들 수 있다.
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Rotating(360.0f));
        }
        moving.MoveNext();
    }

    IEnumerator Rotating(float angle)
    {
        
        //스페이스바를 누를때마다 초당 한바퀴씩 돌도록
        while (angle > 0.0f)
        {
            float delta = 360.0f * Time.deltaTime;
            if (delta > angle)
            {
              delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * delta);
            yield return null;
        }

    }

    //초당 2미터의 속도로 위로 3미터, 아래로 3미터 왔다갔다하게 만드시오
    IEnumerator MovingUp(float d) //코루틴은 무조건 yield return이 포함되어 있어야 한다.
    {
        Vector3 dir = Vector3.up;
        float dist = d;
        while (true)
        {
            float delta = 2.0f * Time.deltaTime;
            if(delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir*delta);
            if (Mathf.Approximately(dist, 0.0f))
            {
                dir = -dir;
                dist = d;

                yield return StartCoroutine(Rotating(180.0f)); 
            }
            //여기 코드가 매프레임마다 반복

            yield return null;

            //핵심. 지연 리턴 0.5초 후 프레임이 바뀌면 다시 실행한다. null은 지연이 없다
            }
        }

   
    
}
