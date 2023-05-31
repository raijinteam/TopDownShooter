using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroData : MonoBehaviour
{
    // profile
    public bool isLocked;
    public Sprite heroSprite;
    public string str_HeroName;
    [TextArea(2,6)]
    public string str_HeroDescription;
    public int currentLevel;
    public int coinsForUpgrade;
    public int currentCards;
    public int requireCardsToUnlock;


    [Space]

    [Header("Hero State")]
    // Stats
    public float flt_MoveSpeed;
    public float flt_MaxHealth;
    public float flt_Damage;
    public float flt_FireRate;

    [Header("Update Hero State")]

    //Upgrade States
    public float flt_UpgradeMoveSpeed;
    public float flt_UpgradeHealth;
    public float flt_UpgradeDamage;
    public float flt_UpgradeFirerate;
}
