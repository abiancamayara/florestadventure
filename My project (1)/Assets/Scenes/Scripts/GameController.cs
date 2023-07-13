using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI healthText;

    public int score;
    public TMPro.TextMeshProUGUI scoreText;

    public int totalScore;

    public static GameController instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        totalScore = PlayerPrefs.GetInt("score", score + totalScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        
        PlayerPrefs.SetInt("score", score);
    }

    public void UpdateLives(int value)
    {
        healthText.text = "x " + value.ToString();
    }
}
