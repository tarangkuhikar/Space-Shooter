using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    public void Restartgame()
    {
        SceneManager.LoadScene(0);
    }
}
