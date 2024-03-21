using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public GameObject scores;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoresText;

    public bool areScoresDisplayed = false;
    public void ShowScore()
    {
        if (ChessGameManager.MasterSingleton.queenCounter == 0)
        {
            ChessGameManager.MasterSingleton.isWinner = true;
            Debug.Log("You Won");
            currentScoreText.text = ChessGameManager.MasterSingleton.score + "!";
            currentScoreText.gameObject.SetActive(true);
            if (ChessGameManager.MasterSingleton.CheckScore.isHighScore)
            {
                highscoreText.gameObject.SetActive(true);
            }
        }
        else
        {
            ChessGameManager.MasterSingleton.gameOver = true;
            ChessGameManager.MasterSingleton.GameOver.SetActive(true);
        }

        if (ChessGameManager.MasterSingleton.CheckScore.highscores.Count > 0)
        {
            ChessGameManager.MasterSingleton.CheckScore.highScoresText.text = "Best Score: " + ChessGameManager.MasterSingleton.CheckScore.highscores[0] + "\n";
            if (ChessGameManager.MasterSingleton.CheckScore.highscores.Count != 1)
            {
                for (int i = 1; i < ChessGameManager.MasterSingleton.CheckScore.highscores.Count; i++)
                {
                    ChessGameManager.MasterSingleton.CheckScore.highScoresText.text += (i + 1).ToString() + ": " + ChessGameManager.MasterSingleton.CheckScore.highscores[i] + "\n";
                }
            }
        }

        ChessGameManager.MasterSingleton.inGame.SetActive(false);
        scores.SetActive(true);
        ChessGameManager.MasterSingleton.solutions.SetActive(true);
        ChessGameManager.MasterSingleton.Utilities.StopTimer();
        areScoresDisplayed = true;
    }

    public void HideScores()
    {
        ChessGameManager.MasterSingleton.Utilities.StartTimer();
        areScoresDisplayed = false;
    }
}
