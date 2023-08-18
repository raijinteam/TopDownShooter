using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyPurchaseUI : MonoBehaviour
{
    [SerializeField] private int rewardAmount;
    [SerializeField] private int purchaseAmount;
    [SerializeField] private TextMeshProUGUI txt_RewardAmount;
    [SerializeField] private TextMeshProUGUI txt_PurchaseAmount;


    private void OnEnable()
    {
        txt_RewardAmount.text = "x"+ rewardAmount.ToString();
        txt_PurchaseAmount.text = purchaseAmount.ToString();
    }


    public void OnClick_PuchaseEnergy()
    {
        if (!DataManager.instance.HasEnoughGemsForUse(purchaseAmount))
        {

            return;
        }

        DataManager.instance.IncreaseEnergy(rewardAmount);
        DataManager.instance.DecreaseGems(purchaseAmount);
        this.gameObject.SetActive(false);
    }

    public void Onclick_Close()
    {
        this.gameObject.SetActive(false);
    }
}
