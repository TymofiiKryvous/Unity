using System.Collections.Generic;
using UnityEngine;

public class RampageSpell : ISpell
{
    public string Name => "Rampage";
    public bool IsReady => cooldownTimer <= 0;

    private float cooldownTimer = 0;
    private float cooldown = 10f;
    private float radius = 5f;
    private float damage = 30f;

    public void Cast(Vector3 casterPosition, List<EnemyModel> enemies)
    {
        if (!IsReady) return;

        cooldownTimer = cooldown;

        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(casterPosition, enemy.Position) <= radius)
            {
                enemy.TakeDamage(damage);
            }
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
        damage += 10f;
        radius += 1f;
    }
}
