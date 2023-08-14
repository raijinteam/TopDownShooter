using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpeditionProcressUI : MonoBehaviour
{
    public int index;
    public int expeditionStartIndex;
    [SerializeField] private Image img_Icon;
    [SerializeField] private TextMeshProUGUI txt_Reward;
    [SerializeField] private TextMeshProUGUI txt_Timer;
    [SerializeField] private Slider slider_Timer;

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        SetData();
    }
    private void SetData()
    {

        int timer = (int)TimeCalculation.instance.currentExpeditionTimer[index];
        slider_Timer.maxValue = TimeCalculation.instance.expeditionTimer[index];
        slider_Timer.value = TimeCalculation.instance.currentExpeditionTimer[index];

        float hours = timer / 3600;
        float minutes = (timer % 3600) / 60;
        float seconds = timer % 60;

        txt_Timer.text = $"{hours} : {minutes} : {seconds}";

    }




    public void OnClick_SkipWithAd()
    {

    }

    public void OnClick_SkipWithGems()
    {

    }
}
