using UnityEngine;
using TMPro; // Usar si estás con TextMeshPro
using UnityEngine.UI; // Usar si usas el UI Text normal

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("UI Text")]
    public TextMeshProUGUI scoreTextTMP; // Si usas TextMeshPro
    public Text scoreText;               // Si usas UI Text normal

    [Header("Score")]
    public int score = 0;

    void Awake()
    {
        // Singleton para fácil acceso
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
