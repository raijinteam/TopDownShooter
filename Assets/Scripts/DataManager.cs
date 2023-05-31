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

    [Header("Expedition")]
    public bool[] all_ExpeditionRunningStatus;
    public int expeditionOneIndex = 0;
    public int expeditionTwoIndex = 0;


    private void OnEnable()
    {
        
    }

    private void Awake()
    {
        if (instance != this || instance == null)
        {
            instance = this;
        }
    }




    private void Start()
    {
        //isSoundTurnOn = UiManager.instance.settingUI.SoundToggleButton;
        //isMusicTurnOn = UiManager.instance.settingUI.MusicToggleButton;
        //isHighGraphicsTurnOn = UiManager.instance.settingUI.GraphicsToggleButton;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearPlayerPrefsData();
        }
    }

    private void ClearPlayerPrefsData()
    {
        PlayerPrefs.DeleteAll();
    }

}
