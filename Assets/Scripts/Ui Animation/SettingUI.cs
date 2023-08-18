using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Image img_MusicTick;
    [SerializeField] private Image img_SoundTick;


    private bool isMusicOn;
    private bool isSoundOn;

    private void OnEnable()
    {

        isMusicOn = DataManager.instance.isMusicTurnOn;
        isSoundOn = DataManager.instance.isSoundTurnOn;

        CheckIfMusicIsOn();
        CheckIfSoundIsOn();
    }


    private void CheckIfMusicIsOn()
    {
        if (isMusicOn)
        {
            img_MusicTick.gameObject.SetActive(true);
        }
        else
        {
            img_MusicTick.gameObject.SetActive(false);
        }
    }

    private void CheckIfSoundIsOn()
    {
        if (isSoundOn)
        {
            img_SoundTick.gameObject.SetActive(true);
        }
        else
        {
            img_SoundTick.gameObject.SetActive(false);
        }
    }

    public void OnClick_SoundValueChange()
    {
        if (!isSoundOn)
        {
            DataManager.instance.isMusicTurnOn = true;
            isSoundOn = true;
        }
        else
        {
            DataManager.instance.isMusicTurnOn = false;
            isSoundOn = false;
        }
        CheckIfSoundIsOn();
    }

    public void OnClick_MusicValueChange()
    {
        if (!isMusicOn)
        {
            DataManager.instance.isSoundTurnOn = true;
            isMusicOn = true;
        }
        else
        {
            DataManager.instance.isSoundTurnOn = false;
            isMusicOn = false;
        }
        CheckIfMusicIsOn();
        
    }




    public void OnClick_SettingScreenClose()
    {
        this.gameObject.SetActive(false);
    }
}
