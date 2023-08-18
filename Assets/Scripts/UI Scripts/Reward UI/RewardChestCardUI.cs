using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardChestCardUI : MonoBehaviour
{
    [SerializeField] private Image img_RewardIcon;
    [SerializeField] private TextMeshProUGUI txt_RewardAmount;


    public void DisableCards()
    {
        img_RewardIcon.gameObject.SetActive(false);
        txt_RewardAmount.gameObject.SetActive(false);
    }
    public void SetRewardCardData(Sprite _imgReward , int _rewardAmount)
    {
        img_RewardIcon.gameObject.SetActive(true);
        txt_RewardAmount.transform.parent.gameObject.SetActive(true);
        img_RewardIcon.sprite = _imgReward;
        txt_RewardAmount.text = _rewardAmount.ToString();
    }
}
