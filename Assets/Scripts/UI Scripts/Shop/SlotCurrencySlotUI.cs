using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotCurrencySlotUI : MonoBehaviour
{

    public Image img_Reward;
    public TextMeshProUGUI txt_RewardAmount;
    public TextMeshProUGUI txt_PriceAmount;


    public void SetData(int rewardAmount , int price)
    {
        txt_RewardAmount.text = rewardAmount.ToString();
        txt_PriceAmount.text = price.ToString();
    }
}
