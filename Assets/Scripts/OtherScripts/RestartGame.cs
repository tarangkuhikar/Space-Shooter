using UnityEngine;

public class RestartGame : MonoBehaviour
{

    public void Restartgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void LevelSelectionScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
