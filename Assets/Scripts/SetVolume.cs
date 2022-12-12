﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour {

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;
    private void Start() {
        slider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
    }
    public void SetLevel(float sliderValue) {
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Volume", sliderValue);
    }
}