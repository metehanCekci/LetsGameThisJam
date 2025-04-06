using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
    public ItemEffect itemEffect;
    public TextMeshProUGUI costText;
    public GameObject newFeatureImage;

    public Image iconImage; // İkon gösterilecek Image objesi
    public Sprite[] statIcons; // 12 ikonluk dizi (Inspector'da sırayla atanacak)

    public void GenerateItemWithEffect(ItemEffect.EffectType fixedEffect)
    {
        itemEffect.effectType = fixedEffect;  // önce tipi ver
        itemEffect.amount = GetFixedStatValue(fixedEffect);
        itemEffect.itemCost = GetFixedGoldCost(fixedEffect);

        if (costText != null)
            costText.text = $"{itemEffect.itemCost}";

        // Yeni gelen özellik etiketi sadece Lifesteal için gösteriliyor
        if (newFeatureImage != null)
            newFeatureImage.SetActive(fixedEffect == ItemEffect.EffectType.Lifesteal);

        // 🔥 İKON DOĞRU TİPLE eşleştirilerek atanıyor
        if (iconImage != null && statIcons != null && statIcons.Length > (int)fixedEffect)
        {
            iconImage.sprite = statIcons[(int)fixedEffect];
            iconImage.gameObject.SetActive(true);
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
            case ItemEffect.EffectType.DodgeChance: return 0.1f;
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
            case ItemEffect.EffectType.CritChance: return 80f;
            case ItemEffect.EffectType.DodgeChance: return 90f;
            case ItemEffect.EffectType.MissingHealthDamage: return 120f;
            default: return 100f;
        }
    }
}
