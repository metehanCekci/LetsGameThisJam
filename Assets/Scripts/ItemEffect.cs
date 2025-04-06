using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    public enum EffectType
    {
        MaxHealth = 0,
        Stamina = 1,
        AttackPower = 2,
        WalkSpeed = 3,
        RunSpeed = 4,
        AttackRange = 5,
        GoldMultiplier = 6,
        AttackCooldown = 7,
        Lifesteal = 8,
        CritChance = 9,
        DodgeChance = 10,
        MissingHealthDamage = 11
    }


    public EffectType effectType;
    public float amount;
    public float itemCost;

    public GameObject grayOverlay;

    private Button button;
    private bool isPurchased = false;

    void Start()
    {
        button = GetComponentInChildren<Button>();

        if (grayOverlay != null)
            grayOverlay.SetActive(true); // başta tüm overlay'ler açık

        button.interactable = true;
        UpdateButtonInteractable();
    }

    void Update()
    {
        if (!isPurchased)
            UpdateButtonInteractable();
    }

    void UpdateButtonInteractable()
    {
        if (GameManagerScript.instance == null) return;

        bool canAfford = GameManagerScript.instance.GoldAmount >= itemCost;

        if (isPurchased)
        {
            button.interactable = false;

            if (grayOverlay != null)
                grayOverlay.SetActive(false); // satın alındıysa kapanır
        }
        else
        {
            button.interactable = canAfford;

            if (grayOverlay != null)
                grayOverlay.SetActive(!canAfford); // gold yoksa açık, varsa kapalı
        }
    }

    public void ApplyEffect()
    {
        if (GameManagerScript.instance == null) return;
        if (GameManagerScript.instance.GoldAmount < itemCost) return;
        if (isPurchased) return;

        GameManagerScript.instance.GoldAmount -= itemCost;

        switch (effectType)
        {
            case EffectType.MaxHealth: GameManagerScript.instance.Health += amount; break;
            case EffectType.Stamina: GameManagerScript.instance.MaxStamina += amount; break;
            case EffectType.AttackPower: GameManagerScript.instance.AttackPower += amount; break;
            case EffectType.WalkSpeed: GameManagerScript.instance.WalkSpeed += amount; break;
            case EffectType.RunSpeed: GameManagerScript.instance.RunSpeed += amount; break;
            case EffectType.AttackRange: GameManagerScript.instance.AttackRange += amount; break;
            case EffectType.GoldMultiplier: GameManagerScript.instance.goldMultiplier += amount; break;
            case EffectType.AttackCooldown: GameManagerScript.instance.AttackCooldown -= amount; break;

            case EffectType.Lifesteal:
                GameManagerScript.instance.Lifesteal += amount;
                GameManagerScript.instance.hasLifesteal = true;
                break;

            case EffectType.CritChance:
                GameManagerScript.instance.critChance += amount;
                break;

            case EffectType.DodgeChance:
                GameManagerScript.instance.dodgeChance += amount;
                break;

            case EffectType.MissingHealthDamage:
                GameManagerScript.instance.missingHealthDamageBonus += amount;
                break;


        }

        isPurchased = true;
        button.interactable = false;

        if (grayOverlay != null)
            grayOverlay.SetActive(true);

        Debug.Log($"{effectType} etkisi uygulandı, {itemCost} altın harcandı.");
        Transform alindiText = transform.Find("Alindi");
        if (alindiText != null)
        {
            alindiText.gameObject.SetActive(true);
        }
    }
    public bool IsPurchased()
    {
        return isPurchased;
    }
    public void ResetItem()
    {
        isPurchased = false;

        if (grayOverlay != null)
            grayOverlay.SetActive(true);

        if (button != null)
            button.interactable = true;

        Transform alindiText = transform.Find("Alindi");
        if (alindiText != null)
            alindiText.gameObject.SetActive(false);
    }

}