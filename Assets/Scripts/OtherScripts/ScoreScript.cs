using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    static TMP_Text ScoreText;
    static int Score;
    public void Start()
    {
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        ScoreText = GetComponent<TMP_Text>();
    }

    private void PlayerBehaviour_PlayerDied()
    {
        Score = 0;
    }

    public static void ScoreChanged(int x)
    {
        Score += x;
        ScoreText.text = Score.ToString();
    }
}
