using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = this.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScoreDisplay(int score)
    {
        scoreText.text = score.ToString();
    }
}
