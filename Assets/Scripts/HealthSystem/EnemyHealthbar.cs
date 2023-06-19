using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    
    private HealthSystem HealthSystem;
    Transform healthbar;
    public void Setup(HealthSystem healthSystem)
    {
        HealthSystem = healthSystem;
        HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthbar = GetComponent<Transform>();
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    { 
        healthbar.localScale = new Vector3(HealthSystem.GetHealthPercentage(),1,0);
    }
}
