using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI volumeAmount;

    [SerializeField]
    private Slider slider;

    public AudioSource audioSource;

    private void Start()
    {
        LoadAudio();
        //volumeAmount.text = ((int)(PlayerPrefs.GetFloat("audioVolume", 0.5f) * 100)).ToString();
    }

    public void SetAudio(float value)
    {
        audioSource.volume = value;
        //AudioListener.volume = value;
        volumeAmount.text = ((int)(value * 100)).ToString();
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", audioSource.volume);
    }

    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("audioVolume", 0.5f);
            audioSource.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
    }
}
