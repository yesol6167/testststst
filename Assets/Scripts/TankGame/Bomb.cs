using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bomb : MonoBehaviour
{
    public LayerMask crashMask;
    bool bFire = false;
    public float MoveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (bFire)
        {
            float delta = MoveSpeed * Time.deltaTime;
            Ray ray = new Ray(transform.position, transform.up); //현재 위치, 다음 위치 world 기준 
            if(Physics.Raycast(ray, out RaycastHit hit, delta, crashMask))
            {
                Invoke("CreateTarget", 2.0f); //createtarget 함수를 불러올 수 있음
                hit.transform.GetComponent<Cube>()?.OnDelete();
                // ?은 null 인지 아닌지 확인해주는
            }


            transform.Translate(Vector3.up * delta);
        }
    }

    public void OnFire()
    {
        bFire = true;
        //transform.parent = null;
        transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bFire = false;
        Destroy(this.gameObject);
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        if ((crashMask & 1 << collision.gameObject.layer) != 0)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((crashMask & 1 << other.gameObject.layer) != 0)
        {
           // Invoke("CreateTarget", 2.0f); //createtarget 함수를 불러올 수 있음
           // other.GetComponent<Cube>().OnDelete();
          
        }
    }

    void CreateTarget()
    {

        Destroy(gameObject);
        GameObject obj = Instantiate(Resources.Load("Prefabs\\Tar")) as GameObject;
        //Resources 안에 있는 것만 접근해서 가져올 수 있음
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
    }
}
