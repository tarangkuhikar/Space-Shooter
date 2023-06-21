using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    static TMP_Text ScoreText;
    static int Score;
    public void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
    }

    public static void ScoreChanged(int x)
    {
        Score += x;
        ScoreText.text = Score.ToString();
    }
}
