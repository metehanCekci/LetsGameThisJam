using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
    public ItemEffect itemEffect;
    public TextMeshProUGUI costText;

    public void GenerateItemWithEffect(ItemEffect.EffectType fixedEffect)
    {
        float value = GetFixedStatValue(fixedEffect);      
        float cost = GetFixedGoldCost(fixedEffect);        

        itemEffect.effectType = fixedEffect;
        itemEffect.amount = value;
        itemEffect.itemCost = cost;

        if (costText != null)
            costText.text = $"{cost} Altın";
    }

    float GetFixedStatValue(ItemEffect.EffectType type)
    {
        switch (type)
        {
            case ItemEffect.EffectType.MaxHealth: return 20f;
            case ItemEffect.EffectType.Stamina: return 20f;
            case ItemEffect.EffectType.AttackPower: return 3f;
            case ItemEffect.EffectType.WalkSpeed: return 2f;
            case ItemEffect.EffectType.RunSpeed: return 3f;
            case ItemEffect.EffectType.AttackRange: return 2f;
            case ItemEffect.EffectType.GoldMultiplier: return 0.25f;
            case ItemEffect.EffectType.AttackCooldown: return -0.1f;
            default: return 1f;
        }
    }

    float GetFixedGoldCost(ItemEffect.EffectType type)
    {
        switch (type)
        {
            case ItemEffect.EffectType.MaxHealth: return 40f;
            case ItemEffect.EffectType.Stamina: return 30f;
            case ItemEffect.EffectType.AttackPower: return 70f;
            case ItemEffect.EffectType.WalkSpeed: return 25f;
            case ItemEffect.EffectType.RunSpeed: return 30f;
            case ItemEffect.EffectType.AttackRange: return 35f;
            case ItemEffect.EffectType.GoldMultiplier: return 100f;
            case ItemEffect.EffectType.AttackCooldown: return 80f;
            default: return 100f;
        }
    }
}
