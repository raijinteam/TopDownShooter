using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEquipmentProperty : MonoBehaviour
{
    public int currentLevel;
    public string name;
    public Sprite sprite;
    public bool isLocked = true;

    public int requireCoinsToUpgrade;
    public float currentDamage;
    public float damageIncrease;
    public int[] requireMaterialToLevelUp;
}
