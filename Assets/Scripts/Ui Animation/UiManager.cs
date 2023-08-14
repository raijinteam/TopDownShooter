using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public NavigationMenuAnimation ui_Navbar;
    public HomeUI ui_Home; // ui_Home
    public SettingUI ui_Setting; // ui_Settings
    public PassiveUpgradeUI ui_PassiveUpgrade;
    public RewardSummaryUI rewardSummaryUI; // ui_RewardSummary
    public PlayerUI ui_PlayerManager;
    public ExpeditionPanelUI[] ui_ExpeditionPanel;

    [SerializeField] private bool canChangeMenus;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    public bool CanChangeMenus
    {
        get
        {
            return canChangeMenus;
        }
        set
        {
            canChangeMenus = value;
        }
    }

}
