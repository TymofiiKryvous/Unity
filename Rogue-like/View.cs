using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button[] upgradeButtons;

    public void UpdatePlayerStats(int currentXP, int xpForNextLevel, int level)
    {
        xpText.text = $"XP: {currentXP}/{xpForNextLevel}";
        levelText.text = $"Level: {level}";
    }

    public void ShowXPDrop(Vector3 position)
    {
        var xpSphere = Instantiate(Resources.Load<GameObject>("Prefabs/XPSphere"), position, Quaternion.identity);
        Destroy(xpSphere, 5f); // XP-сфера зникає через 5 секунд
    }

    public void ShowUpgradePanel(List<ISpell> spells, Action<ISpell, ISpell> onUpgradeSelected)
    {
        upgradePanel.SetActive(true);

        // Показуємо три варіанти апгрейдів
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            var button = upgradeButtons[i];
            ISpell newSpell = null;
            ISpell upgradedSpell = null;

            if (i < spells.Count) upgradedSpell = spells[i]; // Покращення існуючих
            else newSpell = i == spells.Count ? new FreezeBolt() : new RampageSpell(); // Нові навички

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onUpgradeSelected(newSpell, upgradedSpell));
            button.GetComponentInChildren<Text>().text = newSpell?.Name ?? $"Upgrade {upgradedSpell.Name}";
        }
    }

    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
}