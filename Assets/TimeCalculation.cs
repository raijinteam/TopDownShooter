using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCalculation : MonoBehaviour
{
    public static TimeCalculation instance;

    [Header("Expedition One")]

    public float expeditionOneTimer = 30f;

    public float currentExpeditionOneTimer;

    [Header("Expedition Two")]
    public float expeditionTwoTimer = 30f;

    public float currentExpeditionTwoTimer;



    private void Awake()
    {
        if (instance != this || instance == null)
        {
            instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (DataManager.instance.all_ExpeditionRunningStatus[0])
        {
            CalcuateExpeditionOneTime();
        }

        if (DataManager.instance.all_ExpeditionRunningStatus[1])
        {
            CalcuateExpeditionTwoTime();
        }
    }

    private void CalcuateExpeditionOneTime()
    {
        currentExpeditionOneTimer -= Time.deltaTime;

        if(currentExpeditionOneTimer <= 0)
        {
            currentExpeditionOneTimer = expeditionOneTimer;
            UiManager.instance.ui_Home.CompleteExpaditionOne();

        }
        UiManager.instance.ui_Home.ExpeditionOneTimer(currentExpeditionOneTimer);
    }
    public void ResetExpeditionOneTimer()
    {
        currentExpeditionOneTimer = expeditionOneTimer;
    }





    private void CalcuateExpeditionTwoTime()
    {
        currentExpeditionTwoTimer -= Time.deltaTime;

        if (currentExpeditionTwoTimer <= 0)
        {
            currentExpeditionTwoTimer = expeditionOneTimer;
            UiManager.instance.ui_Home.CompleteExpeditionTwo();

        }
        UiManager.instance.ui_Home.ExpeditionTwoTimer(currentExpeditionTwoTimer);
    }
    
    public void ResetExpeditionTwoTimer()
    {
        currentExpeditionTwoTimer = expeditionTwoTimer;
    }

}
