using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInstaller : LifetimeScope
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameView gameView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GameFactory>(Lifetime.Singleton)
            .WithParameter("enemyPrefab", enemyPrefab);

        builder.Register<GameModel>(Lifetime.Singleton);
        builder.RegisterComponent(gameView);
        builder.Register<GamePresenter>(Lifetime.Singleton);
    }
}

public class GameFactory
{
    private readonly GameObject enemyPrefab;

    public GameFactory(GameObject enemyPrefab)
    {
        this.enemyPrefab = enemyPrefab;
    }

    public EnemyModel CreateEnemy(Vector3 position)
    {
        return new EnemyModel(position, 3f, 100f);
    }

    public ISpell CreateFreezeBolt()
    {
        return new FreezeBolt();
    }
}
