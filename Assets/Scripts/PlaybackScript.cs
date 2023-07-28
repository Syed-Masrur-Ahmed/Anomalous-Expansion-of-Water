using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaybackScript : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PlayButton;
    
    public void Pause() 
    {
        Time.timeScale = 0;
        PauseButton.SetActive(false);
        PlayButton.SetActive(true);
    }

    public void Play()
    {
        Time.timeScale = 1;
        PauseButton.SetActive(true);
        PlayButton.SetActive(false);
    }

    public void FastForward() 
    {
        Time.timeScale = 1.5f;
        PauseButton.SetActive(true);
        PlayButton.SetActive(false);
    }
}
