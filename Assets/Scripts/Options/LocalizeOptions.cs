using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizeOptions : MonoBehaviour
{
    private readonly string[] englishTexts = { "Options", "Audio", "Volume", "Language", "Language", "Back" };
    private readonly string[] turkishTexts = { "Ayarlar", "Ses", "Þiddet", "Dil", "Dil:", "Geri" };

    public List<TextMeshProUGUI> gameObjects = new(10);

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
