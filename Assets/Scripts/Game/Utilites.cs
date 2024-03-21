using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        LoadAudio();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void LoadAudio()
    {
        audioSource.volume = PlayerPrefs.GetFloat("audioVolume", 0.5f);
    }

    public void StartTimer()
    {
        // Kronometreyi baþlat
        ChessGameManager.MasterSingleton.isRunning = true;
    }

    public void StopTimer()
    {
        // Kronometreyi durdur
        ChessGameManager.MasterSingleton.isRunning = false;
    }
}
