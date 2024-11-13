using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerPresenter {
    private readonly PlayerModel model;
    private readonly PlayerView view;
    private readonly Queue<Ability> abilityQueue = new Queue<Ability>();
    private readonly CountdownTimer timer = new CountdownTimer(0);

    public PlayerPresenter(PlayerView view, PlayerModel model) {
        this.view = view;
        this.model = model;

        BindView();
        BindModel();
        view.InitializeButtons(model.Abilities);
    }

    private void BindView() {
        view.OnAbilityPressed.Subscribe(index => {
            var ability = model.Abilities[index];
            if (model.UseMana(ability.Data.manaCost)) {
                abilityQueue.Enqueue(ability);
            }
        });
    }

    private void BindModel() {
        model.Health.Subscribe(view.UpdateHealth);
        model.Mana.Subscribe(view.UpdateMana);
    }

    public void Update(float deltaTime) {
        timer.Tick(deltaTime);
        view.UpdateAbilityProgress(timer.Progress);

        if (!timer.IsRunning && abilityQueue.TryDequeue(out Ability ability)) {
            view.PlayAbilityAnimation(ability.Data.animationHash);
            timer.Reset(ability.Data.duration);
            timer.Start();
        }
    }
}

public class CountdownTimer {
    private float duration;
    private float timeRemaining;
    public bool IsRunning { get; private set; }
    public float Progress => IsRunning ? timeRemaining / duration : 0;

    public CountdownTimer(float duration) => Reset(duration);
    public void Tick(float deltaTime) {
        if (IsRunning && (timeRemaining -= deltaTime) <= 0) {
            IsRunning = false;
            timeRemaining = 0;
        }
    }

    public void Reset(float newDuration) {
        duration = newDuration;
        timeRemaining = newDuration;
    }

    public void Start() => IsRunning = true;
    public void Stop() => IsRunning = false;
}
