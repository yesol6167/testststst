using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainBoard : MonoBehaviour
{
    public GameObject MenuBoard;
    public Image OpenBook;
    public Button MenuButton;
    bool Active = false;


    // Start is called before the first frame update
    void Start()
    {
        MenuBoard.SetActive(Active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAction()
    {
        MenuBoard.SetActive(!Active);
        Active = !Active;

    }
 
  }
   
