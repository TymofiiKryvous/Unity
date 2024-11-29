using System.Collections.Generic;
using UnityEngine;

public class GamePresenter
{
    private readonly GameModel model;
    private readonly PlayerView view;

    public GamePresenter(GameModel model, PlayerView view)
    {
        this.model = model;
        this.view = view;

        model.InitializePlayer(new Vector3(0, 0, 0)); // Початкова позиція гравця
        GameEvents.OnLevelUp += HandleLevelUp;
    }

    public void Update(float deltaTime)
    {
        if (model.IsPaused) return; // Гра на паузі під час апгрейда

        model.UpdateCooldowns(deltaTime);

        foreach (var enemy in model.GetEnemies())
        {
            enemy.UpdateMovement(deltaTime);
        }

        UpdatePlayerMovement(deltaTime);

        view.UpdatePlayerStats(model.CurrentXP, model.XPForNextLevel, model.Level);
    }

    private void UpdatePlayerMovement(float deltaTime)
    {
        var player = model.Player;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = player.transform.position.z;

        float speed = 5f; // Швидкість гравця
        player.transform.position = Vector3.MoveTowards(player.transform.position, mousePosition, speed * deltaTime);
    }

    public void CastSpells()
    {
        var playerPosition = model.Player.transform.position;
        foreach (var spell in model.Spells)
        {
            if (spell.IsReady)
            {
                spell.Cast(playerPosition, model.GetEnemies());
            }
        }
    }

    public void SpawnEnemy(Vector3 position)
    {
        model.SpawnEnemy(position);
    }

    public void HandleEnemyDefeat(Enemy enemy)
    {
        model.EnemyDefeated(enemy);
        view.ShowXPDrop(enemy.transform.position);
    }

    private void HandleLevelUp(GameModel model)
    {
        view.ShowUpgradePanel(model.Spells, UpgradeSelected);
    }

    private void UpgradeSelected(ISpell newSpell, ISpell upgradedSpell)
    {
        model.SelectUpgrade(newSpell, upgradedSpell);
        view.HideUpgradePanel();
    }
}
