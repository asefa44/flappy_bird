using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    private int score = 0;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameManager.instance.OnRestartGame += ResetScore;
        scoreText.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if(score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScoreText.text = score.ToString();   
        }
    }
    public void OnScoreZoneReached()
    {
        score++;
        scoreText.text = score.ToString();
        UpdateBestScore();
    }
    private void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
