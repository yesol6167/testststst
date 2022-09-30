using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayer : CharacterMovement, IBattle
{
    public LayerMask pickMask = default;
    public LayerMask enemyMask = default;

    Transform myTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치에서 내부의 가상공간으로 뻗어 나가는 레이를 만든다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // 레이어 마스크에 해당하는 오브젝트가 선택되었는지 확인한다.
            if(Physics.Raycast(ray, out hit, 1000.0f, enemyMask))
            {
                myTarget = hit.transform;
                AttackTarget(myTarget);
            }
            else if (Physics.Raycast(ray, out hit, 1000.0f, pickMask))
            {
                base.MoveToPosition(hit.point); //부모에 있는 함수
            }
        }
/*
        if (Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAttacking"))
        {
            myAnim.SetTrigger("Attack");
        }
*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 300.0f); //위로 300만큼의 힘을 가하는것
        }
        
    }

    

    private void OnCollisionExit(Collision collision) //바닥에서 떨어질때
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", true);
        }
      
    }

    private void OnCollisionEnter(Collision collision) //바닥에 붙었을때
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", false);
        }
    }

    public void OnDamage(float dmg)
    {
        myAnim.SetTrigger("Damage");
    }

    public void AttackTarget()
    {
        if (myTarget.GetComponent<IBattle>().IsLive())
        {
            myTarget.GetComponent<IBattle>()?.OnDamage(myStat.AP);
        }  
    }

    public bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f);
    }
}
