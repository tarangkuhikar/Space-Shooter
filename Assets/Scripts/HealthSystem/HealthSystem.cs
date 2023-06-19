using System;
public class HealthSystem
{
    public event EventHandler OnHealthOver;
    public event EventHandler OnHealthChanged;
    public HealthSystem(float maxhealth)
    {
        Health = maxhealth;
        MaxHealth = maxhealth;
    }
    private float Health;
    private float MaxHealth;

    
    public float GetHealth()
    {
        return Health;
    }

    public float GetHealthPercentage()
    {
        return Health / MaxHealth;
    }
    public void Damage(float damageAmount)
    {
        Health -= damageAmount;
        if (Health <= 0)
        {
            Health = 0;
            OnHealthOver?.Invoke(this, EventArgs.Empty);
        }
        
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float healAmount)
    {
        Health += healAmount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
