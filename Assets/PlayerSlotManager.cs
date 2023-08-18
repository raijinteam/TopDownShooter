using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlotManager : MonoBehaviour
{
    public static PlayerSlotManager instance;


    public bool isHeadItemEquipped;
    public bool isGunItemEquipped;
    public bool isArrmorItemEquipped;
    public bool isGlovesItemEquipped;
    public bool isAblitiesItemEquipped;
    public bool isAnythingItemEquipped;

    private void Awake()
    {
        instance = this;
    }


    public void SetAllEQState()
    {
        DataManager.instance.SetHeadEQState(false);
        DataManager.instance.SetGunEQState(false);
        DataManager.instance.SetArrmorEQState(false);
        DataManager.instance.SetGlovesEQState(false);
        DataManager.instance.SetAnytimeEQState(false);
        DataManager.instance.SetAblitiesEQState(false);

        GetAllEQState();
    }

    public void GetAllEQState()
    {
        Debug.Log("Second method called");
        isHeadItemEquipped = DataManager.instance.GetHeadEQState();
        isGunItemEquipped = DataManager.instance.GetGunEQState();
        isArrmorItemEquipped = DataManager.instance.GetArrmorEQState();
        isGlovesItemEquipped = DataManager.instance.GetGlovesEQState();
        isAblitiesItemEquipped = DataManager.instance.GetAnytimeEQState();
        isAnythingItemEquipped = DataManager.instance.GetAblitiesEQState();
    }


}
