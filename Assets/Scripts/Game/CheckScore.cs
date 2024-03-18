using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CheckScore : MonoBehaviour
{
    private string[] highscoreStrings;
    public static bool isHighScore = false;
    public static List<int> highscores = new(5);
    public static TextMeshProUGUI highScoresText;

    //private void InsertAndShift(int[] array, int index, int value)
    //{
    //    for (int i = array.Length - 1; i > index; i--)
    //    {
    //        array[i] = array[i - 1];
    //    }
    //    array[index] = value;
    //}

    //public static CheckScore instance;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //    //DontDestroyOnLoad(gameObject);
    //}

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
                    if (Int32.TryParse(highscoreStrings[i], out int currentScore))
                    {
                        highscores[i] = currentScore;
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
    }

    private void KeepRecord()
    {
        string highscoresString = string.Join(",", highscores.Select(p => p.ToString()).ToArray());
        PlayerPrefs.SetString("highscores", highscoresString);
    }
}
