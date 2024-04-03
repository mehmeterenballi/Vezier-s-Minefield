using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    private ChessGameManager MasterSingleton;
    public AudioSource audioSource;

    private void Awake()
    {
        MasterSingleton = ChessGameManager.MasterSingleton;
    }

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
        MasterSingleton.isRunning = true;
    }

    public void StopTimer()
    {
        // Kronometreyi durdur
        MasterSingleton.isRunning = false;
    }
}
