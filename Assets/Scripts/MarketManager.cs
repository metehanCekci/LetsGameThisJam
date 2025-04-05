using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class MarketManager : MonoBehaviour
{
    public List<ItemGenerator> itemGenerators;
    public GameObject gameManager;

    void Start()
    {
        if (GameManagerScript.instance == null)
        {
            Instantiate(gameManager);
        }

        List<ItemEffect.EffectType> effects = new List<ItemEffect.EffectType>
        {
            ItemEffect.EffectType.MaxHealth,
            ItemEffect.EffectType.Stamina,
            ItemEffect.EffectType.AttackPower,
            ItemEffect.EffectType.WalkSpeed,
            ItemEffect.EffectType.RunSpeed,
            ItemEffect.EffectType.AttackRange,
            ItemEffect.EffectType.GoldMultiplier,
            ItemEffect.EffectType.AttackCooldown,
            ItemEffect.EffectType.Lifesteal
        }
        ;
        ShuffleList(effects);

        int generatorIndex = 0;

        foreach (var effect in effects)
        {
            if (generatorIndex >= itemGenerators.Count) break;

            if (!CanEffectBeShown(effect)) continue;

            itemGenerators[generatorIndex].GenerateItemWithEffect(effect);
            generatorIndex++;
        }
    }

    bool CanEffectBeShown(ItemEffect.EffectType type)
    {
        switch (type)
        {
            case ItemEffect.EffectType.AttackCooldown:
                return GameManagerScript.instance.AttackCooldown > 0.2f;

            case ItemEffect.EffectType.MaxHealth:
                return GameManagerScript.instance.Health < 160;

            case ItemEffect.EffectType.Stamina:
                return GameManagerScript.instance.MaxStamina < 200;

            case ItemEffect.EffectType.GoldMultiplier:
                return GameManagerScript.instance.goldMultiplier < 2f;

            default:
                return true;
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void RefreshMarket()
    {
        if (GameManagerScript.instance.GoldAmount < 20)
        {
            Debug.Log("Yetersiz altýn! Market yenilenemedi.");
            return;
        }

        GameManagerScript.instance.GoldAmount -= 20;
        Debug.Log("Market yenilendi, 20 altýn harcandý.");

        List<ItemEffect.EffectType> effects = new List<ItemEffect.EffectType>
    {
        ItemEffect.EffectType.MaxHealth,
        ItemEffect.EffectType.Stamina,
        ItemEffect.EffectType.AttackPower,
        ItemEffect.EffectType.WalkSpeed,
        ItemEffect.EffectType.RunSpeed,
        ItemEffect.EffectType.AttackRange,
        ItemEffect.EffectType.GoldMultiplier,
        ItemEffect.EffectType.AttackCooldown,
        ItemEffect.EffectType.Lifesteal
    };

        ShuffleList(effects);

        int generatorIndex = 0;

        foreach (var generator in itemGenerators)
        {
            var effect = generator.GetComponent<ItemEffect>();
            if (effect != null)
            {
                effect.ResetItem(); // önce içeriði sýfýrla
            }

            generator.gameObject.SetActive(false); // sonra görünmez yap
        }

        foreach (var effect in effects)
        {
            if (generatorIndex >= itemGenerators.Count) break;
            if (!CanEffectBeShown(effect)) continue;

            var generator = itemGenerators[generatorIndex];
            generator.gameObject.SetActive(true); // sadece kullanýlacaklarý göster
            generator.GenerateItemWithEffect(effect);
            generatorIndex++;
        }
    }


}
