using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{


    #region Public Value
    public Player_Input player_Input;
    public EnviCtrl enviCtrl;
    public UICtrl uICtrl;
    #endregion
    #region Get Set
    public float LastSpawnPos { get; set; }
    public static MyGameManager Instance { private set; get; }
    //Game state
    public GameState currentGameState { get; set; }
    #endregion


    #region //PRIVATE TRANSFROM
    public Vector3 playerBeforeDeathPos;
    #endregion
    private void Awake()
    {
        Instance = this;
        //Declare some defaul value
        LastSpawnPos = Const.LAST_POST_ORIGIN; // set the last post to spawn
        currentGameState = GameState.ready;
    }
    private void Start()
    {
        uICtrl = GetComponent<UICtrl>();
        player_Input = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Input>();
        Invoke("Test", 2f);
        Test();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        uICtrl.popUpGameObject[2].SetActive(true);
        
        StartCoroutine(coolDownTime(delegate
        {
            uICtrl.scoreText.text = "999";
            Debug.Log("Start the game. Player cam move after 2s");
            currentGameState = GameState.playing;
        }));
    }
    public void Death()
    {
        uICtrl.popUpGameObject[2].SetActive(false);
        currentGameState = GameState.death;
        uICtrl.scoreText.text = "";
        //stop movement
        player_Input.rb.useGravity = false;
        player_Input.rb.velocity = Vector3.zero;
        //show pop up game over screen
        uICtrl.showGameOverScreen();
    }
    //22:15
    public void counitnueGame()
    {
        uICtrl.popUpGameObject[2].SetActive(true);
        uICtrl.popUpGameObject[1].SetActive(false);

        player_Input.transform.position = new Vector3(playerBeforeDeathPos.x, 2f, playerBeforeDeathPos.z);
        StartCoroutine(coolDownTime(delegate
        {
            uICtrl.scoreText.text = "999";
            currentGameState = GameState.playing;
            //actice play gravity
            player_Input.rb.useGravity = true;
        }));

    }
    public void newGame()
    {
        uICtrl.popUpGameObject[2].SetActive(true);
        LastSpawnPos = Const.LAST_POST_ORIGIN;
        enviCtrl.SpawnFirstFiveObs();
        uICtrl.popUpGameObject[1].SetActive(false);
        player_Input.transform.position = new Vector3(0.119999997f, -0.839999974f, -0.949999988f);
        StartCoroutine(coolDownTime(delegate
        {
            uICtrl.scoreText.text = "999";
            currentGameState = GameState.playing;
            //actice play gravity
            player_Input.rb.useGravity = true;
        }));
    }
    IEnumerator coolDownTime(Action callback)
    {
        uICtrl.scoreText.text = "";
        int time = 3;
        while (time > 0)
        {
            uICtrl.coolDownText.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }
        uICtrl.coolDownText.text = "";
        if (callback != null)
        {
            callback();
        }
    }
    public void Test(){
        uICtrl.popUpGameObject[0].SetActive(false);
        uICtrl.ShowDialog("this is dialog content", delegate{
            uICtrl.popUpGameObject[0].SetActive(true);
            uICtrl.ShowNotice("Show notice");
        });
    }
}

public enum GameState
{
    ready, playing, death
}
