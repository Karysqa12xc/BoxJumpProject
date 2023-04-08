using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [SerializeField]private GameObject[] btnGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGame(){
        btnGameObjects[0].SetActive(false);
        MyGameManager.Instance.StartGame();
    }
}
