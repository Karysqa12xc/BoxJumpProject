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
    /// <summary>
    /// 0: die[1 -3]: jump
    /// </summary>
    public AudioClip[] sounds;
    /// <summary>
    /// 0: bg 2: other
    /// </summary>
    public AudioSource[] audioSource;
    public Vector3 playerBeforeDeathPos;
    #endregion
    #region Get Set
    public float LastSpawnPos { get; set; }
    public static MyGameManager Instance { private set; get; }
    //Game state
    public GameState currentGameState { get; set; }
    public long HighScore { get; set; }
    public long Score { get; set; }

    public int MissionTotalJumpAmounts { get; set; }
    public int MissionJumpAmountsInOnRun { get; set; }
    public long MissionScoreAmountReach { get; set; }
    public int NextObsCtrl {get; set;}
    #endregion


    #region //PRIVATE
    private UserInfo userInfoPlayer;
    
    #endregion
    private void Awake()
    {
        uICtrl = GetComponent<UICtrl>();
        player_Input = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Input>();
        Instance = this;
        //Declare some default value
        LastSpawnPos = Const.LAST_POST_ORIGIN; // set the last post to spawn
        currentGameState = GameState.ready;

        NextObsCtrl = UnityEngine.Random.Range(0, uICtrl.mats.Length);
        player_Input.gameObject.GetComponent<Renderer>().material = uICtrl.mats[UnityEngine.Random.Range(0, uICtrl.mats.Length)];
        uICtrl.effectPlayer.GetComponent<Renderer>().material = uICtrl.mats[UnityEngine.Random.Range(0, uICtrl.mats.Length)];
        enviCtrl.START_ORI_OBS.gameObject.GetComponent<Renderer>().material = uICtrl.mats[NextObsCtrl];
    }
    private void Start()
    {
        // Invoke("Test", 2f);
        // Test();
        uICtrl.missionText[0].text = "Score Amount Reach:" + MissionScoreAmountReach + "/50";
        uICtrl.missionText[1].text = "Missions Jumps Amount In On Run: " + MissionJumpAmountsInOnRun + "/50";
        uICtrl.missionText[2].text = "Total Jump Amount: " + MissionTotalJumpAmounts + "/100";
        //Set user info
        string userInfo = PlayerPrefs.GetString("user_info", "{\"HighScore\": 0,\"MissionTotalJumpAmounts\": 0,\"MissionJumpAmountsInOnRun\": 0,\"MissionScoreAmountReach\": 0}");
        userInfoPlayer = JsonUtility.FromJson<UserInfo>(userInfo);
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
            currentGameState = GameState.playing;
        }));
    }
    public void Death()
    {
        audioSource[0].Stop();
        audioSource[1].PlayOneShot(sounds[0]);

        uICtrl.popUpGameObject[2].SetActive(false);
        currentGameState = GameState.death;
        uICtrl.scoreText.text = "";
        //stop movement
        player_Input.rb.useGravity = false;
        player_Input.rb.velocity = Vector3.zero;
        //show pop up game over screen
        uICtrl.showGameOverScreen();
        MissionScoreAmountReach += Score;
        uICtrl.missionText[0].text = "Score Amount Reach: " + MissionScoreAmountReach + "/150";
        //Save user info
        userInfoPlayer.HighScore = HighScore;
        userInfoPlayer.MissionJumpAmountsInOnRun = MissionJumpAmountsInOnRun;
        userInfoPlayer.MissionTotalJumpAmounts  = MissionTotalJumpAmounts;
        userInfoPlayer.MissionScoreAmountReach = MissionScoreAmountReach;
        PlayerPrefs.SetString("user_info", JsonUtility.ToJson(userInfoPlayer));
        PlayerPrefs.Save();
    }
    //22:15
    public void continueGame()
    {
        audioSource[0].Play();
        uICtrl.popUpGameObject[2].SetActive(true);
        uICtrl.popUpGameObject[1].SetActive(false);

        player_Input.transform.position = new Vector3(playerBeforeDeathPos.x, 2f, playerBeforeDeathPos.z);
        StartCoroutine(coolDownTime(delegate
        {
            uICtrl.scoreText.text = "999";
            currentGameState = GameState.playing;
            //active play gravity
            player_Input.rb.useGravity = true;
        }));

    }
    public void newGame()
    {
        audioSource[0].Play();
        uICtrl.popUpGameObject[2].SetActive(true);
        LastSpawnPos = Const.LAST_POST_ORIGIN;
        MissionJumpAmountsInOnRun = 0;
        uICtrl.missionText[1].text = "Missions Jumps Amount In On Run: " + MissionJumpAmountsInOnRun + "/50";
        enviCtrl.SpawnFirstFiveObs();
        uICtrl.popUpGameObject[1].SetActive(false);
        player_Input.transform.position = new Vector3(0.119999997f, -0.839999974f, -0.949999988f);
        StartCoroutine(coolDownTime(delegate
        {
            uICtrl.scoreText.text = "999";
            currentGameState = GameState.playing;
            //active play gravity
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

    public void UpdateScore(long _score)
    {
        this.Score = _score;
        uICtrl.scoreText.text = _score.ToString();
        if (_score > HighScore) HighScore = _score;
    }

    public void UpdateJumpMission()
    {
        MissionTotalJumpAmounts++;
        MissionJumpAmountsInOnRun++;
        uICtrl.missionText[1].text = "Missions Jumps Amount In On Run: " + MissionJumpAmountsInOnRun + "/50";
        uICtrl.missionText[2].text = "Total Jump Amount: " + MissionTotalJumpAmounts + "/100";
    }
    public void Test()
    {
        uICtrl.popUpGameObject[0].SetActive(false);
        uICtrl.ShowDialog("this is dialog content", delegate
        {
            uICtrl.popUpGameObject[0].SetActive(true);
            uICtrl.ShowNotice("Show notice");
        });
    }
}

public enum GameState
{
    ready, playing, death
}

public class UserInfo
{
    public long HighScore;
    public int MissionTotalJumpAmounts;
    public int MissionJumpAmountsInOnRun;
    public long MissionScoreAmountReach;
}
