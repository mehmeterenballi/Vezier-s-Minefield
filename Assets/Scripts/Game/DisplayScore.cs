using TMPro;
using UnityEngine;


public class DisplayScore : MonoBehaviour
{
    private ChessGameManager MasterSingleton;

    public GameObject scores;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoresText;

    public bool areScoresDisplayed = false;

    private void Awake()
    {
        MasterSingleton = ChessGameManager.MasterSingleton;
    }

    public void ShowScore()
    {
        if (MasterSingleton.queenCounter == 0)
        {
            MasterSingleton.isWinner = true;
            Debug.Log("You Won");
            currentScoreText.text = MasterSingleton.score + "!";
            currentScoreText.gameObject.SetActive(true);
            if (MasterSingleton.CheckScore.isHighScore)
            {
                highscoreText.gameObject.SetActive(true);
            }
        }
        else
        {
            MasterSingleton.gameOver = true;
            MasterSingleton.GameOver.SetActive(true);
        }

        if (MasterSingleton.CheckScore.highscores.Count > 0)
        {
            MasterSingleton.CheckScore.highScoresText.text = "Best Score: " + MasterSingleton.CheckScore.highscores[0] + "\n";
            if (MasterSingleton.CheckScore.highscores.Count != 1)
            {
                for (int i = 1; i < MasterSingleton.CheckScore.highscores.Count; i++)
                {
                    MasterSingleton.CheckScore.highScoresText.text += (i + 1).ToString() + ": " + MasterSingleton.CheckScore.highscores[i] + "\n";
                }
            }
        }

        MasterSingleton.inGame.SetActive(false);
        scores.SetActive(true);
        MasterSingleton.solutions.SetActive(true);
        MasterSingleton.Utilities.StopTimer();
        areScoresDisplayed = true;
    }

    public void HideScores()
    {
        MasterSingleton.Utilities.StartTimer();
        areScoresDisplayed = false;
    }
}
