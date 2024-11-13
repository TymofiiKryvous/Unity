using UnityEngine;
using UniRx;
using System.Collections.Generic;

public class PlayerModel {
    public ReactiveProperty<float> Health { get; private set; }
    public ReactiveProperty<float> Mana { get; private set; }
    public List<Ability> Abilities { get; private set; } = new List<Ability>();

    public PlayerModel(float maxHealth, float maxMana, AbilityData[] abilitiesData) {
        Health = new ReactiveProperty<float>(maxHealth);
        Mana = new ReactiveProperty<float>(maxMana);

        foreach (var data in abilitiesData) {
            Abilities.Add(new Ability(data));
        }
    }

    public void TakeDamage(float amount) => Health.Value = Mathf.Max(Health.Value - amount, 0);
    public void Heal(float amount) => Health.Value = Mathf.Min(Health.Value + amount, Health.Value);
    public bool UseMana(float amount) {
        if (Mana.Value >= amount) {
            Mana.Value -= amount;
            return true;
        }
        return false;
    }
}

public class Ability {
    public readonly AbilityData Data;
    public Ability(AbilityData data) => Data = data;
}
