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

    [Header("Expedition Ui")]
    public ExpeditionUI[] ui_Expedition;

    private void OnEnable()
    {
        homeCamera.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        homeCamera.gameObject.SetActive(false);
    }

   

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
