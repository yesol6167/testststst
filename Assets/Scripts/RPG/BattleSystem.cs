using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBattle
{
    void OnDamage(float dmg);  //인터페이스는 함수를 구현하면 안된다.
    bool IsLive();
}

public class BattleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
