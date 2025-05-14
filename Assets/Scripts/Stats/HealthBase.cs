using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [Header("Health Stats")]
    public float maxHealth = 50f;


    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount) 
    {
        _currentHealth -= amount;
        Debug.Log($"Tomou {amount} de dano, vida atual: {_currentHealth}");

        if (_currentHealth <= 0) 
        {
            Kill();
        }
    }

    public void Kill() 
    {
        Debug.Log("Objeto morreu");
        OnKill();
    }

    protected virtual void OnKill()
    {
        Destroy(gameObject);
    }
}
