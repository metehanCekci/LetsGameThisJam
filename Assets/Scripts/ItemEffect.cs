using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    public enum EffectType
    {
        Health,
        Stamina,
        AttackPower,
        WalkSpeed,
        RunSpeed,
        AttackRange,
        GoldAmount,
        GoldMultiplier,
        AttackCooldown
    }

    public EffectType effectType;
    public int amount;
    public int itemCost; 

    private Button button;
    private Image buttonImage;
    private Color originalColor;

    void Start()
    {
        button = GetComponentInChildren<Button>();
        buttonImage = button.GetComponent<Image>();
        originalColor = buttonImage.color;

        UpdateButtonInteractable();
    }

    void Update()
    {
        UpdateButtonInteractable();
    }

    void UpdateButtonInteractable()
    {
        if (GameManagerScript.instance == null) return;

        bool canAfford = GameManagerScript.instance.GoldAmount >= itemCost;
        button.interactable = canAfford;
        buttonImage.color = canAfford ? originalColor : new Color(originalColor.r * 0.5f, originalColor.g * 0.5f, originalColor.b * 0.5f, originalColor.a);
    }

    public void ApplyEffect()
    {
        if (GameManagerScript.instance == null) return;
        if (GameManagerScript.instance.GoldAmount < itemCost) return;

        GameManagerScript.instance.GoldAmount -= itemCost;

        switch (effectType)
        {
            case EffectType.Health:
                GameManagerScript.instance.Health += amount;
                break;
            case EffectType.Stamina:
                GameManagerScript.instance.MaxStamina += amount;
                break;
            case EffectType.AttackPower:
                GameManagerScript.instance.AttackPower += amount;
                break;
            case EffectType.WalkSpeed:
                GameManagerScript.instance.WalkSpeed += amount;
                break;
            case EffectType.RunSpeed:
                GameManagerScript.instance.RunSpeed += amount;
                break;
            case EffectType.AttackRange:
                GameManagerScript.instance.AttackRange += amount;
                break;
            case EffectType.GoldAmount:
                GameManagerScript.instance.GoldAmount += amount;
                break;
            case EffectType.GoldMultiplier:
                GameManagerScript.instance.goldMultiplier += amount;
                break;
            case EffectType.AttackCooldown:
                GameManagerScript.instance.AttackCooldown -= amount;
                break;
        }

        
        Button btn = GetComponentInChildren<Button>();
        if (btn != null)
        {
            btn.interactable = false;
        }

        Debug.Log($"{effectType} etkisi uygulandý, {itemCost} altýn harcandý.");
    }

}
