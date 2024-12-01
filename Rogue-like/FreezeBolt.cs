using System.Collections.Generic;
using UnityEngine;

public class FreezeBolt : ISpell
{
    public string Name => "Freeze Bolt";
    public bool IsReady => cooldownTimer <= 0;

    private float cooldownTimer = 0;
    private float cooldown = 5f;
    private float freezeDuration = 3f;
    private float damage = 50f;

    public void Cast(Vector3 casterPosition, List<EnemyModel> enemies)
    {
        if (!IsReady) return;

        cooldownTimer = cooldown;

        // Найти ближайшего врага
        EnemyModel closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(casterPosition, enemy.Position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            closestEnemy.TakeDamage(damage);
            // "Заморозка" как логическое состояние можно реализовать
            // если нужно, добавим флаг "IsFrozen" в EnemyModel.
        }
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= deltaTime;
        }
    }

    public void Upgrade()
    {
        damage += 20f;
        cooldown = Mathf.Max(1f, cooldown - 0.5f);
    }
}