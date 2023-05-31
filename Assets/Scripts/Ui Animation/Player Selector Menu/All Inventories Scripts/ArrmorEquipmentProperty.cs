using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrmorEquipmentProperty : MonoBehaviour
{
    public int currentLevel;
    public string name;
    public Sprite sprite;
    public bool isLocked = true;

    public int requireCoinsToUpgrade;
    public float currentHealth;
    public float healthIncrease;
    public int[] requireMaterialToLevelUp;
}
