using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaybackScript : MonoBehaviour
{
    public TMP_Text TimeScaleText;
    public Button PauseButton;
    public Button PlayButton;
    public Button FastForwardButton;
    public Button SlowDownButton;
    private float[] timeScales = {0.5f, 0.75f, 1f, 1.5f, 2f};
    private int timeIndex = 2;

    public void ChangeTimeIndex(int n) {
        timeIndex += n;
        Time.timeScale = timeScales[timeIndex];
        TimeScaleText.text = "Time: " + timeScales[timeIndex].ToString() + "x"; 
        FastForwardButton.interactable = Convert.ToBoolean(4 - timeIndex);
        SlowDownButton.interactable = Convert.ToBoolean(timeIndex);
    }

    public void ShowPlayButton() {
        PauseButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);
    }

    public void ShowPauseButton() {
        PauseButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);
    }
    
    public void Pause() {
        Time.timeScale = 0;
        ShowPlayButton();
    }    

    public void Play() {
        ChangeTimeIndex(0);
        ShowPauseButton();
    }

    public void FastForward() {   
        ChangeTimeIndex(1);
        ShowPauseButton();
    }

    public void SlowDown() {   
        ChangeTimeIndex(-1);
        ShowPauseButton();
    }
}
