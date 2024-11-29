using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInstaller : LifetimeScope
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        // Реєстрація фабрики
        builder.Register<GameFactory>(Lifetime.Singleton)
            .WithParameter("playerPrefab", playerPrefab)
            .WithParameter("enemyPrefab", enemyPrefab);

        // Реєстрація моделі, в'ю і презентера
        builder.Register<GameModel>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<PlayerView>();
        builder.Register<GamePresenter>(Lifetime.Singleton);
    }
}

public class GameFactory
{
    private readonly GameObject playerPrefab;
    private readonly GameObject enemyPrefab;

    public GameFactory(GameObject playerPrefab, GameObject enemyPrefab)
    {
        this.playerPrefab = playerPrefab;
        this.enemyPrefab = enemyPrefab;
    }

    public GameObject CreatePlayer(Vector3 position)
    {
        return Object.Instantiate(playerPrefab, position, Quaternion.identity);
    }

    public Enemy CreateEnemy(Vector3 position)
    {
        var enemyObject = Object.Instantiate(enemyPrefab, position, Quaternion.identity);
        return enemyObject.GetComponent<Enemy>();
    }

    public ISpell CreateFreezeBolt()
    {
        return new FreezeBolt();
    }
}