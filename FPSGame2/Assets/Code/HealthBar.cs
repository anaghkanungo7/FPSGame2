using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    private float CurrentHealth;
    public float MaxHealth = 100f;

    ClassPlayerController player;

    private void Start() {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<ClassPlayerController>();
    }

    private void Update() {
        CurrentHealth = player.health;
        healthBar.fillAmount = CurrentHealth / MaxHealth;
    }


}
