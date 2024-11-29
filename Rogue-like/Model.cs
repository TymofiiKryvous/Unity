using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    private const int BaseXP = 10;

    public int Level { get; private set; } = 1;
    public int CurrentXP { get; private set; } = 0;
    public int XPForNextLevel => BaseXP * (int)Mathf.Pow(2, Level);

    public bool IsPaused { get; private set; } = false;

    public List<ISpell> Spells { get; } = new();

    private readonly List<Enemy> enemies = new();
    private readonly GameFactory factory;

    public GameObject Player { get; private set; }

    public GameModel(GameFactory factory)
    {
        this.factory = factory;
    }

    public void InitializePlayer(Vector3 position)
    {
        Player = factory.CreatePlayer(position);
        Spells.Add(factory.CreateFreezeBolt());
    }

    public void AddXP(int amount)
    {
        CurrentXP += amount;
        if (CurrentXP >= XPForNextLevel)
        {
            CurrentXP -= XPForNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;
        IsPaused = true; // Зупиняємо гру до вибору апгрейда
        GameEvents.OnLevelUp?.Invoke(this);
    }

    public void SelectUpgrade(ISpell newSpell = null, ISpell upgradedSpell = null)
    {
        if (newSpell != null) Spells.Add(newSpell);
        if (upgradedSpell != null) upgradedSpell.Upgrade();

        IsPaused = false; // Продовжуємо гру
    }

    public void SpawnEnemy(Vector3 position)
    {
        var enemy = factory.CreateEnemy(position);
        enemies.Add(enemy);
    }

    public void EnemyDefeated(Enemy enemy)
    {
        enemies.Remove(enemy);
        AddXP(10); // Наприклад, 10 XP за ворога
    }

    public List<Enemy> GetEnemies() => enemies;

    public void UpdateCooldowns(float deltaTime)
    {
        foreach (var spell in Spells)
        {
            spell.UpdateCooldown(deltaTime);
        }
    }
}
