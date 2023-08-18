using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeCalculation : MonoBehaviour
{
    public static TimeCalculation instance;

    public float activeGameTime;

    [Header("Daily Reward")]
    public float dailyRewardTimeInSeconds;
    public float currentDailyRewardTime;

    [Header("Daily Mission")]
    public float dailyMissionTimeInSecond;
    public float currentDailyMissionTime;

    [Header("Expedition")]
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

    public void SetDailyRewardTime()
    {
        CalculateDailyRewardScreenOffTime();
        currentDailyRewardTime = DataManager.instance.GetDailyRewardTime();
    }

    public void SetExpeditionOneTimer(int index )
    {
        CalculateExpeditionScreenOffTime(index);
        currentExpeditionTimer[index] = DataManager.instance.GetExpeditionTime(index);
    }

    public void SetDailyMissionTimeData()
    {
        //Debug.Log("Daily misison first called");
        CalculateTimeForDailyMission();
        Debug.Log("Daily Mission Time : " + DataManager.instance.GetDailyMissionTime());
        currentDailyMissionTime = DataManager.instance.GetDailyMissionTime() ;
    }

    private void Update()
    {
        activeGameTime += Time.deltaTime;


        CalculateDailyRewardOnScreenTime();
        CalculateDailyMissionTimeWhenGameIsRunning();
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


        if (UiManager.instance.ui_DailyReward.isRewardClimed)
        {
            float quitDailyRewardTime = currentDailyRewardTime - activeGameTime;
            Debug.Log("Daily reward Quit time : " + quitDailyRewardTime);
            DataManager.instance.SetDailyRewardTime(quitDailyRewardTime);
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

    #region Daily Reward Time Cal


    private void CalculateDailyRewardScreenOffTime()
    {
        CalculateScreenOffTime();

        float dayTime = (int)DataManager.instance.GetDailyRewardTime();
        if (DataManager.instance.GetDailyRewardActiveState())
        {
            currentDailyRewardTime = dayTime - (float)timeSpan.TotalSeconds;
            Debug.Log("Daily Reward Screen off TIme : " + currentDailyRewardTime);
            if(currentDailyRewardTime <= 0)
            {
                DataManager.instance.SetDailyRewardActiveState(false);
                UiManager.instance.ui_DailyReward.isRewardClimed = false;
                currentDailyRewardTime = dailyRewardTimeInSeconds;
                DataManager.instance.SetDailyRewardTime(currentDailyRewardTime);
            }
        }
    }

    private void CalculateDailyRewardOnScreenTime()
    {
        if (DataManager.instance.GetDailyRewardActiveState())
        {
            currentDailyRewardTime -= Time.deltaTime;
            if(currentDailyRewardTime <= 0)
            {
                DataManager.instance.SetDailyRewardActiveState(false);
                UiManager.instance.ui_DailyReward.isRewardClimed = false;
                currentDailyRewardTime = dailyRewardTimeInSeconds;
                DataManager.instance.SetDailyRewardTime(currentDailyRewardTime);
            }
        }
    }

    #endregion



    #region Daily Misison Time cal


    public void CalculateTimeForDailyMission()
    {
        CalculateScreenOffTime();
        // Debug.Log("Daily mission time : " + (int)DataManager.Instance.GetDailyMissionTime());
        int dayTime = (int)DataManager.instance.GetDailyMissionTime();

        //  Debug.Log("Total Day time is : " + (dayTime - timeSpan.TotalSeconds));

        currentDailyMissionTime = dayTime - (int)timeSpan.TotalSeconds;

        if (currentDailyMissionTime <= 0)
        {
            // Debug.Log("Set Reward Time");
            DataManager.instance.SetDailyMissionTime(0);
            DataManager.instance.SetDailyMissionOnedayComplete(true);
            UiManager.instance.ui_DailyMission.ResetAllListData();
            UiManager.instance.ui_DailyMission.SpawnDailyMissions();
            Debug.Log("In Time manager 1");
            currentDailyMissionTime = (int)dailyMissionTimeInSecond;
        }
    }


    private void CalculateDailyMissionTimeWhenGameIsRunning()
    {

        currentDailyMissionTime -= Time.deltaTime;
        if (currentDailyMissionTime <= 0)
        {
            DataManager.instance.SetDailyMissionTime(0);
            //    Debug.Log("Set Reward Time");
            DataManager.instance.SetDailyMissionOnedayComplete(true);
            Debug.Log("In Time manager 2");
            UiManager.instance.ui_DailyMission.ResetAllListData();
            UiManager.instance.ui_DailyMission.SpawnDailyMissions();
            currentDailyMissionTime = dailyMissionTimeInSecond;
        }

    }


    #endregion


    #region Expedition Time Cal

    private void CalculateExpeditionScreenOffTime(int index)
    {
        CalculateScreenOffTime();

        float dayTime = (int)DataManager.instance.GetExpeditionTime(index);

        if (UiManager.instance.ui_Home.ui_Expedition[index].expeditionState == ExpeditionState.running)
        {
            currentExpeditionTimer[index] = dayTime - (float)timeSpan.TotalSeconds;
            Debug.Log("Daily Challange Screen of time : " + currentExpeditionTimer[index]);
            if (currentExpeditionTimer[index] <= 0)
            {
                DataManager.instance.SetExpeditionState(index, ExpeditionState.finished);
                currentExpeditionTimer[index] = expeditionTimer[index];
                DataManager.instance.SetExpeditionTimer(index, expeditionTimer[index]);
            }
        }
    }

    private void CalcuateExpeditionOneTime(int index)
    {
        if (UiManager.instance.ui_Home.ui_Expedition[index].expeditionState == ExpeditionState.running)
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
    #endregion






}
