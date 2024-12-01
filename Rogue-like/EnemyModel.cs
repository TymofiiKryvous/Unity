using UnityEngine;

public class EnemyModel
{
    public Vector3 Position { get; private set; }
    public float Health { get; private set; }
    private readonly float speed;

    public EnemyModel(Vector3 position, float speed, float health)
    {
        Position = position;
        this.speed = speed;
        Health = health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void UpdateMovement(Vector3 targetPosition, float deltaTime)
    {
        Position = Vector3.MoveTowards(Position, targetPosition, speed * deltaTime);
    }
}
