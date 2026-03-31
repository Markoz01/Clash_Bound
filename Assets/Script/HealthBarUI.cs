using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private PlayerHealth playerHealth;

    private void Start()
    {
        StartCoroutine(FindPlayerHealth());
    }

    private IEnumerator FindPlayerHealth()
    {
        while(playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            yield return null;
        }
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider.value = playerHealth.maxHealth;
        playerHealth.CurrentHealth.OnValueChanged += OnHealthChanged;
    }


    private void OnHealthChanged(float oldHealth, float newHealth)
    {
        healthSlider.value = newHealth;
    }

}
