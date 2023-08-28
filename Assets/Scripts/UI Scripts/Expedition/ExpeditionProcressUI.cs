using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpeditionProcressUI : MonoBehaviour
{
    public int index;
    public int expeditionStartIndex;

    [SerializeField] private Image img_Icon;
    [SerializeField] private Slider slider_Timer;
    [SerializeField] private TextMeshProUGUI txt_Timer;
    [SerializeField] private Image img_SkipTimeIcon;

    [Header("Only Expedition One")]
    [SerializeField] private TextMeshProUGUI txt_Reward;
    [SerializeField] private float gemsAmountForUnlockRewards;
    [SerializeField] private float decreaseTimeForWatchVideo;


    [Header("Only Expedition Two")]
    [SerializeField] private TextMeshProUGUI[] all_RewardText;

    [SerializeField] private bool hasSkipit;

    private void OnEnable()
    {
        if (index == 0)
        {
            expeditionStartIndex = UiManager.instance.ui_ExpeditionPanel[index].expeditionStateIndex;
            SetExpeditionOneData();
        }
        else
        {
            expeditionStartIndex = UiManager.instance.ui_ExpeditionPanel[index].expeditionStateIndex;
            SetExpeditionTwoData();
        }

        hasSkipit = DataManager.instance.HasAnySkipitUpForGetReward();

        GameManager.instance.SetSkipitOrAdsAicon(img_SkipTimeIcon);
    }

    private void Update()
    {
        SetData();
    }
    private void SetData()
    {

        int timer = (int)TimeCalculation.instance.currentExpeditionTimer[index];
        slider_Timer.maxValue = TimeCalculation.instance.expeditionTimer[index];
        slider_Timer.value = TimeCalculation.instance.currentExpeditionTimer[index];

        float hours = timer / 3600;
        float minutes = (timer % 3600) / 60;
        float seconds = timer % 60;

        txt_Timer.text = $"{hours} : {minutes} : {seconds}";

    }

    //this is based on time if time is 1 hour then 10 gems and time is 2 hour then 20 gems
    private int CalculateGemsForUnlockItem()
    {
        return 0;
    }


    private void SetExpeditionOneData()
    {
        txt_Reward.text = UiManager.instance.ui_ExpeditionPanel[index].all_ExpeditionItem[expeditionStartIndex].reward.ToString();
    }

    private void SetExpeditionTwoData()
    {
        for (int i = 0; i < 3; i++)
        {
            all_RewardText[i].text = UiManager.instance.ui_ExpeditionPanel[index].all_ExpeditionItems[expeditionStartIndex].all_RewardAmounts[i].ToString();
        }
    }


    public void OnClick_SkipWithAd()
    {

        if (!DataManager.instance.isTutorialPlaying)
        {
            if (hasSkipit)
            {
                DataManager.instance.decreaseSkipIts(1);
            }
            else
            {
                //Show Video
            }

            TimeCalculation.instance.currentExpeditionTimer[index] -= decreaseTimeForWatchVideo;
            this.gameObject.SetActive(false);
        }

    }

    public void OnClick_SkipWithGems()
    {
        if (DataManager.instance.isTutorialPlaying)
        {
            TimeCalculation.instance.currentExpeditionTimer[index] = 0;
            UiManager.instance.ui_tutorial.tutorialState = TutorialState.ExpeditionRewardClaim;
            UiManager.instance.ui_ExpeditionPanel[0].gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            if (!DataManager.instance.HasEnoughGemsForUse(CalculateGemsForUnlockItem()))
            {
                return;
            }
            DataManager.instance.DecreaseGems(CalculateGemsForUnlockItem());

            this.gameObject.SetActive(false);
        }


    }

    public void OnClick_Close()
    {
        if (!DataManager.instance.isTutorialPlaying)
        {
            this.gameObject.SetActive(false);

        }
    }
}
