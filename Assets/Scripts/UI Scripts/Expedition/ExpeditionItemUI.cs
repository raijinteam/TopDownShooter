using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpeditionItemUI : MonoBehaviour
{
    public float time;
    public float reward;

    [SerializeField] private Image img_icon;
    [SerializeField] private TextMeshProUGUI txt_Time;
    [SerializeField] private TextMeshProUGUI txt_Reward;


    private void OnEnable()
    {
        txt_Time.text = time.ToString();
        txt_Reward.text = reward.ToString();
    }

}
