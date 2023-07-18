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
        _scoreText.text = PlayerPrefs.GetInt("_highScore", 0).ToString();
    }

    private void PlayerBehaviour_PlayerDied()
    {

        if (_score > PlayerPrefs.GetInt("_highScore", 0))
        {
            PlayerPrefs.SetInt("_highScore", _score);
        }

        _score = 0;
    }


    public static void ScoreChanged(int x)
    {
        _score += x;
        _scoreText.text = _score.ToString();
    }

}