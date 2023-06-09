using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviCtrl : MonoBehaviour
{
    [SerializeField] private GameObject obs_prefabs;
    [SerializeField] private Transform obs_parent;
    public List<OBS_CTRL> SpawnedOBS { private set; get; }

    public GameObject START_ORI_OBS;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFirstFiveObs();
    }

    public void SpawnFirstFiveObs()
    {
        SpawnedOBS = new List<OBS_CTRL>();
        if (SpawnedOBS == null)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnNextObs();
            }
        }
        else
        {
            for (int i = 0; i < SpawnedOBS.Count; i++)
            {
                SpawnedOBS[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < 5; i++)
            {
                SpawnNextObs();
            }
        }
    }
    public void SpawnNextObs()
    {
        //random lengh
        int rand = UnityEngine.Random.Range(4, 10);
        //random distance
        int randDis = UnityEngine.Random.Range(1, Const.MAX_OBS_DIS);
        GameObject groundObj = spawnObs(rand);
        groundObj.transform.position = new Vector3(0, -3.9f,
        MyGameManager.Instance.LastSpawnPos + rand
        + randDis);
        groundObj.SetActive(true);
        MyGameManager.Instance.LastSpawnPos += (rand + randDis);
    }
    GameObject spawnObs(int randomLenght)
    {
        //if there is a element in the list has length = rand lenghtobs && it has after of playe
        for (int i = 0; i < SpawnedOBS.Count; i++)
        {
            if (SpawnedOBS[i].lenghtOfOBS == randomLenght && !MyGameManager.Instance.enviCtrl.SpawnedOBS[i].gameObject.activeInHierarchy)
            {
                return SpawnedOBS[i].gameObject;
            }

        }

        //if not
        GameObject groundObj = Instantiate(obs_prefabs, obs_parent, false);
        groundObj.transform.localScale = new Vector3(groundObj.transform.localScale.x, groundObj.transform.localScale.y, randomLenght);
        OBS_CTRL obs_CTRL = groundObj.GetComponent<OBS_CTRL>();
        obs_CTRL.lenghtOfOBS = randomLenght;
        SpawnedOBS.Add(obs_CTRL);
        return groundObj;



    }
    // Update is called once per frame
    void Update()
    {

    }

    //Object Pooling - mục đích sử dụng lại những vật thể cụ để spawn object
}
