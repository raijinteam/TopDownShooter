using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyRewardUI : MonoBehaviour
{

    [SerializeField] private DailyRewardSlotUI[] all_DailyRewards;
    public bool isRewardClimed;
    [SerializeField] private int index = 0;
    [SerializeField] private TextMeshProUGUI txt_Timer;

    [SerializeField] private int[] all_RewardAmount;

    private List<Sprite> sprites = new List<Sprite>();
    private List<string> strings = new List<string>();



    private void OnEnable()
    {
        SetDailyRewardData();

        index = DataManager.instance.GetDailyRewardDayIndex();
        isRewardClimed = DataManager.instance.GetDailyRewardActiveState();
        for(int i = 0; i < index; i++)
        {
            all_DailyRewards[i].panel_Climed.gameObject.SetActive(true);
            all_DailyRewards[i].isClickable = false;
        }

        if (!isRewardClimed)
        {
            all_DailyRewards[index].panel_Active.gameObject.SetActive(true);
        }
    }






    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isRewardClimed = false;
            all_DailyRewards[index].panel_Active.gameObject.SetActive(true);
            DataManager.instance.SetDailyRewardActiveState(DataManager.instance.isRewardClaim);
        }

        if (isRewardClimed)
        {
            int currentTime = (int)TimeCalculation.instance.currentDailyRewardTime;

            int hours = currentTime / 3600;
            int minutes = (currentTime % 3600) / 60;
            int remainingSeconds = currentTime % 60;

            txt_Timer.text = $"{hours} : { minutes} : { remainingSeconds}";
        }
        else
        {
            all_DailyRewards[index].isClickable = true;
            all_DailyRewards[index].panel_Active.gameObject.SetActive(true);
        }
    }


    public void SetDailyRewardData()
    {
        for(int i = 0; i < all_DailyRewards.Length; i++)
        {
            all_DailyRewards[i].txt_Header.text = "Day " + (i + 1);
            all_DailyRewards[i].txt_RewardAmount.text = all_RewardAmount[i].ToString();
        }
    }

    public void ClearList()
    {
        sprites.Clear();
        strings.Clear();
    }

    private void ResetAllDays()
    {
        for (int i = 0; i < all_DailyRewards.Length; i++)
        {
            all_DailyRewards[i].panel_Climed.gameObject.SetActive(false);
            all_DailyRewards[i].isClickable = false;
        }
        all_DailyRewards[index].isClickable = true;
        all_DailyRewards[index].panel_Active.gameObject.SetActive(true);
    }

    public void OnClick_DailyReward(int _buttonIndex)
    {
        if(_buttonIndex == index && index <= all_DailyRewards.Length && !isRewardClimed)
        {

            all_DailyRewards[index].isClickable = false;
            all_DailyRewards[index].panel_Climed.gameObject.SetActive(true);
            all_DailyRewards[index].panel_Active.gameObject.SetActive(false);
            isRewardClimed = true;
            DataManager.instance.SetDailyRewardActiveState(isRewardClimed);
            Debug.Log("Daily Reward Active State : " + DataManager.instance.GetDailyRewardActiveState());

            //Give Reward
            //Open RewardSummary Screem
            UiManager.instance.ui_Reward.ui_RewardSummary.SetRewardSummaryData(all_DailyRewards[index].img_Icon.sprite , all_RewardAmount[index].ToString());
            UiManager.instance.ui_Reward.gameObject.SetActive(true);

            if (index >= all_DailyRewards.Length - 1)
            {
                Debug.Log("Rewach 7 day");
                index = 0;
                ResetAllDays();
            }
            else
            {
                index++;
            }
            DataManager.instance.SetDailyRewardDayIndex(index);
            all_DailyRewards[index].isClickable = true;
        }
    }

   
    public void OnClick_Close()
    {
        gameObject.SetActive(false);
    }
   
}
