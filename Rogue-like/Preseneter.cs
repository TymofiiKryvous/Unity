using UnityEngine;
using System.Collections.Generic;

public class GamePresenter
{
    private readonly GameModel _model;
    private readonly GameView _view;
    private readonly IObjectFactory _factory;

    public GamePresenter(GameModel model, GameView view, IObjectFactory factory)
    {
        _model = model;
        _view = view;
        _factory = factory;

        Initialize();
    }

    private void Initialize()
    {
        _model.PlayerPosition = _view.player.transform.position;
        _view.DisplayPlayerHitbox();
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = _model.PlayerPosition + Random.insideUnitSphere * _view.spawnRadius;
        spawnPosition.y = 0;

        GameObject enemy = _factory.Instantiate(_view.enemyPrefab, spawnPosition, Quaternion.identity);
        _model.Enemies.Add(enemy);
        _view.SpawnEnemyVisual(enemy, spawnPosition);
    }

    public void UpdateEnemies(float deltaTime)
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in _model.Enemies)
        {
            if (enemy == null) continue;

            // Move enemy towards player
            Vector3 direction = (_model.PlayerPosition - enemy.transform.position).normalized;
            enemy.transform.position += direction * deltaTime * 2f;

            // Check for collision with player
            float distance = Vector3.Distance(enemy.transform.position, _model.PlayerPosition);
            if (distance < 1.5f)
            {
                _model.PlayerHealth -= deltaTime * 5f;
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (GameObject enemy in enemiesToRemove)
        {
            _view.RemoveEnemyVisual(enemy);
            _model.Enemies.Remove(enemy);
        }
    }

    public void Update(float deltaTime)
    {
        UpdateEnemies(deltaTime);
    }
}