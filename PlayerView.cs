using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class PlayerView : MonoBehaviour {
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private AbilityButton[] abilityButtons;

    public Subject<int> OnAbilityPressed = new Subject<int>();

    public void InitializeButtons(IList<Ability> abilities) {
        for (int i = 0; i < abilityButtons.Length; i++) {
            if (i < abilities.Count) {
                abilityButtons[i].Initialize(abilities[i].Data.icon, i);
                abilityButtons[i].OnPressed = () => OnAbilityPressed.OnNext(i);
            } else {
                abilityButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHealth(float health) => healthBar.fillAmount = health;
    public void UpdateMana(float mana) => manaBar.fillAmount = mana;

    public void UpdateAbilityProgress(float progress) {
        foreach (var button in abilityButtons) {
            button.UpdateRadialFill(progress);
        }
    }

    public void PlayAbilityAnimation(int animationHash) {
        // Logic to trigger the animation (e.g., through an Animator component)
    }
}

[Serializable]
public class AbilityButton : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private Image radialFill;
    public Action OnPressed;

    public void Initialize(Sprite icon, int index) {
        iconImage.sprite = icon;
        GetComponent<Button>().onClick.AddListener(() => OnPressed?.Invoke());
    }

    public void UpdateRadialFill(float fillAmount) => radialFill.fillAmount = fillAmount;
}
