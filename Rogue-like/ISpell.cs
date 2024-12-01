using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    string Name { get; }
    bool IsReady { get; }
    void Cast(Vector3 casterPosition, List<EnemyModel> enemies);
    void UpdateCooldown(float deltaTime);
    void Upgrade();
}
