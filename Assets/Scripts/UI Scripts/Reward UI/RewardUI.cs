using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardUI : MonoBehaviour
{

    public ItemRewardUI ui_itemReward;
    public ChestRewardUI ui_ChestReward;
    public RewardSummaryUI ui_RewardSummary;
    public StaterPackUI ui_StaterPack;

    
   

    private int index = 0;

    private bool isTouch;

   

    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }


}
