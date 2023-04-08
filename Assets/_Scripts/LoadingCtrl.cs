using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingCtrl : MonoBehaviour
{
    public Text loadingText;
    private int dotCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("loadGameScence", 3f);
        InvokeRepeating("changeText", .25f, .25f);
        StartCoroutine("loadGameSceneAsync");
    }

    void loadGameScence()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator loadGameSceneAsync(){
        yield return new WaitForSeconds(2f);
        AsyncOperation async =  SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while(!async.isDone){
            yield return null;
        }
        Scene sceneGamePlay = SceneManager.GetSceneByBuildIndex(1);
        if(sceneGamePlay != null && sceneGamePlay.isLoaded){
            SceneManager.SetActiveScene(sceneGamePlay);
            gameObject.SetActive(false);
        }
    }

    void changeText()
    {
        dotCount--;
        if (dotCount == -1)
        {
            dotCount = 3;
        }
        loadingText.text = "LOADING";
        if (dotCount > 0)
        {
            for (int i = 1; i < dotCount; i++)
            {
                loadingText.text += ".";
            }
        }
    }

}
