using UnityEngine;
using UnityEngine.UI;

public class UIStatusBars : MonoBehaviour
{
    [Header("Health Bar")]
    public Image healthFillImage;

    [Header("Stamina Bar")]
    public Image staminaFillImage;

    private float currentStamina;
    private float displayedHealth;

    void Update()
    {
        // Make sure the GameManager instance exists
        if (GameManagerScript.instance == null)
            return;

        // Smoothly update health
        displayedHealth = Mathf.Lerp(displayedHealth, GameManagerScript.instance.Health, Time.deltaTime * 10f);
        float healthPercent = GameManagerScript.instance.MaxHealth > 0
            ? displayedHealth / GameManagerScript.instance.MaxHealth
            : 0f;
        healthFillImage.fillAmount = healthPercent;

        // Smoothly update stamina
        currentStamina = Mathf.Lerp(currentStamina, GameManagerScript.instance.MaxStamina, Time.deltaTime * 10f);
        float staminaPercent = GameManagerScript.instance.MaxStamina > 0
            ? currentStamina / GameManagerScript.instance.MaxStamina
            : 0f;
        staminaFillImage.fillAmount = staminaPercent;
    }
}
