using VContainer;
using VContainer.Unity;
using UnityEngine;

public class PlayerInstaller : LifetimeScope {
    [SerializeField] private PlayerView view;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxMana = 50f;
    [SerializeField] private AbilityData[] abilities;

    protected override void Configure(IContainerBuilder builder) {
        builder.RegisterComponent(view);
        builder.Register<PlayerModel>(Lifetime.Singleton).WithParameter(maxHealth).WithParameter(maxMana).WithParameter(abilities);
        builder.Register<PlayerPresenter>(Lifetime.Singleton).WithParameter(view);
    }
}
