using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthbar;
    private HealthSystem HealthSystem;

    
    public void Setup(HealthSystem healthSystem)
    {
        HealthSystem = healthSystem;
        healthbar = GetComponent<Slider>();
        HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        healthbar.value = HealthSystem.GetHealth();
    }
}
