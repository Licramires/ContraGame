using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [Header("UI Text")]
    public TextMeshProUGUI scoreTextTMP;
    public Text scoreText;
    [Header("Score")]
    public int score = 0;
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        UpdateScoreText();
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        if (scoreTextTMP != null)
            scoreTextTMP.text = "Score: " + score;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}