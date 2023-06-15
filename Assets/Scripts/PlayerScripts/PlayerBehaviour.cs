using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public HealthBar healthBar;
    private void Start()
    {
        HealthSystem playerhealth = new HealthSystem(100);
        healthBar.Setup(playerhealth);

    }
}
