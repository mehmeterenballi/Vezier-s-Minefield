using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public ChessGameManager gameManager;

    public static TextMeshProUGUI highscoreText;
    public static TextMeshProUGUI currentScoreText;
    public static GameObject scores;

    public static TextMeshProUGUI highScoresText;

    public static bool areScoresDisplayed = false;
    public static void ShowScore()
    {
        if (ChessGameManager.MasterSingleton.queenCounter == 0)
        {
            ChessGameManager.MasterSingleton.isWinner = true;
            Debug.Log("You Won");
            currentScoreText.text = ChessGameManager.MasterSingleton.score + "!";
            currentScoreText.gameObject.SetActive(true);
            if (CheckScore.isHighScore)
            {
                highscoreText.gameObject.SetActive(true);
            }
        }
        else
        {
            ChessGameManager.MasterSingleton.gameOver = true;
            ChessGameManager.MasterSingleton.GameOver.SetActive(true);
        }

        if (CheckScore.highscores.Count > 0)
        {
            CheckScore.highScoresText.text = "Best Score: " + CheckScore.highscores[0] + "\n";
            if (CheckScore.highscores.Count != 1)
            {
                for (int i = 1; i < CheckScore.highscores.Count; i++)
                {
                    CheckScore.highScoresText.text += (i + 1).ToString() + ": " + CheckScore.highscores[i] + "\n";
                }
            }
        }

        ChessGameManager.MasterSingleton.inGame.SetActive(false);
        scores.SetActive(true);
        ChessGameManager.MasterSingleton.solutions.SetActive(true);
        Utilites.StopTimer();
        areScoresDisplayed = true;
    }

    public static void HideScores()
    {
        Utilites.StartTimer();
        areScoresDisplayed = false;
    }
}
