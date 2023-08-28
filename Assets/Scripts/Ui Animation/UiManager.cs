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
    public PlayerUI ui_PlayerManager;
    public ExpeditionPanelUI[] ui_ExpeditionPanel;
    public CommonPanelUI ui_CommonPanel;
    public RewardUI ui_Reward;
    public DailyRewardUI ui_DailyReward;
    public DailyMissionUI ui_DailyMission;
    public EnergyPurchaseUI ui_EnergyPurchase;
    public ShopPanelUI ui_Shop;
    public TutorialUI ui_tutorial;

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
