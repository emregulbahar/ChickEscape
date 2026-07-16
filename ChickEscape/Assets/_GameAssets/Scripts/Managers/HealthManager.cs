using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{

    public static HealthManager Instance { get; private set;}

    public event Action OnPlayerDeath;

    [Header("References")]
    [SerializeField] private PlayerHealtUI _playerHealtUI;
    [Header("Settings")]
    [SerializeField] private int maxHealth = 3;

    

    private int currentHealth;

    private void Awake() 
    {
        Instance = this;
    }


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int Damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= Damage;
            _playerHealtUI.AnimateDamage();

            if (currentHealth <= 0)
            {
                OnPlayerDeath?.Invoke();
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
