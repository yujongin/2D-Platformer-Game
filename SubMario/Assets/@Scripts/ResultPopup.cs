using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPopup : MonoBehaviour
{
    [SerializeField]
    private TMP_Text resultTitle;
    [SerializeField]
    private TMP_Text scroeLabel;
    [SerializeField]
    private GameObject highScoreObject;
    [SerializeField]
    private GameObject highScorePopup;



    void OnEnable()
    {
        Time.timeScale = 0;
        if (GameManager.Instance.IsCleared)
        {
            resultTitle.text = "Cleared!";
            scroeLabel.text = GameManager.Instance.TimeLimit.ToString("#.##");
            SaveHighScore();
        }
        else
        {
            resultTitle.text = "failed!";
            scroeLabel.text = "";
            highScoreObject.SetActive(false);
        }

    }

    void SaveHighScore()
    {
        float score = GameManager.Instance.TimeLimit;
        float highscore = PlayerPrefs.GetFloat("highscore", 0);

        if (GameManager.Instance.TimeLimit > highscore)
        {
            highScoreObject.SetActive(true);
            PlayerPrefs.SetFloat("highscore", GameManager.Instance.TimeLimit);
            PlayerPrefs.Save();
        }
        else
        {
            highScoreObject.SetActive(false);
        }


        string currentScoreString = score.ToString("#.##");
        string savedScoreString = PlayerPrefs.GetString("HighScores", "");


        if (savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(",");
            List<string> scoreList = new List<string>(scoreArray);

            for (int i = 0; i < scoreList.Count; i++)
            {
                float savedScore = float.Parse(scoreList[i]);
                if (savedScore < score)
                {
                    scoreList.Insert(i, currentScoreString);
                    break;
                }
            }

            if (scoreArray.Length == scoreList.Count)
            {
                scoreList.Add(currentScoreString);
            }
            if (scoreList.Count > 10)
            {
                scoreList.RemoveAt(10);
            }

            string result = string.Join(",", scoreList);
            PlayerPrefs.SetString("HighScores", result);
        }

        PlayerPrefs.Save();
    }

    public void TryAgainPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void ShowHighScoresPressed()
    {
        highScorePopup.SetActive(true);
    }
}
