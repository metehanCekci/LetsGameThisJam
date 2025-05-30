﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
    public ItemEffect itemEffect;
    public TextMeshProUGUI costText;
    public GameObject newFeatureImage;


    public void GenerateItemWithEffect(ItemEffect.EffectType fixedEffect)
    {
        float value = GetFixedStatValue(fixedEffect);      
        float cost = GetFixedGoldCost(fixedEffect);        

        itemEffect.effectType = fixedEffect;
        itemEffect.amount = value;
        itemEffect.itemCost = cost;

        if (costText != null)
            costText.text = $"{cost} Altın";
        if (newFeatureImage != null)
{
    bool isNewUpgrade = fixedEffect == ItemEffect.EffectType.Lifesteal;
    newFeatureImage.SetActive(isNewUpgrade);
}

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
            case ItemEffect.EffectType.AttackRange: return 0.5f;
            case ItemEffect.EffectType.GoldMultiplier: return 0.25f;
            case ItemEffect.EffectType.AttackCooldown: return -0.1f;
            case ItemEffect.EffectType.Lifesteal: return 1f;
            case ItemEffect.EffectType.CritChance: return 0.1f;
            case ItemEffect.EffectType.DodgeChance: return 0.05f;
            case ItemEffect.EffectType.MissingHealthDamage: return 0.5f;

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
            case ItemEffect.EffectType.Lifesteal: return 100f;
            case ItemEffect.EffectType.CritChance: return 60f;
            case ItemEffect.EffectType.DodgeChance: return 80f;
            case ItemEffect.EffectType.MissingHealthDamage: return 90f;



            default: return 100f;
        }
    }
}
