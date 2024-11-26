using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public float spawnRadius = 10f;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject hitboxCircle;

    public void DisplayPlayerHitbox()
    {
        hitboxCircle.SetActive(true);
        hitboxCircle.transform.position = player.transform.position;
    }

    public void SpawnEnemyVisual(GameObject enemy, Vector3 position)
    {
        enemy.transform.position = position;
        enemy.SetActive(true);
    }

    public void RemoveEnemyVisual(GameObject enemy)
    {
        Destroy(enemy);
    }
}
