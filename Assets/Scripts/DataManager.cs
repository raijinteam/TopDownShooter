using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;



 
    [Header ("Player Data")]
    public int playerLevel;
    public int coins;

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
    }



    private void SetFirstTimeLoadData()
    {
        //Set Player coins
        SetPlayerCoins(coins);


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




        //Set Expedition ONe Panel Data
        UiManager.instance.ui_Home.ui_Expedition[0].expeditionState = (ExpeditionState)GetExpeditionState(0);
        UiManager.instance.ui_Home.ui_Expedition[0].SetExpeditionPanels();
        TimeCalculation.instance.SetExpeditionOneTimer(0);

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



    public void DecreaseCoins(int amount)
    {
        coins -= amount;
        SetPlayerCoins(coins);
    }




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
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_HEADEQ_CURRENT_MAT, value);
    }

    public void SetGunCurrentMat(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GUNEQ_CURRENT_MAT, value);
    }

    public void SetArrmorCurrentMat(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ARRMOREQ_CURRENT_MAT, value);
    }

    public void SetGloveurrentMat(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_GLOVESEQ_CURRENT_MAT, value);
    }
    public void SetAnythingCurrentMat(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ANYTHINGEQ_CURRENT_MAT, value);
    }
    public void SetAblitiesCurrentMat(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_ABLITIESEQ_CURRENT_MAT, value);
    }

    #endregion



    public void SetExpeditionState(int index , ExpeditionState expeditionState)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_EXPEDITIONSTATE + index, (int)expeditionState);
    }


    public void SetPlayerCoins(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.KEY_COINS, value);
    }
    public void SetExpeditionTimer(int index , float time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKey.KEY_EXPEDITIONTIME + index, time);
    }

    public void SetGameQuitTime(string quitTime)
    {
        PlayerPrefs.SetString(PlayerPrefsKey.KEY_GAME_QUIT_TIME, quitTime);
    }

    #endregion



    #region Get All Data



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


    public int GetPlayerCoins()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.KEY_COINS);
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
