using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Gold;
    public int Fame;
    // Start is called before the first frame update


    public void Update()
    {
        UIManager.Instance.Gold.text = Gold.ToString();
        UIManager.Instance.Fame.text = Fame.ToString();
    }
}
