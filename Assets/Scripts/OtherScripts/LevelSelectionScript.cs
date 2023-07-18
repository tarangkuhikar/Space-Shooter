using UnityEngine;
using TMPro;
public class LevelSelectionScript : MonoBehaviour
{
    [SerializeField]
    int level;
    [SerializeField]
    TMP_Text levelText;

    private void OnEnable()
    {
        levelText.text = "Level " + level;
    }
    public void LevelSelected()
    {
        GameManager.level = level;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
