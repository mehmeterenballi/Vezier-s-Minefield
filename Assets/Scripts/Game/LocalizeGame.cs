using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizeGame : MonoBehaviour
{
    private readonly string[] englishTexts = { "Replay", "Back", "High Score!", "You Lost!" };
    private readonly string[] turkishTexts = { "Tekrar Oyna", "Geri", "Yüksek Skor!", "Kaybettin!" };

    public List<TextMeshProUGUI> gameObjects = new(4);

    private string currentLanguage;

    private void Start()
    {
        LoadLanguage();
    }

    public void SetLanguage(string language)
    {
        SaveLanguage(language);
        SetTexts(language);
    }

    private void SaveLanguage(string language)
    {
        PlayerPrefs.SetString("ChosenLanguage", language);
    }

    private void SetTexts(string language)
    {
        if (language == "EN")
        {
            SetTexts(englishTexts);
        }
        else if (language == "TR")
        {
            SetTexts(turkishTexts);
        }
        else
        {
            Debug.Log("Language not defined, error occurred.");
        }
    }

    private void SetTexts(string[] texts)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].text = texts[i];
        }

    }

    private void LoadLanguage()
    {
        currentLanguage = PlayerPrefs.GetString("ChosenLanguage", "TR");
        SetLanguage(currentLanguage);
    }
}
