using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInstaller : LifetimeScope
{
    [SerializeField] private GameView gameView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GameModel>(Lifetime.Singleton);
        builder.RegisterComponent(gameView);
        builder.Register<IObjectFactory, ObjectFactory>(Lifetime.Singleton);
        builder.Register<GamePresenter>(Lifetime.Singleton)
               .AsImplementedInterfaces();
    }
}

public interface IObjectFactory
{
    GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
    void Destroy(GameObject obj);
}

public class ObjectFactory : IObjectFactory
{
    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(prefab, position, rotation);
    }

    public void Destroy(GameObject obj)
    {
        Object.Destroy(obj);
    }
}
