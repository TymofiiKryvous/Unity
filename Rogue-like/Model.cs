
using UnityEngine;
using System.Collections.Generic;

public class GameModel
{
    public List<GameObject> Enemies { get; private set; } = new List<GameObject>();
    public float PlayerHealth { get; set; } = 100f;
    public float PlayerMana { get; set; } = 100f;
    public Vector3 PlayerPosition { get; set; }
}