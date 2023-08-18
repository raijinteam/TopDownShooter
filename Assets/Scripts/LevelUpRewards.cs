using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum RewardType
{
    coins,
    gems,
    energy,
    skipits,
    chest
}

[System.Serializable]
public class LevelUpRewards
{
    public RewardType rewardType;
    public int rewardAmount;
    public int chestIndex;
}
