using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    TMP_Text ScoreText;
    int Score;

    public void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        Enemy_Behaviour.ScoreChanged += Enemy_Behaviour_ScoreChanged;
        Enemy2Behaviour.ScoreChanged += Enemy_Behaviour_ScoreChanged;
    }

    private void Enemy_Behaviour_ScoreChanged(int x)
    {
        Score += x;
        ScoreText.text = Score.ToString();
    }
}
