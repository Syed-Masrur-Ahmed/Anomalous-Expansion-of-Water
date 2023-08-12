using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlaybackScript : MonoBehaviour
{
    public TMP_Text TimeScaleText;
    public Button PauseButton;
    public Button PlayButton;
    public Button FastForwardButton;
    public Button SlowDownButton;
    public Button NextFrameButton;
    public Button PreviousFrameButton;

    private float[] speeds = {0.5f, 0.75f, 1f, 1.5f, 2f};
    private int speedIndex = 2;

    private const float recordInterval = 0.09f;
    private float timer;
    private int stateIndex;
    private ParticleScript[] particleScripts;
    private List<ParticleInfo[]> states = new List<ParticleInfo[]>();

    public void ChangeSpeed(int n) {
        speedIndex += n;
        Time.timeScale = speeds[speedIndex];
        TimeScaleText.text = "Time: " + speeds[speedIndex].ToString() + "x"; 
        FastForwardButton.interactable = Convert.ToBoolean(4 - speedIndex);
        SlowDownButton.interactable = Convert.ToBoolean(speedIndex);
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
        ChangeSpeed(0);
        states.RemoveRange(stateIndex + 1, states.Count - stateIndex - 1);
        ShowPauseButton();
    }

    public void FastForward() {   
        ChangeSpeed(1);
        states.RemoveRange(stateIndex + 1, states.Count - stateIndex - 1);
        ShowPauseButton();
    }

    public void SlowDown() {   
        ChangeSpeed(-1);
        states.RemoveRange(stateIndex + 1, states.Count - stateIndex - 1);
        ShowPauseButton();
    }

    void AssumeState(int si) {
        for (int i = 0; i < 125; i++) {
            particleScripts[i].gameObject.transform.position = states[si][i].position;
            particleScripts[i].ChangeTemperature(states[si][i].temperature - particleScripts[i].temperature);
        }
    }

    public void NextFrame() {
        if (stateIndex >= states.Count - 1) Time.timeScale = 5;
        else AssumeState(++stateIndex);
    }

    public void PreviousFrame() {
        AssumeState(--stateIndex);
    }

    public void RecordState() {
        states.Add(particleScripts.Select((x) => new ParticleInfo(x.gameObject.transform.position, x.temperature)).ToArray());
        if (states.Count > 100) states.RemoveAt(0);
        stateIndex = states.Count - 1;
    }

    void Start() {
        timer = 0f;
        stateIndex = -1;
        particleScripts = GameObject.FindGameObjectsWithTag("Particle").Select((x) => x.GetComponent<ParticleScript>()).ToArray();
    }

    void Update() {
        //PreviousFrameButton.interactable = (Time.timeScale == 0 && stateIndex > 0);
        NextFrameButton.interactable = (Time.timeScale == 0);
    }

    void FixedUpdate() {
        timer += Time.deltaTime;
        if (timer >= recordInterval) {
            if (Time.timeScale > 2f) Time.timeScale = 0;
            timer = 0;
            RecordState();
        }
    }
}
