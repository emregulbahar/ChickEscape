using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int Damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= Damage;
            // TODO : UI ANIMATED

            if (currentHealth <= 0)
            {
                //TODO: Player Dead
            }
        }
    }

    public void Heal(int healAmount)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        }
    }


}
