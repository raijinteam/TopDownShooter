using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Header;
    [SerializeField] private TextMeshProUGUI txt_Price;
    [SerializeField] private RectTransform panel_Info;


    public void SetCheastPrice(int _amount)
    {
        txt_Price.text = _amount.ToString();
    }

    public GameObject GetRewardInfoPanel()
    {
        return panel_Info.gameObject;
    }
}
