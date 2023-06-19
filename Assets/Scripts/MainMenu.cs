using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayButtonPressed()
    {
        SceneManager.LoadScene("Game");   
    }
}
