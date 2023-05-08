using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private Slider gameVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        gameVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void Awake()
    {
        gameVolumeSlider.value = PlayerPrefs.GetFloat("GameMusicVolume", .5f);
    }

    // Change the Volume set in the Player Preferences (saved settings)
    private void OnVolumeChanged(float value)
    {
        Debug.Log($"Volume: {value}");
        PlayerPrefs.SetFloat("MenuMusicVolume", value);
        PlayerPrefs.SetFloat("GameMusicVolume", value);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}