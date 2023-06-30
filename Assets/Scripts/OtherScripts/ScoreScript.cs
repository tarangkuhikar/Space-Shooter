using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    static TMP_Text _scoreText;
    static int _score;
    public void Start()
    {
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        _scoreText = GetComponent<TMP_Text>();
    }

    private void PlayerBehaviour_PlayerDied()
    {
        _score = 0;
    }

    public static void ScoreChanged(int x)
    {
        _score += x;
        _scoreText.text = _score.ToString();
    }
}
