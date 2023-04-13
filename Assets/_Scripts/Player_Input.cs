using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public Rigidbody rb { private set; get; }
    [SerializeField] private float Speed, maxSpeed;
    [SerializeField] private float jumpVelocity = 10f, fallSpeed = 2.5f, fallDownSpeed = 150;
    
    private void Awake()
    {
        Speed = Const.ORIGIN_SPEED;
        maxSpeed = Const.MAX_SPEED;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyGameManager.Instance.currentGameState != GameState.playing) return;
        //==Player Movement==
        //Player death
        if (isDeath())
        {
            MyGameManager.Instance.Death();
        }
        //Move by z and more than speed 
        if(Speed < maxSpeed) Speed += 0.025f * Time.deltaTime;
    
        transform.Translate(0, 0, Speed * Time.deltaTime);
        //Jump by y
        Jump();
        //Spawn obs if can
        if (MyGameManager.Instance.enviCtrl.SpawnedOBS != null)
        {
            for (int i = 0; i < MyGameManager.Instance.enviCtrl.SpawnedOBS.Count; i++)
            {
                if (transform.position.z > MyGameManager.Instance.enviCtrl.SpawnedOBS[i].gameObject.transform.position.z + 11 &&
                MyGameManager.Instance.enviCtrl.SpawnedOBS[i].gameObject.activeInHierarchy)
                {
                    MyGameManager.Instance.enviCtrl.SpawnedOBS[i].gameObject.SetActive(false);
                    MyGameManager.Instance.enviCtrl.SpawnNextObs();
                    break;
                }
            }
        }

        MyGameManager.Instance.UpdateScore((long)transform.position.z + (long)0.95);
    }

    // void SpawnOBSIfCan()
    // {

    // }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isGround())
            {
                MyGameManager.Instance.audioSource[1].PlayOneShot(MyGameManager.Instance.sounds[Random.Range(1, 4)]);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.velocity = Vector3.up * jumpVelocity;
                MyGameManager.Instance.UpdateJumpMission();
            }
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallSpeed - 1) * Time.deltaTime;
            rb.AddForce(Vector3.down * fallDownSpeed);
        }
    }
    bool isGround() => transform.position.y < -0.14;
    bool isDeath() => transform.position.y <= Const.Death_POS_Y;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            MyGameManager.Instance.playerBeforeDeathPos = other.transform.position;
        }
    }
}
