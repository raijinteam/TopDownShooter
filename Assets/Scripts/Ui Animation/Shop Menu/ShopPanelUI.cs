using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShopPanelUI : MonoBehaviour
{
    [SerializeField] private RectTransform rt_ScrollContent;
    [SerializeField] private float flt_AnimationDuration = 1f;
    [Header("Chest")]
    public ChestRewardData[] all_ChestRewardData;
    public ChestUI[] all_ChestUI;
    public int[] all_ChestPrice;
    [Header("Skipits")]
    public SlotCurrencySlotUI[] all_Skipits;
    public int[] all_Skipits_Price;
    public int[] all_Skipits_Amount;
    [Header("Gold")]
    public SlotCurrencySlotUI[] all_Coins;
    public int[] all_Coins_Price;
    public int[] all_Coins_Amount;
    [Header("Gems")]
    public SlotCurrencySlotUI[] all_Gems;
    public int[] all_Gems_Price;
    public int[] all_Gems_Amount;



    private void OnEnable()
    {
        SetAllChestPrice();
        SetSkipits();
        SetGolds();
        SetGems();
    }

    private void OnDisable()
    {
        rt_ScrollContent.DOAnchorPosY(0, 0.01f);
    }

    public void SetScrollContent(float value)
    {
        rt_ScrollContent.DOAnchorPosY(value, flt_AnimationDuration);
    }

    private void SetAllChestPrice()
    {
        for (int i = 0; i < all_ChestPrice.Length; i++)
        {
            all_ChestUI[i].SetCheastPrice(all_ChestPrice[i]);
        }
    }


    public void SetSkipits()
    {
        for (int i = 0; i < all_Skipits.Length; i++)
        {
            all_Skipits[i].SetData(all_Skipits_Amount[i], all_Skipits_Price[i]);
        }
    }

    public void SetGolds()
    {
        for (int i = 0; i < all_Coins.Length; i++)
        {
            all_Coins[i].SetData(all_Coins_Amount[i], all_Coins_Price[i]);
        }
    }

    public void SetGems()
    {
        for (int i = 0; i < all_Gems.Length; i++)
        {
            all_Gems[i].SetData(all_Gems_Amount[i], all_Gems_Price[i]);
        }
    }

    public void OnClick_OpenChestInfo(int index)
    {
        if (all_ChestUI[index].GetRewardInfoPanel().activeInHierarchy)
        {
            all_ChestUI[index].GetRewardInfoPanel().SetActive(false);
        }
        else
        {
            all_ChestUI[index].GetRewardInfoPanel().SetActive(true);
        }
    }




    public void OnClick_BuyStaterPack(int index)
    {
        int rawadAmount = RewardManager.Instance.all_StaterPacks[index].all_RewardIcons.Length;
        List<Sprite> sprites = new List<Sprite>();
        List<int> rewardAmount = new List<int>();
        for (int i = 0; i < rawadAmount; i++)
        {
            sprites.Add(RewardManager.Instance.all_StaterPacks[index].all_RewardIcons[i]);
            rewardAmount.Add(RewardManager.Instance.all_StaterPacks[index].all_RewardsAmount[i]);
        }
        UiManager.instance.ui_Reward.ui_StaterPack.SetStaterPackData(sprites, rewardAmount);
        UiManager.instance.ui_Reward.gameObject.SetActive(true);
    }

    public void OnClick_BuyChest(int index)
    {
        int numberOfRewards = RewardManager.Instance.all_ChestData[index].all_ChestData.Length;

        for (int i = 0; i < numberOfRewards; i++)
        {
            Sprite sprite = RewardManager.Instance.all_ChestData[index].all_ChestData[i].rewardIcon;
            int rewardAmount = RewardManager.Instance.all_ChestData[index].all_ChestData[i].rewardAmount;

            List<Sprite> sprites = new List<Sprite>();
            sprites.Add(RewardManager.Instance.all_ChestData[index].all_ChestData[i].rewardIcon);
            List<int> rewards = new List<int>();
            rewards.Add(RewardManager.Instance.all_ChestData[index].all_ChestData[i].rewardAmount);
            UiManager.instance.ui_Reward.ui_ChestReward.SetChestRewardData(sprites, rewards);
        }
        UiManager.instance.ui_Reward.gameObject.SetActive(true);
        UiManager.instance.ui_Reward.ui_ChestReward.canShowRewardSummaryUI = true;
    }

    public void OnClick_BuySkipits(int index)
    {
        if (!DataManager.instance.HasEnoughGemsForUse(all_Skipits_Price[index]))
        {
            //Show Not Enough Gems panel
            Debug.Log("Not enough Gems");
            return;
        }


        Sprite sprite = all_Skipits[index].img_Reward.sprite;
        int rewardAmount = all_Skipits_Amount[index];
        DataManager.instance.IncreaseSkipIts(rewardAmount);
        DataManager.instance.DecreaseGems(all_Skipits_Price[index]);
        UiManager.instance.ui_Reward.ui_itemReward.SetRewardData(sprite, rewardAmount);
        UiManager.instance.ui_Reward.gameObject.SetActive(true);
    }


    public void OnClick_BuyGold(int index)
    {

        if (!DataManager.instance.HasEnoughGemsForUse(all_Coins_Price[index]))
        {
            //Show Not Enough Gems panel
            Debug.Log("Not enough Gems");
            return;
        }

        Sprite sprite = all_Coins[index].img_Reward.sprite;
        int rewardAmount = all_Coins_Amount[index];
        DataManager.instance.IncreaseCoins(rewardAmount);
        DataManager.instance.DecreaseGems(all_Coins_Price[index]);
        UiManager.instance.ui_Reward.ui_itemReward.SetRewardData(sprite, rewardAmount);
        UiManager.instance.ui_Reward.gameObject.SetActive(true);
    }

    public void OnClick_BuyGems(int index)
    {
        Sprite sprite = all_Gems[index].img_Reward.sprite;
        int rewardAmount = all_Gems_Amount[index];
        DataManager.instance.IncreaseGems(rewardAmount);
        UiManager.instance.ui_Reward.ui_itemReward.SetRewardData(sprite, rewardAmount);
        UiManager.instance.ui_Reward.gameObject.SetActive(true);
    }

}
