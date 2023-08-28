using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PassiveUpgradeUI : MonoBehaviour
{
    [Header("Required Scripts")]
    public PassiveRewardInfoUI ui_PassiveRewardInfo;
    public PassiveUpgradeSelectionUI ui_PassiveUpgradeSelection;
    public int coinsForUpgrade;


    private void OnEnable()
    {
        coinsForUpgrade = DataManager.instance.GetPassiveUpgradeCoinAmount();
        ui_PassiveUpgradeSelection.gameObject.SetActive(true);
    }

    public void IncreaseCoins()
    {
        coinsForUpgrade += 100;
        DataManager.instance.SetPassiveUpgradeCoinAmount(coinsForUpgrade);
        coinsForUpgrade = DataManager.instance.GetPassiveUpgradeCoinAmount();
    }
}
