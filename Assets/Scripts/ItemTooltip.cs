using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public TextMeshProUGUI tooltipText;

    private ItemEffect effect;

    void Start()
    {
        tooltip.SetActive(false);
        effect = GetComponentInParent<ItemEffect>(); // Hem Item Icon hem Gray için çalýþýr
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null && effect != null && !effect.IsPurchased())
        {
            tooltip.SetActive(true);
            tooltipText.text = $"{TranslateEffect(effect.effectType)} {effect.amount}";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            tooltip.SetActive(false);
        }
    }

    string TranslateEffect(ItemEffect.EffectType type)
    {
        switch (type)
        {
            case ItemEffect.EffectType.MaxHealth: return "Can + ";
            case ItemEffect.EffectType.Stamina: return "Dayanýklýlýk + ";
            case ItemEffect.EffectType.AttackPower: return "Saldýrý Gücü + ";
            case ItemEffect.EffectType.WalkSpeed: return "Yürüme Hýzý + ";
            case ItemEffect.EffectType.RunSpeed: return "Koþma Hýzý + ";
            case ItemEffect.EffectType.AttackRange: return "Saldýrý Menzili + ";
            case ItemEffect.EffectType.GoldAmount: return "Altýn +";
            case ItemEffect.EffectType.GoldMultiplier: return "Altýn Çarpaný + ";
            case ItemEffect.EffectType.AttackCooldown: return "Saldýrý Bekleme Süresi ";
            case ItemEffect.EffectType.Lifesteal: return "Can Çalma + ";
            default: return type.ToString();
        }
    }

}
