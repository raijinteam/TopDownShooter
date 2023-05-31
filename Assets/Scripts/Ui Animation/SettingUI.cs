using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header("Toggles Buttons")]
    [SerializeField] private Toggle toggle_Sound;
    [SerializeField] private Toggle toggle_Music;
    [SerializeField] private Toggle toggle_HighGrapgics;


    private void OnEnable()
    {
        toggle_Sound.isOn = DataManager.instance.isSoundTurnOn;
        toggle_Music.isOn = DataManager.instance.isMusicTurnOn;
        toggle_HighGrapgics.isOn = DataManager.instance.isHighGraphicsTurnOn;
    }


    public void OnClick_SoundValueChange()
    {
        if (toggle_Sound.isOn)
        {
            DataManager.instance.isSoundTurnOn = true;
        }
        else
        {
            DataManager.instance.isSoundTurnOn = false;
        }
    }

    public void OnClick_MusicValueChange()
    {
        if (toggle_Music.isOn)
        {
            DataManager.instance.isMusicTurnOn = true;
        }
        else
        {
            DataManager.instance.isMusicTurnOn = false;
        }
    }


    public void OnClick_HighGraphicsValueChange()
    {
        if (toggle_HighGrapgics.isOn)
        {
            DataManager.instance.isHighGraphicsTurnOn = true;
        }
        else
        {
            DataManager.instance.isHighGraphicsTurnOn = false;
        }
    }


    public void OnClick_SettingScreenClose()
    {
        this.gameObject.SetActive(false);
    }
}
