using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeCalculation : MonoBehaviour
{
    public static TimeCalculation instance;

    public float activeGameTime;
    public float[] expeditionTimer;
    public float[] currentExpeditionTimer;

    private TimeSpan timeSpan;

    private void Awake()
    {
        if (instance != this || instance == null)
        {
            instance = this;
        }
    }


    public void SetExpeditionOneTimer(int index )
    {
        CalculateExpeditionScreenOffTime(index);
        currentExpeditionTimer[index] = DataManager.instance.GetExpeditionTime(index);
    }


    private void Update()
    {
        activeGameTime += Time.deltaTime;


        CalcuateExpeditionOneTime(0);
        CalcuateExpeditionOneTime(1);
    }


    private void OnDestroy()
    {
        for(int i = 0; i < 2; i++)
        {
            if(UiManager.instance.ui_Home.ui_Expedition[i].expeditionState == ExpeditionState.running)
            {
                float quitExpeditionTime = currentExpeditionTimer[i] - activeGameTime;
                DataManager.instance.SetExpeditionTimer(i, quitExpeditionTime);
            }
        }

        DateTime gameQuitTime = DateTime.Now;
        DataManager.instance.SetGameQuitTime(gameQuitTime.ToString());
    }


    public void CalculateScreenOffTime()
    {
        string gameQuitTimeString = DataManager.instance.GetGameQuitTime();


        if (!gameQuitTimeString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(gameQuitTimeString);
            DateTime currentTime = DateTime.Now;

            if (currentTime > quitTime)
            {
                timeSpan = currentTime - quitTime;
                Debug.Log("Total Quit Seconds : " + (float)timeSpan.TotalSeconds);
            }
        }
    }


    private void CalculateExpeditionScreenOffTime(int index)
    {
        CalculateScreenOffTime();

        float dayTime = (int)DataManager.instance.GetExpeditionTime(index);

        if(UiManager.instance.ui_Home.ui_Expedition[index].expeditionState == ExpeditionState.running)
        {
            currentExpeditionTimer[index] = dayTime - (float)timeSpan.TotalSeconds;
            Debug.Log("Daily Challange Screen of time : " + currentExpeditionTimer[index]);
            if(currentExpeditionTimer[index] <= 0)
            {
                DataManager.instance.SetExpeditionState(index, ExpeditionState.finished);
                currentExpeditionTimer[index] = expeditionTimer[index];
                DataManager.instance.SetExpeditionTimer(index, expeditionTimer[index]);
            }
        }
    }

    private void CalcuateExpeditionOneTime(int index)
    {
        if(UiManager.instance.ui_Home.ui_Expedition[index].expeditionState == ExpeditionState.running)
        {
            currentExpeditionTimer[index] -= Time.deltaTime;

            if (currentExpeditionTimer[index] <= 0)
            {
                DataManager.instance.SetExpeditionState(index, ExpeditionState.finished);
                UiManager.instance.ui_Home.ui_Expedition[index].expeditionState = ExpeditionState.finished;
                UiManager.instance.ui_Home.ui_Expedition[index].SetExpeditionPanels();
                currentExpeditionTimer[index] = expeditionTimer[index];
            }
        }
    }




}
