using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject enemyPrefab;
    private readonly Dictionary<EnemyModel, GameObject> enemyObjects = new();

    public void UpdatePlayerPosition(Vector3 position)
    {
        playerObject.transform.position = position;
    }

    public void UpdateEnemyPositions(List<EnemyModel> enemies)
    {
        foreach (var enemy in enemies)
        {
            if (!enemyObjects.ContainsKey(enemy))
            {
                var enemyObject = Instantiate(enemyPrefab, enemy.Position, Quaternion.identity);
                enemyObjects[enemy] = enemyObject;
            }

            if (enemy.Health <= 0)
            {
                Destroy(enemyObjects[enemy]);
                enemyObjects.Remove(enemy);
            }
            else
            {
                enemyObjects[enemy].transform.position = enemy.Position;
            }
        }
    }
}
