using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ExpeditionOneState
{
    start,
    running,
    claim
}


public enum ExpeditionTwoState
{
    start,
    running,
    claim
}


public class HomeUI : MonoBehaviour
{

    [SerializeField] private Camera homeCamera;

    [Header("Settings")]
    [SerializeField] private GameObject go_SettingPenal;

    [Header("Daily Mission")]
    [SerializeField] private GameObject go_DailyMissionPanel;

    [Header("Daily Login")]
    public GameObject go_DailyRewardPanel;

    [Header("Expedition One")]
    public ExpeditionPanelUI expaditionOnePanel;
    [SerializeField] private ExpeditionRunningUI expeditionOneRunningPanel;

    [Header("Expedition Two")]
    public ExpeditionPanelUI expeditionTwoPanel;
    [SerializeField] private ExpeditionRunningUI expeditionTwoRunningPanel;


    [Header ("Home Data")]
    [SerializeField] private TextMeshProUGUI txt_ExpeditionOneRunningButton;
    [SerializeField] private TextMeshProUGUI txt_ExpeditionTwoRunningButton;


    public ExpeditionOneState expeditionOneState;
    public ExpeditionTwoState expeditionTwoState;


    [Header("Expedition One Buttons")]
    [SerializeField] private Button btn_ExpeditionOneStart;
    [SerializeField] private Button btn_ExpeditionOneRunning;
    [SerializeField] private Button btn_ExpeditioOneClaim;
    private string str_ExpeditionOneTimeLeft;

    [Header("Expedition One Buttons")]
    [SerializeField] private Button btn_ExpeditionTwoStart;
    [SerializeField] private Button btn_ExpeditionTwoRunning;
    [SerializeField] private Button btn_ExpeditioTwoClaim;
    private string str_ExpeditionTwoTimeLeft;

    private void OnEnable()
    {
        CheckExpeditionOneState();
        CheckExpeditionTwoState();
        homeCamera.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        homeCamera.gameObject.SetActive(false);
    }

    public void ExpeditionOneTimer(float _timeLeft)
    {
        float hours = _timeLeft / 3600;
        float minutes = (_timeLeft % 3600) / 60;
        float seconds = _timeLeft % 60;

        // timer code
        if (_timeLeft < 60)
        {
            str_ExpeditionOneTimeLeft = seconds.ToString("F0") + "S";
           // txt_ExpeditionRunning.text = str_ExpeditionTimeLeft;
        }
        else if (_timeLeft < 3600)
        {
            str_ExpeditionOneTimeLeft = minutes.ToString("F0") + "M :" + seconds.ToString("F0") + "S";
            //txt_ExpeditionRunning.text = str_ExpeditionTimeLeft;
        }
        else if (_timeLeft > 3600)
        {
            str_ExpeditionOneTimeLeft = minutes.ToString("F0") + "M :" + seconds.ToString("F0") + "S";
            //txt_ExpeditionRunning.text = str_ExpeditionTimeLeft;
        }

        txt_ExpeditionOneRunningButton.text = str_ExpeditionOneTimeLeft;

        // expeditionRunningUI.ExpeditionOneTimer();
        expeditionOneRunningPanel.ExpeditionOneTimer(str_ExpeditionOneTimeLeft, _timeLeft);
    }

    public void CompleteExpaditionOne()
    {
        expeditionOneState = ExpeditionOneState.claim;
        CheckExpeditionOneState();
        btn_ExpeditionOneRunning.gameObject.SetActive(false);
        expeditionOneRunningPanel.gameObject.SetActive(false);
        DataManager.instance.all_ExpeditionRunningStatus[0] = false;
        expaditionOnePanel.isExpeditionOneCompelete = true;
    }
    public void CheckExpeditionOneState()
    {

        if (expeditionOneState == ExpeditionOneState.start)
        {
            btn_ExpeditionOneStart.gameObject.SetActive(true);
            btn_ExpeditionOneRunning.gameObject.SetActive(false);
            btn_ExpeditioOneClaim.gameObject.SetActive(false);
        }
        if (expeditionOneState == ExpeditionOneState.running)
        {
            btn_ExpeditionOneStart.gameObject.SetActive(false);
            btn_ExpeditionOneRunning.gameObject.SetActive(true);
            btn_ExpeditioOneClaim.gameObject.SetActive(false);
        }

        if (expeditionOneState == ExpeditionOneState.claim)
        {
            btn_ExpeditionOneStart.gameObject.SetActive(false);
            btn_ExpeditionOneRunning.gameObject.SetActive(false);
            btn_ExpeditioOneClaim.gameObject.SetActive(true);
        }
    }



    public void ExpeditionTwoTimer(float _timeLeft)
    {
        float hours = _timeLeft / 3600;
        float minutes = (_timeLeft % 3600) / 60;
        float seconds = _timeLeft % 60;

        // timer code
        if (_timeLeft < 60)
        {
            str_ExpeditionTwoTimeLeft = seconds.ToString("F0") + "S";
        }
        else if (_timeLeft < 3600)
        {
            str_ExpeditionTwoTimeLeft = minutes.ToString("F0") + "M :" + seconds.ToString("F0") + "S";
        }
        else if (_timeLeft > 3600)
        {
            str_ExpeditionTwoTimeLeft = minutes.ToString("F0") + "M :" + seconds.ToString("F0") + "S";
        }

        txt_ExpeditionTwoRunningButton.text = str_ExpeditionTwoTimeLeft;

        expeditionTwoRunningPanel.ExpeditionTwoTimer(str_ExpeditionTwoTimeLeft, _timeLeft);
    }
    

    public void CheckExpeditionTwoState()
    {
        if (expeditionTwoState == ExpeditionTwoState.start)
        {
            btn_ExpeditionTwoStart.gameObject.SetActive(true);
            btn_ExpeditionTwoRunning.gameObject.SetActive(false);
            btn_ExpeditioTwoClaim.gameObject.SetActive(false);
        }
        if (expeditionTwoState == ExpeditionTwoState.running)
        {
            btn_ExpeditionTwoStart.gameObject.SetActive(false);
            btn_ExpeditionTwoRunning.gameObject.SetActive(true);
            btn_ExpeditioTwoClaim.gameObject.SetActive(false);
        }

        if (expeditionTwoState == ExpeditionTwoState.claim)
        {
            btn_ExpeditionTwoStart.gameObject.SetActive(false);
            btn_ExpeditionTwoRunning.gameObject.SetActive(false);
            btn_ExpeditioTwoClaim.gameObject.SetActive(true);
        }
    }
    public void CompleteExpeditionTwo()
    {
        expeditionTwoState = ExpeditionTwoState.claim;
        CheckExpeditionTwoState();
        btn_ExpeditionTwoRunning.gameObject.SetActive(false);
        expeditionTwoRunningPanel.gameObject.SetActive(false);
        DataManager.instance.all_ExpeditionRunningStatus[1] = false;
        expeditionTwoPanel.isExpeditionTwoCompelete = true;
    }




    #region All Expedition Click Buttons


    #region Expedition One Buttons
    public void OnClick_ExpaditionOnePanelOpen()
    {
        TimeCalculation.instance.currentExpeditionOneTimer = TimeCalculation.instance.expeditionOneTimer;
        expaditionOnePanel.gameObject.SetActive(true);
    }

    public void OnClick_ExpeditionOneRunningScreenOpen()
    {
        expeditionOneRunningPanel.gameObject.SetActive(true);
    }


    public void OnClick_ExpeditionOneClaim()
    {
        expeditionOneState = ExpeditionOneState.start;
        CheckExpeditionOneState();
        TimeCalculation.instance.ResetExpeditionOneTimer();
    }

    #endregion

    #region Expedition Two Buttons

    public void OnClick_ExpaditionTwoPanelOpen()
    {
        TimeCalculation.instance.currentExpeditionTwoTimer = TimeCalculation.instance.expeditionTwoTimer;
        expeditionTwoPanel.gameObject.SetActive(true);
    }

    public void OnClick_ExpeditionTwoRunningScreenOpen()
    {
        expeditionTwoRunningPanel.gameObject.SetActive(true);
    }


    public void OnClick_ExpeditionTwoClaim()
    {
        expeditionTwoState = ExpeditionTwoState.start;
        CheckExpeditionTwoState();
        TimeCalculation.instance.ResetExpeditionTwoTimer();
    }


    #endregion


    #endregion


    #region Game Start Button

    public void OnClick_StartGame()
    {
        print("Game Start");
    }

    #endregion


    #region Setting Button

    public void OnClick_SettingPanelOpen()
    {
        go_SettingPenal.SetActive(true);
    }

    #endregion


    #region Daily Reward Button

    public void OnClick_DailyRewardPanelOpen()
    {
        go_DailyRewardPanel.SetActive(true);
    }

    #endregion


    #region Daily Mission Button

    public void OnClick_DailyMissionPanelOpen()
    {
        go_DailyMissionPanel.SetActive(true);
    }

    #endregion
}
