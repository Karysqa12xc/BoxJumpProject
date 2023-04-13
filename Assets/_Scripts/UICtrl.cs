using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICtrl : MonoBehaviour
{
    [Header("POPUP In Game")]
    //0: Play game screen, 1: Game over screen, 2: Ingame screen, 4: chooseColor screen, 5: dialog; 6: Mission
    public GameObject[] popUpGameObject;
    [Header("UI Game Text")]
    public Text coolDownText, scoreText, dialogText, noticeContentText, gameOverHighScoreTXT, gameOverScoreTXT;
    [Header("Text mission")]
    /// <summary>
    /// Text mission: 
    /// 0: MissionJumpAmountsInOnJump
    /// 2: MissionTotalJumpAmounts
    /// 1: MissionScoreAmountReach
    /// </summary>
    public Text[] missionText;
    [Header("Default Position UI")]
    public Transform popTargetPos;
    [Header("Skin player and ground")]
    public Material[] mats;
    public ParticleSystem effectPlayer;
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
        gameOverScoreTXT.text = "SCORE: " + MyGameManager.Instance.Score.ToString();
        gameOverHighScoreTXT.text = "HIGH SCORE: " + MyGameManager.Instance.HighScore.ToString();
        popUpGameObject[1].SetActive(true);
    }
    public void ChoosePlayerColor(int type){
        MyGameManager.Instance.player_Input.gameObject.GetComponent<Renderer>().material = mats[type];
        effectPlayer.GetComponent<Renderer>().material = mats[type];
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
    public void showMission(){
        popUpGameObject[6].SetActive(true);
        popUpGameObject[1].SetActive(false);
    }
}
