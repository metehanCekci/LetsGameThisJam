using UnityEngine;
using System.Collections.Generic;

public class MarketManager : MonoBehaviour
{
    public List<ItemGenerator> itemGenerators;

    void Start()
    {
        List<ItemEffect.EffectType> effects = new List<ItemEffect.EffectType>
        {
            ItemEffect.EffectType.Health,
            ItemEffect.EffectType.Stamina,
            ItemEffect.EffectType.AttackPower,
            ItemEffect.EffectType.WalkSpeed,
            ItemEffect.EffectType.RunSpeed,
            ItemEffect.EffectType.AttackRange,
            ItemEffect.EffectType.GoldMultiplier,
            ItemEffect.EffectType.AttackCooldown
        };

        ShuffleList(effects);

        for (int i = 0; i < itemGenerators.Count && i < effects.Count; i++)
        {
            itemGenerators[i].GenerateItemWithEffect(effects[i]);
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
}
