using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField] private CombatController controller;
    [SerializeField] private Image healthBar;

    private void OnEnable()
    {
        controller.OnHealthChaned += HandleTakeDamage;
    }

    private void HandleTakeDamage(float amount)
    {
        healthBar.fillAmount = amount;
    }

    private void OnDisable()
    {
        controller.OnHealthChaned -= HandleTakeDamage;
    }
}
