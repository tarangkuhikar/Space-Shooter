using UnityEngine;


public class LivesScript : MonoBehaviour
{
    static TMPro.TMP_Text _lives;

    private void Start()
    {
        _lives = gameObject.GetComponent<TMPro.TMP_Text>();
    }
 
    public static void LivesChanged(int playerlives)
    {
        _lives.text = playerlives.ToString();
    }
}
