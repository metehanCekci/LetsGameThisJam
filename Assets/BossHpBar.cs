using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [SerializeField] private Image hpFillImage; // Fill olarak kullanılan Image
    public EnemyHealthScript healthscript;



    // Call this method whenever the boss takes damage
    private void FixedUpdate() {
        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        if (hpFillImage != null)
        {
            hpFillImage.fillAmount = (float)healthscript.currentHealth / healthscript.maxHealth;

        }
    }
}
