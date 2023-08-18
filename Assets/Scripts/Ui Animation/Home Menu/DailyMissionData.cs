using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyMissionData : MonoBehaviour
{
    public int index;

    public RewardType rewardType;
    public Image img_RewardIcon;
    public TextMeshProUGUI txt_Description;
    public TextMeshProUGUI txt_MissionValue;
    public TextMeshProUGUI txt_RewardAmount;
    public TextMeshProUGUI txt_SliderAmountText;
    public RectTransform img_DailyMissionComplate;
    public Slider slider_RewardComplate;
    public Button btn_Claim;

    public bool isDailyMissionClaimed;
    public bool isDailyMissionFinished;
    public bool isGemsReward;

    public void CheckForRewardIsClaimed()
    {
        if (isDailyMissionClaimed)
        {
            btn_Claim.gameObject.SetActive(true);
        }
    }

    public void OnClick_DailyMissionClaimButton()
    {
        btn_Claim.interactable = false;
        UiManager.instance.ui_Reward.ui_RewardSummary.SetRewardSummaryData(img_RewardIcon.sprite, txt_RewardAmount.text);
        UiManager.instance.ui_Reward.ui_RewardSummary.gameObject.SetActive(true);
        slider_RewardComplate.value = 0.99f;
        img_DailyMissionComplate.gameObject.SetActive(true);
    }
}
