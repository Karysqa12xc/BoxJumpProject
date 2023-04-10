using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICtrl : MonoBehaviour
{
    //0: Play game screen, 1: Game over screen, 2: Ingame screen, 4: chooseColor screen, 5: dialog
    public GameObject[] popUpGameObject;
    public Text coolDownText, scoreText, dialogText, noticeContentText;
    public Transform popTargetPos;
    public Material[] mats;

    private Action callBackOnBtnOK = null;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var pop in popUpGameObject)
        {
            pop.SetActive(false);
            pop.transform.position = popTargetPos.position;
        }
        popUpGameObject[0].SetActive(true);
    }

    public void PlayGame(){
        popUpGameObject[0].SetActive(false);
        MyGameManager.Instance.StartGame();
    }

    public void showGameOverScreen()
    {
        popUpGameObject[1].SetActive(true);
    }
    public void ChoosePlayerColor(int type){
        MyGameManager.Instance.player_Input.gameObject.GetComponent<Renderer>().material = mats[type];
    }
    public void ChooseroundColor(int type){
        MyGameManager.Instance.enviCtrl.START_ORI_OBS.gameObject.GetComponent<Renderer>().material = mats[type];
        for (int i = 0; i < MyGameManager.Instance.enviCtrl.SpawnedOBS.Count; i++)
        {
            MyGameManager.Instance.enviCtrl.SpawnedOBS[i].gameObject.GetComponent<Renderer>().material = mats[type];
        }    
    }

    public void ShowChooseColorScreen(){
        popUpGameObject[0].SetActive(false);
        popUpGameObject[3].SetActive(true);
    }
    public void turnOffChooseColorScreen(){
        popUpGameObject[3].SetActive(false);
        popUpGameObject[0].SetActive(true);
    }
    public void ShowDialog(string content,Action callback = null){
        //Show notice
        dialogText.text = content;
        if(callback != null){
            callBackOnBtnOK = callback;
        }
        popUpGameObject[4].SetActive(true);
    }

    public void closeDialog(){
        popUpGameObject[4].SetActive(false);
        if(callBackOnBtnOK != null){
            callBackOnBtnOK();
        }
    }

    public void ShowNotice(string content){
        noticeContentText.text = content;
        popUpGameObject[5].SetActive(true);
        Invoke("_ShowNotice", 2f);
    }
    void _ShowNotice(){
        popUpGameObject[5].SetActive(false);
    }

}
