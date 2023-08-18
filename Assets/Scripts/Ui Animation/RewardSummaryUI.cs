using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSummaryUI : MonoBehaviour
{

    [SerializeField] private Image[] allRewardIcons;
    [SerializeField] private TextMeshProUGUI rewardAmountText;

    //IF THERE IS ONLY ONE REWARD CALL THAT FUNCTION
    public void SetRewardSummaryData(Sprite _rewardIcon , string _rewardAmount)
    {
        this.gameObject.SetActive(true);
        allRewardIcons[0].gameObject.SetActive(true);
        allRewardIcons[0].transform.GetChild(0).GetComponent<Image>().sprite = _rewardIcon;
        allRewardIcons[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _rewardAmount.ToString();
    }


    //IF THERE IS MULTIPLE REWARD CALL THAT FUNCTION
    public void SetMultiplRewardSummaryData(List<Sprite> allRewardSprite , List<string> allRewardAmount)
    {
        this.gameObject.SetActive(true);
        Debug.Log("List count : " + allRewardSprite.Count);
        for(int i =0; i < allRewardSprite.Count; i++)
        {
            Debug.Log("Set Data in reward summary ui" + i);
            allRewardIcons[i].gameObject.SetActive(true);

            allRewardIcons[i].transform.GetChild(0).GetComponent<Image>().sprite = allRewardSprite[i];
            allRewardIcons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allRewardAmount[i];
        }
    }


    #region Reward Claim Button

    public void OnClick_ClaimRewardButton()
    {
        for (int i = 0; i < allRewardIcons.Length; i++)
        {
            allRewardIcons[i].gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    #endregion

}
