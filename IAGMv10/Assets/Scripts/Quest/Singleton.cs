using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GameManager : Singleton<GameManager> 형태로 사용
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance; // 싱글톤 패턴을 사용하기 위한 인스턴스 변수

    public static T Instance // 인스턴스에 접근하기 위한 프로퍼티
    {
        get 
        {
            if(instance == null) // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            {
                instance = (T)FindObjectOfType(typeof(T)); // 똑같은 것이 있는지 한번더 확인

                if(instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T)); // 없다면 생성
                    instance = obj.GetComponent<T>(); 
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(transform.parent != null && transform.root != null) // 부모나 최상위 폴더안에 있을 경우
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
