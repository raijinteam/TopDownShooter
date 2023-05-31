using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpeditionPanelUI : MonoBehaviour
{
    [Header("Expedition Data")]
    public int myPanelId;


    [Header("Require Scripts")]
    [SerializeField] private ExpaditionReward rewardData;



    [SerializeField] private GameObject go_ExpeditionPanel;
    [SerializeField] private ExpeditionRunningUI expeditionRunningPanel;
    [SerializeField] private ExpaditionRewardData[] all_ExpeditionRewardData;


    public bool isExpeditionOneCompelete;
    public bool isExpeditionTwoCompelete;




    private void OnEnable()
    {
        SetExpeditionOneData();
    }


    private void SetExpeditionOneData()
    {

        for (int i = 0; i < all_ExpeditionRewardData.Length; i++)
        {
            TextMeshProUGUI rewardText1 = all_ExpeditionRewardData[i].rewardText1;
            TextMeshProUGUI rewardText2 = all_ExpeditionRewardData[i].rewardText2;

            rewardText1.text = (rewardData.allExpeditionOneFirstRewardAmounts[i]).ToString();
            rewardText2.text = (rewardData.allExpeditionOneSecondRewardAmounts[i]).ToString();
        }

        for (int i = 0; i < all_ExpeditionRewardData.Length; i++)
        {
            TextMeshProUGUI rewardText1 = all_ExpeditionRewardData[i].rewardText1;
            TextMeshProUGUI rewardText2 = all_ExpeditionRewardData[i].rewardText2;

            rewardText1.text = (rewardData.allExpeditionTwoFirstRewardAmounts[i]).ToString();
            rewardText2.text = (rewardData.allExpeditionTwoSecondRewardAmounts[i]).ToString();
        }
    }



    public void OnClick_ExpeditionOneActive(int _Index)
    {
        if(myPanelId == 0)
        {
            TimeCalculation.instance.currentExpeditionOneTimer = TimeCalculation.instance.expeditionOneTimer;

            UiManager.instance.ui_Home.expeditionOneState = ExpeditionOneState.running;
            UiManager.instance.ui_Home.CheckExpeditionOneState();
            DataManager.instance.all_ExpeditionRunningStatus[0] = true;
            DataManager.instance.expeditionOneIndex = _Index;
            this.gameObject.SetActive(false);
            expeditionRunningPanel.gameObject.SetActive(true);

            print("Expediton one index " + DataManager.instance.expeditionOneIndex);
            expeditionRunningPanel.SetExpeditionRunningData();
        }
        else if(myPanelId == 1) { 

            TimeCalculation.instance.currentExpeditionTwoTimer = TimeCalculation.instance.expeditionTwoTimer;
            UiManager.instance.ui_Home.expeditionTwoState = ExpeditionTwoState.running;
            UiManager.instance.ui_Home.CheckExpeditionTwoState();
            DataManager.instance.all_ExpeditionRunningStatus[1] = true;
            DataManager.instance.expeditionTwoIndex = _Index;
            this.gameObject.SetActive(false);
            expeditionRunningPanel.gameObject.SetActive(true);


            expeditionRunningPanel.SetExpeditionTwoRunningData();
        }
    }


    public void OnClick_CloseExpeditionScreen()
    {
        this.gameObject.SetActive(false);
    }

}
