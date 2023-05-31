using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpaditionReward : MonoBehaviour
{
    [Header("Expedition One Data")]
    public Sprite[] all_ExpeditionOneIconSprites;
    public int[] allExpeditionOneFirstRewardAmounts;
    public int[] allExpeditionOneSecondRewardAmounts;

    [Header("Expedition Two Data")]
    public Sprite[] all_ExpeditionTwoIconSprites;
    public int[] allExpeditionTwoFirstRewardAmounts;
    public int[] allExpeditionTwoSecondRewardAmounts;


    private int levelMultiplier = 100;

}
