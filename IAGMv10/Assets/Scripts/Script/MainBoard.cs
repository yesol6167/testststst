using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainBoard : MonoBehaviour
{
    public GameObject[] MenuBoard;
    public Sprite myCloseBook;
    public Sprite myOpenBook;
    public Button MenuButton;
    bool Active = false;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = myCloseBook;
        for(int i = 0; i < MenuBoard.Length; i++)
        {
            MenuBoard[i].SetActive(Active);
        }
    }
    public void OnAction()
    {
        for (int i = 0; i < MenuBoard.Length; i++)
        {
            MenuBoard[i].SetActive(!Active);
        }
        Active = !Active;

        if(Active)
        {
            gameObject.GetComponent<Image>().sprite = myOpenBook;
        }
        else 
        {
            gameObject.GetComponent<Image>().sprite = myCloseBook;
        }
    }
  }
   
