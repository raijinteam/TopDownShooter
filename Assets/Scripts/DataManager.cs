using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;



 
    [Header ("Player Data")]
    public int userLevel;
    public float userCurrentXP;
    public int activePlayerIndex;
    public float userRequireXP;
    public int increaseXPPerCount;
    public int coins;
    public int gems;
    public int energy;
    public int skipits;

    [Header("daily Reward")]
    public bool isRewardClaim;

    [Header("Settings")]
    public bool isSoundTurnOn;
    public bool isMusicTurnOn;
    public bool isHighGraphicsTurnOn;

    
    private void Awake()
    {
        if (instance != this || instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey.KEY_COINS))
        {
            GetPlayerData();
        }
        else
        {
            SetFirstTimeLoadData();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearPlayerPrefsData();
        }

        //Only FOr testing
        if (Input.GetKeyDown(KeyCode.A))
        {
            IncreaseUserCurrentXP(10);
        }

    }



    private void SetFirstTimeLoadData()
    {
        //Set Player coins
        SetPlayerCoins(coins);
        SetPlayerGems(gems);
        SetPlayerEnergy(energy);
        SetPlayerSkipits(skipits);

        SetUserLevel(1);
        SetUserXP(0);
        SetUserRequireXP((int)userRequireXP);

        UiManager.instance.ui_CommonPanel.SetCommonPanelData();

        SetDailyRewardActiveState(false);
        isRewardClaim = GetDailyRewardActiveState();


        //Set All Passive Upgrade Level
        UiManager.instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.SetAllPassiveUpgradeLevels();


        //Set Daily Reward time
        SetDailyRewardTime(TimeCalculation.instance.dailyRewardTimeInSeconds);
        TimeCalculation.instance.SetDailyRewardTime();

        //Daily Mission
        SetDailyMissionTime(TimeCalculation.instance.dailyMissionTimeInSecond);
        TimeCalculation.instance.SetDailyMissionTimeData();
        SetDailyMissionOnedayComplete(true);
        UiManager.instance.ui_DailyMission.SpawnDailyMissions();

        //Set Active Hero INdex
        SetActiveHeroIndex(0);
        HeroesManager.Instance.currentActiveSelectedHeroIndex = GetActiveHeroIndex();

        //Set Active Hero in home screen
        UiManager.instance.ui_Home.SetActiveHeroModel();

        //Set All Heros Data
        HeroesManager.Instance.SetHeroDataWhenFirstLoad();

        //Daily Mission Time Data
        
        

        //Set All EQ States
        PlayerSlotManager.instance.SetAllEQState();

        //Set all equment datas
        SetAllEQDataAtOneTime();



        //set expedition one 
        SetExpeditionState(0, ExpeditionState.start);
        SetExpeditionState(1, ExpeditionState.start); // Set Expedition Two State
    }



    private void GetPlayerData()
    {
        coins = GetPlayerCoins(); // Get player total coins
        gems = GetPlayerGems();
        energy = GetPlayerEnergy();
        skipits = GetPlayerSkipits();

        userLevel = GetUserLevel();
        userCurrentXP = GetUserCurrentXP();
        userRequireXP = GetUserRequireXP();

        UiManager.instance.ui_CommonPanel.SetCommonPanelData();


        UiManager.instance.ui_DailyMission.SpawnDailyMissions();//Spawn All Daily missions


        //Get All Passive Upgrade Levels
        UiManager.instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.GetAllPassiveUpgradeLevels();

        //Set Actibe Hero Index
        HeroesManager.Instance.currentActiveSelectedHeroIndex = GetActiveHeroIndex();

        //Set Active Hero in home screen
        UiManager.instance.ui_Home.SetActiveHeroModel();

        //Get All Hero Data
        HeroesManager.Instance.GetHeroData();


        //Daily Mission Time Data
        TimeCalculation.instance.SetDailyMissionTimeData();


        //Set Expedition ONe Panel Data
        UiManager.instance.ui_Home.ui_Expedition[0].SetExpeditionState();
        UiManager.instance.ui_Home.ui_Expedition[1].SetExpeditionState();


        //Daily reward Time set
        TimeCalculation.instance.SetDailyRewardTime();
        UiManager.instance.ui_DailyReward.isRewardClimed = GetDailyRewardActiveState();

        //Get ALl Eq Slot 
        PlayerSlotManager.instance.GetAllEQState();

        //Get all eq data 
        GetAllEQData();

        //Set Expedition ONe Panel Data
        UiManager.instance.ui_Home.ui_Expedition[1].expeditionState = (ExpeditionState)GetExpeditionState(1);
        UiManager.instance.ui_Home.ui_Expedition[1].SetExpeditionPanels();
    }


    private void SetAllEQDataAtOneTime()
    {
        SlotHeadEquipmentManager.instance.SetEqData(0);
        SlotGunsManager.instance.SetEqData(0);
        SlotArrmorManager.instance.SetEqData(0);
        SlotGlovesManager.instance.SetEqData(0);
        SlotAnythingManager.instance.SetEqData(0);
        SlotAblitiesManager.instance.SetEqData(0);
    }

    private void GetAllEQData()
    {
        SlotHeadEquipmentManager.instance.GetEQData();
        SlotGunsManager.instance.GetEQData();
        SlotArrmorManager.instance.GetEQData();
        SlotGlovesManager.instance.GetEQData();
        SlotAnythingManager.instance.GetEQData();
        SlotAblitiesManager.instance.GetEQData();
    }


    private void ClearPlayerPrefsData()
    {
        Debug.Log("Clear data");
        PlayerPrefs.DeleteAll();
    }


    public bool CheckForUserHasEnoughXPForLevelUP()
    {
        if(userCurrentXP >= userRequireXP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseUserCurrentXP(int _amount)
    {
        if (CheckForUserHasEnoughXPForLevelUP())
        {
            //Give Level UP Reward 
            userLevel++;
            SetUserLevel(userLevel);
            CalculateUserXP();
            RewardManager.Instance.GiveRewardLevelUP(userLevel);
        }

        userCurrentXP +=_amount;
        SetUserXP((int)userCurrentXP);
        UiManager.instance.ui_CommonPanel.SetUserCurrentXP();
    }

    public void CalculateUserXP()
    {
        float increasePer = userRequireXP * (increaseXPPerCount / 100f);
        Debug.Log("Increase Per : " + increasePer);
        userRequireXP += increasePer;
    }



    #region Game Currency 



    public bool HasEnoughGemsForUse(int amount)
    {
        if(gems >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasEnoughCoinsForUse(int amount)
    {
        if (coins >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasEnoughEnergyForUse(int amount)
    {
        if (energy >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasEnoughSkipitsForUse(int amount)
    {
        if (skipits >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void IncreaseCoins(int amount)
    {
        coins += amount;
        SetPlayerCoins(coins);
    }

    public void IncreaseGems(int amount)
    {
        gems += amount;
        SetPlayerGems(gems);
    }

    public void IncreaseEnergy(int amount)
    {
        energy += amount;
        SetPlayerEnergy(energy);
    }
    public void IncreaseSkipIts(int amount)
    {
        skipits += amount;
        SetPlayerSkipits(skipits);
    }

    public void DecreaseCoins(int amount)
    {
        coins -= amount;
        SetPlayerCoins(coins);
    }
    public void DecreaseGems(int amount)
    {
        gems -= amount;
        SetPlayerGems(gems);
    }

    public void DecreaseEnergy(int amount)
    {
        energy -= amount;
        SetPlayerEnergy(energy);
    }
    public void decreaseSkipIts(int amount)
    {
        skipits -= amount;
        SetPlayerSkipits(skipits);
    }

    #endregion






    #region Set All Data

    #region All player EQ Data

    public void SetHeadEQState(bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HEADEQ_STATE, 0);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HEADEQ_STATE, 1);
        }
    }
    public void SetGunEQState(bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GUNEQ_STATE, 0);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GUNEQ_STATE, 1);
        }
    }
    public void SetArrmorEQState(bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ARRMOREQ_STATE, 0);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ARRMOREQ_STATE, 1);
        }
    }
    public void SetGlovesEQState(bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GLOVEEQ_STATE, 0);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GLOVEEQ_STATE, 1);
        }
    }
    public void SetAnytimeEQState(bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_STATE, 0);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_STATE, 1);
        }
    }
    public void SetAblitiesEQState(bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ABLITIESEQ_STATE, 0);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ABLITIESEQ_STATE, 1);
        }
    }

    public void SetHeadActiveIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HEAD_SELECTED_INDEX, index);
    }

    public void SetGunActiveIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GUN_SELECTED_INDEX, index);
    }

    public void SetArrmorActiveIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ARRMOR_SELECTED_INDEX, index);
    }

    public void SetGloveActiveIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GLOVE_SELECTED_INDEX, index);
    }

    public void SetAnythingActiveIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ANYTHING_SELECTED_INDEX, index);
    }

    public void SetAblititesActiveIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ABLITIES_SELECTED_INDEX, index);
    }



    //HEAD EQ LEVEL
    public void SetHeadEQLevel(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HEADEQ_LEVEL + index, value);
    }
    //HEAD EQ LEVEL
    public void SetGunEQLevel(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GUNEQ_LEVEL + index, value);
    }
    //HEAD EQ LEVEL
    public void SetArrmorEQLevel(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ARRMOREQ_LEVEL + index, value);
    }
    //HEAD EQ LEVEL
    public void SetGloveEQLevel(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GUNEQ_LEVEL + index, value);
    }
    //HEAD EQ LEVEL
    public void SetAnythingEQLevel(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_LEVEL + index, value);
    }
    //HEAD EQ LEVEL
    public void SetAbiitiesEQLevel(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ABLITIESEQ_LEVEL + index, value);
    }


    public void SetHeadCurrentMat(int value)
    {
        Debug.Log("Set Head Mat");
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HEADEQ_CURRENT_MAT, value);
    }

    public void SetGunCurrentMat(int value)
    {
        Debug.Log("Set Gun Mat");
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GUNEQ_CURRENT_MAT, value);
    }

    public void SetArrmorCurrentMat(int value)
    {
        Debug.Log("Set Arrmor Mat");
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ARRMOREQ_CURRENT_MAT, value);
    }

    public void SetGloveurrentMat(int value)
    {
        Debug.Log("Set Gloves Mat");
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GLOVESEQ_CURRENT_MAT, value);
    }
    public void SetAnythingCurrentMat(int value)
    {
        Debug.Log("Set Anyhting Mat");
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_CURRENT_MAT, value);
    }
    public void SetAblitiesCurrentMat(int value)
    {
        Debug.Log("Set Abllites Mat");
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ABLITIESEQ_CURRENT_MAT, value);
    }

    #endregion





    #region All Heros Data

    public void SetHeroLockState(int index, bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HERO_LOCK_STATE + index, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HERO_LOCK_STATE + index, 0);
        }
    }


    public void SetActiveHeroIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ACTIVE_HERO_INDEX, index);
    }

    public void SetHeroLevel(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HERO_LEVEL + index, value);
    }

    public void SetHeroCards(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HERO_CURRENT_CARD + index, value);
    }



    #endregion


    #region USer data

    public void SetUserRequireXP(int amount)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_USER_REQUIRE_XP, amount);
    }

    public void SetUserLevel(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_USER_LEVEL , index);
    }

    public void SetUserXP(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_USER_CURRENT_XP, value);
    }

    public void SetPlayerCoins(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_COINS, value);
    }

    public void SetPlayerGems(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GEMS, value);
    }

    public void SetPlayerEnergy(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ENERGY, value);
    }

    public void SetPlayerSkipits(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_SKIPITS, value);
    }

    #endregion


    public void SetPassiveUpgradeLevel(int index , int level)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_PASSIVE_UPGRADE_LEVEL + index, level);
    }

    public void SetDailyMissionOnedayComplete(bool value)
    {
        if (value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_MISSION_ONEDAY_COMPLETE, 0);
        }
        else
        {
            Debug.Log("Set One Day true");
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_MISSION_ONEDAY_COMPLETE, 1);
        }
    }

    public void SetDailyMisisonSOIndex(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_MISSION_INDEX + index, value);
    }

    public void SetDailyMissionRewardState(int index, bool _state)
    {
        if (_state == false)
        { PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_MISSION_REWARD_CLAIMED + index, 0); }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_MISSION_REWARD_CLAIMED + index, 1);
        }
    }

    public void SetDailyMissionTime(float time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKey.KEY_DAILY_MISSION_TIME, time);
    }

    public void SetAchivementCurrentValue(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILYMISSION_CURRENT_VALUE + index, value);
    }


    public void SetExpeditionState(int index , ExpeditionState expeditionState)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_EXPEDITIONSTATE + index, (int)expeditionState);
    }


    
    public void SetExpeditionTimer(int index , float time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKey.KEY_EXPEDITIONTIME + index, time);
    }


    public void SetDailyRewardDayIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_REWARD_DAY_INDEX, index);
    }

    public void SetDailyRewardTime(float time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKey.KEY_DAILY_REWARD_TIME, time);
    }

    public void SetDailyRewardActiveState(bool value)
    {
        if(value == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_REWARD_COMPLETE, 0);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.KEY_DAILY_REWARD_COMPLETE, 1);
        }
    }

    public void SetGameQuitTime(string quitTime)
    {
        PlayerPrefs.SetString(PlayerPrefsKey.KEY_GAME_QUIT_TIME, quitTime);
    }

    #endregion



    #region Get All Data






    #region Get User Data

    public int GetUserRequireXP()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_USER_REQUIRE_XP);
    }

    public int GetUserLevel()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_USER_LEVEL);
    }

    public int GetUserCurrentXP()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_USER_CURRENT_XP);
    }


    public int GetPlayerCoins()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_COINS);
    }

    public int GetPlayerGems()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GEMS);
    }

    public int GetPlayerEnergy()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ENERGY);
    }

    public int GetPlayerSkipits()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_SKIPITS);
    }

    #endregion



    #region Get ALl Heros Data

    public bool GetHeroLockState(int index)
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_HERO_LOCK_STATE + index) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetActiveHeroIndex()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ACTIVE_HERO_INDEX);
    }

    public int GetHeroLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_HERO_LEVEL + index);
    }

    public int GetHeroCards(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_HERO_CURRENT_CARD + index);
    }

    #endregion




    #region All EQ Data


    public bool GetHeadEQState()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_HEADEQ_STATE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool GetGunEQState()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GUNEQ_STATE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool GetArrmorEQState()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ARRMOREQ_STATE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool GetGlovesEQState()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GLOVEEQ_STATE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool GetAnytimeEQState()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_STATE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool GetAblitiesEQState()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ABLITIESEQ_STATE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetHeadActiveIndex( )
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_HEAD_SELECTED_INDEX);
    }

    public int GetGunActiveIndex( )
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GUN_SELECTED_INDEX);
    }

    public int GetArrmorActiveIndex( )
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ARRMOR_SELECTED_INDEX);
    }

    public int GetGloveActiveIndex( )
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GLOVE_SELECTED_INDEX);
    }

    public int GetAnythingActiveIndex( )
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ANYTHING_SELECTED_INDEX);
    }

    public int GetAblititesActiveIndex( )
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ABLITIES_SELECTED_INDEX);
    }



    public int GetHeadCurrentLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_HEADEQ_LEVEL + index);
    }

    public int GetGunCurrentLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GUNEQ_LEVEL + index);
    }
    public int GetArrmorCurrentLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ARRMOREQ_LEVEL + index);
    }
    public int GetGloveCurrentLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GLOVESEQ_LEVEL + index);
    }
    public int GetAnythingCurrentLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_LEVEL + index);
    }
    public int GetAblitiesCurrentLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ABLITIESEQ_LEVEL + index);
    }

    public int GetHeadMaterials()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_HEADEQ_CURRENT_MAT);
    }

    public int GetGunMaterials()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GUNEQ_CURRENT_MAT);
    }

    public int GetArrmorMaterials()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ARRMOREQ_CURRENT_MAT);
    }

    public int GetGloveMaterials()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_GLOVESEQ_CURRENT_MAT);
    }

    public int GetAnythingMaterials()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_CURRENT_MAT);
    }

    public int GetAblitiesMaterials()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_ABLITIESEQ_CURRENT_MAT);
    }


    #endregion

    public int GetPassiveUpgradeLevel(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_PASSIVE_UPGRADE_LEVEL + index);
    }

    public bool GetDailyMissionOneDayComplete()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_DAILY_MISSION_ONEDAY_COMPLETE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetDailyMisisonSOIndex(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_DAILY_MISSION_INDEX + index);
    }

    public bool GetDailyMissionClaimedState(int index)
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKey.KEY_DAILY_MISSION_REWARD_CLAIMED + index) == 0)
            return false;
        else
            return true;
    }

    public float GetDailyMissionTime()
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKey.KEY_DAILY_MISSION_TIME);
    }
    public int GetAchivementCurrentValue(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_DAILYMISSION_CURRENT_VALUE + index);
    }


    public int GetDailyRewardDayIndex()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_DAILY_REWARD_DAY_INDEX);
    }

    public float GetDailyRewardTime()
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKey.KEY_DAILY_REWARD_TIME);
    }

    public bool GetDailyRewardActiveState()
    {
        if(PlayerPrefs.GetInt(PlayerPrefsKey.KEY_DAILY_REWARD_COMPLETE) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public float GetExpeditionTime(int index)
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKey.KEY_EXPEDITIONTIME + index);
    }

    public int GetExpeditionState(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_EXPEDITIONSTATE + index);
    }

    public string GetGameQuitTime()
    {
        return PlayerPrefs.GetString(PlayerPrefsKey.KEY_GAME_QUIT_TIME);
    }

    #endregion

}
