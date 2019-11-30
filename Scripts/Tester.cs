using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public Text text;
    public SceneController scener;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "dsds";
    }
    //public void ctrl(string scene)
    //{
    //    text.text = scene;
    //    int sceneNr = int.Parse(scene);
    //    scener.ChangeScene(scene);
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
