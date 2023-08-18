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

    [Header("Only Expedition One")]
    [SerializeField] private TextMeshProUGUI txt_Reward;


    [Header("Only Expedition Two")]
    [SerializeField] private TextMeshProUGUI[] all_RewardText;

    private void OnEnable()
    {
        if(index == 0)
        {
            expeditionStartIndex = UiManager.instance.ui_ExpeditionPanel[index].expeditionStateIndex;
            SetExpeditionOneData();
        }
        else
        {
            expeditionStartIndex = UiManager.instance.ui_ExpeditionPanel[index].expeditionStateIndex;
            SetExpeditionTwoData();
        }
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

    private void SetExpeditionOneData()
    {
        txt_Reward.text =  UiManager.instance.ui_ExpeditionPanel[index].all_ExpeditionItem[expeditionStartIndex].reward.ToString();
    }

    private void SetExpeditionTwoData()
    {
        for(int i = 0; i < 3; i++)
        {
            all_RewardText[i].text = UiManager.instance.ui_ExpeditionPanel[index].all_ExpeditionItems[expeditionStartIndex].all_RewardAmounts[i].ToString();
        }
    }


    public void OnClick_SkipWithAd()
    {

    }

    public void OnClick_SkipWithGems()
    {

    }
}
