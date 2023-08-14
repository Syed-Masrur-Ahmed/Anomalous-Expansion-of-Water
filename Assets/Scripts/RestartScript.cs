using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public void Restart(){
        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
