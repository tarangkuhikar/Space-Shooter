using UnityEngine;

public class RestartGame : MonoBehaviour
{

    public void Restartgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
