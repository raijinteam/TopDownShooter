using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpeditionRunningUI : MonoBehaviour
{
    
    [Header("Require Scripts")]
    [SerializeField] private ExpaditionReward rewardData;

    [Header("Expedition Data")]
    [SerializeField] private int expeditionIndex;
    [SerializeField] private ExpeditionPanelUI expaditionScreen;
    public TextMeshProUGUI txt_expanditionTimer;
    public Slider slider_ExpanditionSlider;

    [Header("Reward 1 Data")]
    [SerializeField] private Image icon_ExpeditionRewardOne;
    [SerializeField] private TextMeshProUGUI rewardAmount1;

    [Header("Reward 2 Data")]
    [SerializeField] private Image icon_ExpeditionRewardTwo;
    [SerializeField] private TextMeshProUGUI rewardAmount2;


    private void Start()
    {
        if(expaditionScreen.myPanelId == 0)
        {
            SetExpeditionOneSliderMaxValue();
        }else if(expaditionScreen.myPanelId == 1)
        {
            SetExpeditionTwoSliderMaxValue();
        }
    }


    private void SetExpeditionOneSliderMaxValue()
    {
        slider_ExpanditionSlider.maxValue = TimeCalculation.instance.expeditionOneTimer;
        slider_ExpanditionSlider.value = TimeCalculation.instance.expeditionOneTimer;
    }

    private void SetExpeditionTwoSliderMaxValue()
    {
        slider_ExpanditionSlider.maxValue = TimeCalculation.instance.expeditionTwoTimer;
        slider_ExpanditionSlider.value = TimeCalculation.instance.expeditionTwoTimer;
    }


    public void SetExpeditionRunningData()
    {
        icon_ExpeditionRewardOne.sprite = rewardData.all_ExpeditionOneIconSprites[DataManager.instance.expeditionOneIndex];
        icon_ExpeditionRewardTwo.sprite = rewardData.all_ExpeditionOneIconSprites[DataManager.instance.expeditionOneIndex];
        rewardAmount1.text = rewardData.allExpeditionOneFirstRewardAmounts[DataManager.instance.expeditionOneIndex].ToString();
        rewardAmount2.text = rewardData.allExpeditionOneSecondRewardAmounts[DataManager.instance.expeditionOneIndex].ToString();
    }

    public void SetExpeditionTwoRunningData()
    {
        icon_ExpeditionRewardOne.sprite = rewardData.all_ExpeditionTwoIconSprites[DataManager.instance.expeditionOneIndex];
        icon_ExpeditionRewardTwo.sprite = rewardData.all_ExpeditionTwoIconSprites[DataManager.instance.expeditionOneIndex];
        rewardAmount1.text = rewardData.allExpeditionOneFirstRewardAmounts[DataManager.instance.expeditionOneIndex].ToString();
        rewardAmount2.text = rewardData.allExpeditionOneSecondRewardAmounts[DataManager.instance.expeditionOneIndex].ToString();
    }

    public void ExpeditionOneTimer(string _timeLeftInString, float _timeLeft)
    {
        slider_ExpanditionSlider.value = _timeLeft;

        txt_expanditionTimer.text = _timeLeftInString;
    }

    public void ExpeditionTwoTimer(string _timeLeftInString, float _timeLeft)
    {
        print("Called first Function");
        slider_ExpanditionSlider.value = _timeLeft;
        txt_expanditionTimer.text = _timeLeftInString;
    }




    public void OnClick_CloseExpeditionRunningScreen()
    {
        this.gameObject.SetActive(false);
    }


    #region Expedition One Finish Button

    public void OnClick_FinishExpeditionOne()
    {
        UiManager.instance.ui_Home.CompleteExpaditionOne();
        this.gameObject.SetActive(false);
        expaditionScreen.gameObject.SetActive(false);
        DataManager.instance.all_ExpeditionRunningStatus[0] = false;
        TimeCalculation.instance.ResetExpeditionOneTimer();
    }

    #endregion


    #region Expedition Two Finish Button

    public void OnClick_FinishExpeditionTwo()
    {
        UiManager.instance.ui_Home.CompleteExpeditionTwo();
        this.gameObject.SetActive(false);
        expaditionScreen.gameObject.SetActive(false);
        DataManager.instance.all_ExpeditionRunningStatus[1] = false;
        TimeCalculation.instance.ResetExpeditionTwoTimer();
    }

    #endregion
}
