using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Expediton2ItemUI : MonoBehaviour
{


    public int[] all_RewardAmounts;
    public float time;
    [SerializeField] private TextMeshProUGUI txt_Time;
    [SerializeField] private TextMeshProUGUI[] all_RewardText;



    private void OnEnable()
    {
        txt_Time.text = time.ToString();
        for(int i = 0; i < all_RewardAmounts.Length; i++)
        {
            all_RewardText[i].text = all_RewardAmounts[i].ToString();
        }
    }
}
