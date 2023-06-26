using UnityEngine;


public class LivesScript : MonoBehaviour
{
    static TMPro.TMP_Text lives;

    private void Start()
    {
        lives = gameObject.GetComponent<TMPro.TMP_Text>();
    }

    public static void LivesChanged(int playerlives)
    {
        lives.text = playerlives.ToString();
    }
}
