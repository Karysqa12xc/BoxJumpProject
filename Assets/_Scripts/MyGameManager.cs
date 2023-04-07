using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
   
    
    #region Public Value
    public Player_Input player_Input;
    public EnviCtrl enviCtrl;
    #endregion
    #region Get Set
    public float LastSpawnPos { get; set; }
    public static MyGameManager Instance { private set; get; }
    //Game state
    public GameState currentGameState{get; set;}
    #endregion

    private void Awake()
    {
        Instance = this;
        //Declare some defaul value
        LastSpawnPos = Const.LAST_POST_ORIGIN; // set the last post to spawn
        currentGameState = GameState.ready;
    }
    private void Start() {
        player_Input = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Input>();
        Invoke("StartGame", 2f);
    }
    // Update is called once per frame
    void Update()
    {

    }
    void StartGame(){
        currentGameState = GameState.playing;
    }
    public void Death(){
        currentGameState = GameState.death;
        player_Input.rb.useGravity = false;
        player_Input.rb.velocity = Vector3.zero;
    }
}

public enum GameState
{
    ready, playing, death
}
