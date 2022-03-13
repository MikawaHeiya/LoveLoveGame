using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableInfo : MonoBehaviour
{
    public float maxHealth;
    public float maxMana;

    public float basicAttack;
    public float attackDamageOffset;
    public float exDamageOffset;
    public float manaRecover;
    public float attackManaCost;
    public float guardManaCost;
    public float exManaCost;
    public float moveSpeed;
    public float gpFrameCount;
    public float gpBonus;
    public float gpBonusExistTime;

    public event System.Action<float> HealthChanged;
    public event System.Action<float> ManaChanged;
    public event System.Action PlayedDied;

    private float currentHealth;
    public float Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value < 0 ? 0 : value > maxHealth ? maxHealth : value;
            OnHealthChanged(currentHealth);
        }
    }

    private float currentMana;
    public float Mana
    {
        get { return currentMana; }
        set
        {
            currentMana = value < 0 ? 0 : value > maxMana ? maxMana : value;
            OnManaChanged(currentMana);
        }
    }

    private void OnHealthChanged(float health)
    {
        HealthChanged?.Invoke(health);

        if (health <= 0)
        {
            OnPlayerDied();
        }
    }

    private void OnManaChanged(float mana)
    {
        ManaChanged?.Invoke(mana);
    }

    private void OnPlayerDied()
    {
        PlayedDied?.Invoke();
    }

    private void Start()
    {
        Health = maxHealth;
        Mana = maxMana;
    }

    private void FixedUpdate()
    {
        Mana += manaRecover * maxMana * Time.deltaTime;
    }
}
