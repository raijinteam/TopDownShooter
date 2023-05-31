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
}
