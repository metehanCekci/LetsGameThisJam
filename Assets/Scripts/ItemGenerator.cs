using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
    public ItemEffect itemEffect;
    public TextMeshProUGUI costText;

    public void GenerateItemWithEffect(ItemEffect.EffectType fixedEffect)
    {
        int value = Random.Range(10, 31);
        int cost = CalculateCost(fixedEffect, value);

        itemEffect.effectType = fixedEffect;
        itemEffect.amount = value;
        itemEffect.itemCost = cost;

        if (costText != null)
            costText.text = $"{cost}G";
    }

    int CalculateCost(ItemEffect.EffectType type, int value)
    {
        float multiplier = 1f;

        switch (type)
        {
            case ItemEffect.EffectType.Health: multiplier = 7f; break;
            case ItemEffect.EffectType.Stamina: multiplier = 5f; break;
            case ItemEffect.EffectType.AttackPower: multiplier = 10f; break;
            case ItemEffect.EffectType.WalkSpeed: multiplier = 15f; break;
            case ItemEffect.EffectType.RunSpeed: multiplier = 15f; break;
            case ItemEffect.EffectType.AttackRange: multiplier = 12f; break;
        }

        return Mathf.RoundToInt(value * multiplier);
    }
}
