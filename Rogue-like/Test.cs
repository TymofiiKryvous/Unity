using System;
using System.Collections.Generic;
using UnityEngine;

class Program {
    static void Main(string[] args) {
      
        var gameFactory = new GameFactory(null, null); 
        var gameModel = new GameModel(gameFactory);

        gameModel.InitializePlayer(Vector3.zero);
        Console.WriteLine("Игрок создан!");

       
        gameModel.SpawnEnemy(new Vector3(2, 0, 0));
        gameModel.SpawnEnemy(new Vector3(4, 0, 0));
        Console.WriteLine("Враги созданы!");

        DisplayGameState(gameModel);

 
        Console.WriteLine("\n=== Начало игры ===");
        for (int frame = 0; frame < 10; frame++) {
            Console.WriteLine($"\nКадр {frame + 1}");

            gameModel.UpdateCooldowns(1f);

   
            foreach (var spell in gameModel.Spells) {
                if (spell.IsReady) {
                    spell.Cast(gameModel.Player.transform.position, gameModel.GetEnemies());
                    Console.WriteLine($"Заклинание {spell.Name} применено!");
                }
            }

            foreach (var enemy in gameModel.GetEnemies()) {
                enemy.UpdateMovement(gameModel.Player.transform.position, 3f, 1f);
            }

    
            DisplayGameState(gameModel);
        }

        Console.WriteLine("\n=== Игра завершена ===");
    }

    static void DisplayGameState(GameModel gameModel) {
        Console.WriteLine($"\nСостояние игрока:");
        Console.WriteLine($"  Уровень: {gameModel.Level}");
        Console.WriteLine($"  XP: {gameModel.CurrentXP}/{gameModel.XPForNextLevel}");

        Console.WriteLine($"Враги:");
        foreach (var enemy in gameModel.GetEnemies()) {
            Console.WriteLine($"  Враг на позиции {enemy.Position}, здоровье: {enemy.Health}");
        }
    }
}
