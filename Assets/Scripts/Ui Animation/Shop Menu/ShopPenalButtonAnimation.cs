using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopPenalButtonAnimation : MonoBehaviour
{

    [SerializeField] private Image freeCrateInfo, equipmentCrateInfo, weaponsCrateInfo, specialCrateInfo;


    public void OnOpenFreeCrateInfoButtonCallback()
    {
        freeCrateInfo.gameObject.SetActive(true);
    }

    public void OnOpenEquipmentCrateInfoButtonCallback()
    {
        equipmentCrateInfo.gameObject.SetActive(true);
    }
    public void OnOpenWeaponsCrateInfoButtonCallback()
    {
        weaponsCrateInfo.gameObject.SetActive(true);
    }
    public void OnOpenSpecialCrateInfoButtonCallback()
    {
        specialCrateInfo.gameObject.SetActive(true);
    }


    // Close ..

    public void OnCloseFreeCrateInfoButtonCallback()
    {
        freeCrateInfo.gameObject.SetActive(false);
    }

    public void OnCloseEquipmentCrateInfoButtonCallback()
    {
        equipmentCrateInfo.gameObject.SetActive(false);
    }
    public void OnCloseWeaponsCrateInfoButtonCallback()
    {
        weaponsCrateInfo.gameObject.SetActive(false);
    }
    public void OnCloseSpecialCrateInfoButtonCallback()
    {
        specialCrateInfo.gameObject.SetActive(false);
    }

}
