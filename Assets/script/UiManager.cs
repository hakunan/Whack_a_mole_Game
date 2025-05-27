using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] GameObject gameOverScreen;

    private void Start()
    {
        SetScoreText(0);
        SetTimerText(0);
    }
    public void SetTimerText(float time)
    {
        timerText.text = "TIME : " + Mathf.Floor(time);
    }
    public void SetScoreText(float score)
    {
        scoreText.text = "SCORE : " + score;
    }
    public void SetCurrentLevelText(string level)
    {
        currentLevelText.text = "CurrentLevel : " + level;
    }
    public void GameoverScreenActive()
    {
        gameOverScreen.SetActive(true);
    }
}
