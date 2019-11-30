using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int currentScene;
    static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SceneController GetInstance()
    {
        return instance;
    }

    public void ChangeScene(int sceneNr)
    {
        //int sceneNumber = int.Parse(sceneNr);
        SceneManager.LoadScene(sceneNr, LoadSceneMode.Single);

    }
}
