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

    [Header("All Player Models")]
    [SerializeField] private GameObject[] all_Players;


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
        SetActiveHeroModel();
    }

    private void OnDisable()
    {

        if (all_Players.Length > 0)
        {
            for (int i = 0; i < all_Players.Length; i++)
            {
                all_Players[i].SetActive(false);
            }
        }
    }

    public void SetActiveHeroModel()
    {
        int index = HeroesManager.Instance.currentActiveSelectedHeroIndex;
        for (int i = 0; i < all_Players.Length; i++)
        {
            if (i == index)
            {
                all_Players[i].gameObject.SetActive(true);
            }
            else
            {
                all_Players[i].gameObject.SetActive(false);
            }
        }
    }


    #region Game Start Button

    public void OnClick_StartGame()
    {
        if (!DataManager.instance.isTutorialPlaying)
        {
            print("Game Start");
        }

    }

    #endregion


    #region Setting Button

    public void OnClick_SettingPanelOpen()
    {
        if (!DataManager.instance.isTutorialPlaying)
        {
            go_SettingPenal.SetActive(true);
        }
    }

    #endregion


    #region Daily Reward Button

    public void OnClick_DailyRewardPanelOpen()
    {
        if (!DataManager.instance.isTutorialPlaying)
        {
            go_DailyRewardPanel.SetActive(true);
        }
    }

    #endregion


    #region Daily Mission Button

    public void OnClick_DailyMissionPanelOpen()
    {
        if (!DataManager.instance.isTutorialPlaying)
        {
            go_DailyMissionPanel.SetActive(true);
        }
    }

    #endregion
}
