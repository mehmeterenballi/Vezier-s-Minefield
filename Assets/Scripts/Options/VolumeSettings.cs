using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI volumeAmount;

    [SerializeField]
    private Slider slider;

    private void Start() { LoadAudio(); }

    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        volumeAmount.text = ((int)(value * 100)).ToString();
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", AudioListener.volume);
    }

    private void LoadAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", 0.5f);
        AudioListener.volume = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        slider.value = PlayerPrefs.GetFloat("audioVolume");
    }
}
