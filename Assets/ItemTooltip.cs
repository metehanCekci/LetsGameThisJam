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
        effect = GetComponentInParent<ItemEffect>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null && effect != null)
        {
            tooltip.SetActive(true);
            tooltipText.text = $"{TranslateEffect(effect.effectType)} +{effect.amount}";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
            tooltip.SetActive(false);
    }

    string TranslateEffect(ItemEffect.EffectType type)
    {
        switch (type)
        {
            case ItemEffect.EffectType.Health: return "Can";
            case ItemEffect.EffectType.Stamina: return "Dayan�kl�l�k";
            case ItemEffect.EffectType.AttackPower: return "Sald�r� G�c�";
            case ItemEffect.EffectType.WalkSpeed: return "Y�r�me H�z�";
            case ItemEffect.EffectType.RunSpeed: return "Ko�ma H�z�";
            case ItemEffect.EffectType.AttackRange: return "Sald�r� Menzili";
            case ItemEffect.EffectType.GoldAmount: return "Alt�n";
            case ItemEffect.EffectType.GoldMultiplier: return "Alt�n �arpan�";
            case ItemEffect.EffectType.AttackCooldown: return "Sald�r� Bekleme S�resi";
            default: return type.ToString();
        }
    }
}
