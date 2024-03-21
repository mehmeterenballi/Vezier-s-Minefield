using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CheckScore : MonoBehaviour
{
    private string[] highscoreStrings;
    public bool isHighScore = false;
    public List<int> highscores = new(5);
    public TextMeshProUGUI highScoresText;

    public void ManageScore(int score)
    {
        highscoreStrings = PlayerPrefs.GetString("highscores", "").Split(",");
        isHighScore = false;

        if (ChessGameManager.MasterSingleton.queenCounter == 0)
        {
            if (highscoreStrings[0] == "")
            {
                highscores.Add(score);
                //Debug.Log("highsscores array length: " + highscores.Length);
                isHighScore = true;
                KeepRecord();
            }
            else if (highscoreStrings.Length < 5)
            {
                for (int i = 0; i < highscoreStrings.Length; i++)
                {
                    Debug.Log("highscoreStrings.Length: " + highscoreStrings.Length);
                    if (Int32.TryParse(highscoreStrings[i], out int currentScore))
                    {
                        Debug.Log("highscores.Count: " + highscores.Count);
                        highscores.Insert(i, currentScore);
                        if (highscores.Count > highscoreStrings.Length)
                        {
                            highscores.RemoveAt(highscores.Count - 1);
                        }
                        if (score == highscores[i])
                        {
                            isHighScore = true;
                            break;
                        }
                        if (score < highscores[i])
                        {
                            isHighScore = true;
                            highscores.Insert(i, score);
                            KeepRecord();
                            break;
                        }
                        else if (score > highscores.Max() && highscores.Count < 5)
                        {
                            isHighScore = true;
                            highscores.Insert(highscores.Count, score);
                            KeepRecord();
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("Invalid score: " + highscoreStrings[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < highscoreStrings.Length; i++)
                {
                    if (Int32.TryParse(highscoreStrings[i], out int currentScore))
                    {
                        highscores.Insert(i, currentScore);
                        Debug.Log("highscores size: " + highscores.Count + ", i: " + i);
                        if (score < highscores[i] && i != 5 && score != highscores[i + 1] && score != highscores[i])
                        {
                            highscores.Insert(i, currentScore);
                            highscores.RemoveAt(highscores.Count - 1);
                            isHighScore = true;
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("Invalid score: " + highscoreStrings[i]);
                    }
                }
            }
        }
        if (highscores.Count >= 5)
        {
            highscores.RemoveRange(4, highscores.Count - 4);
        }
    }

    private void KeepRecord()
    {
        string highscoresString = string.Join(",", highscores.Select(p => p.ToString()).ToArray());
        PlayerPrefs.SetString("highscores", highscoresString);
    }
}
