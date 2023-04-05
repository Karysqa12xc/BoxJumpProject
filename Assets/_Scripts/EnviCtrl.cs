using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviCtrl : MonoBehaviour
{
    [SerializeField] private GameObject obs_prefabs;
    [SerializeField] private Transform obs_parent;
    
    // Start is called before the first frame update
    void Start()
    {
        obs_prefabs = GameObject.FindGameObjectWithTag("Ground");
        
        for(int i = 0; i < 20; i++){
            GameObject groundObj =  Instantiate(obs_prefabs, obs_parent, false);
            //random lengh
            int rand = Random.Range(4, 5);
            int randDis = Random.Range(2, 4);
            groundObj.transform.localScale = new Vector3(groundObj.transform.localScale.x, groundObj.transform.localScale.y, rand);
            groundObj.GetComponent<OBS_CTRL>().lenghtOfOBS = rand;
            groundObj.transform.position = new Vector3(0 , -3.469741f,
            MyGameManager.Instance.LastSpawnPos + rand
            + randDis + i);
            MyGameManager.Instance.LastSpawnPos += (rand + randDis);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
