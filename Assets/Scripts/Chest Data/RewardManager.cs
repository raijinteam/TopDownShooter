using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    private void Awake()
    {
        if(Instance != this)
        {
            Instance = this;
        }
    }

    [Header("Chest Reward")]
    public ChestData[] all_ChestData;

    [Header("Stater Pack Reward")]
    public StaterPackData[] all_StaterPacks;

    [Header("Level Reward")]
    public int levelIndex = 0;

    public Sprite sprite_Coins;
    public Sprite sprite_Gems;
    public Sprite sprite_Energy;
    public Sprite sprite_Skipits;
    public Sprite sprite_Chest;


    public List<LevelUpRewards> list_LevelUpReward = new List<LevelUpRewards>();
    public List<Sprite> list_RewardIcons = new List<Sprite>();
    public List<int> list_RewardAmount = new List<int>();


    public void GiveRewardLevelUP(int level)
    {
        list_LevelUpReward.Clear();
        list_RewardIcons.Clear();
        list_LevelUpReward.Clear();
        if (level % 5 == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                list_LevelUpReward.Add(GetRandomReward());
            }
            list_LevelUpReward.Add(GiveChest());
        }
        else
        {
            list_LevelUpReward.Add(GetRandomReward());
        }

        for (int i = 0; i < list_LevelUpReward.Count; i++)
        {
            if (list_LevelUpReward[i].rewardType == RewardType.coins)
            {
                list_RewardIcons.Add(sprite_Coins);
                list_RewardAmount.Add(list_LevelUpReward[i].rewardAmount);
                DataManager.instance.IncreaseCoins(list_RewardAmount[i]);
                Debug.Log("Give Reward Coins");
            }
            if (list_LevelUpReward[i].rewardType == RewardType.gems)
            {
                list_RewardIcons.Add(sprite_Gems);
                list_RewardAmount.Add(list_LevelUpReward[i].rewardAmount);
                DataManager.instance.IncreaseGems(list_RewardAmount[i]);
                Debug.Log("Give Reward Gems");
            }
            if (list_LevelUpReward[i].rewardType == RewardType.energy)
            {
                list_RewardIcons.Add(sprite_Energy);
                list_RewardAmount.Add(list_LevelUpReward[i].rewardAmount);
                DataManager.instance.IncreaseEnergy(list_RewardAmount[i]);
                Debug.Log("Give Reward Energy");
            }
            if (list_LevelUpReward[i].rewardType == RewardType.skipits)
            {
                list_RewardIcons.Add(sprite_Skipits);
                list_RewardAmount.Add(list_LevelUpReward[i].rewardAmount);
                DataManager.instance.IncreaseSkipIts(list_RewardAmount[i]);
                Debug.Log("Give Reward Skipits");
            }
            if (list_LevelUpReward[i].rewardType == RewardType.chest)
            {
                list_RewardIcons.Add(sprite_Chest);
                list_RewardAmount.Add(list_LevelUpReward[i].chestIndex);
                Debug.Log("Give Chest");
            }
        }

        List<string> strings = new List<string>();

        for (int i = 0; i < list_RewardAmount.Count; i++)
        {
            strings.Add(list_RewardAmount[i].ToString());
        }

        UiManager.instance.ui_Reward.ui_RewardSummary.SetMultiplRewardSummaryData(list_RewardIcons, strings);
        UiManager.instance.ui_Reward.gameObject.SetActive(true);
        UiManager.instance.ui_Reward.ui_RewardSummary.gameObject.SetActive(true);

    }


    public LevelUpRewards GetRandomReward()
    {
        int randomIndex = Random.Range(0, 4);

        RewardType randomRewardType = (RewardType)randomIndex;

        return new LevelUpRewards { rewardType = randomRewardType, rewardAmount = 10 };
    }

    public LevelUpRewards GiveChest()
    {
        return new LevelUpRewards { rewardType = RewardType.chest, chestIndex = 0 };
    }

    public LevelUpRewards GiveDailyReward(RewardType _rewardType , int _rewardAmount)
    {
        return new LevelUpRewards { rewardType = _rewardType, rewardAmount = _rewardAmount };
    }

}
