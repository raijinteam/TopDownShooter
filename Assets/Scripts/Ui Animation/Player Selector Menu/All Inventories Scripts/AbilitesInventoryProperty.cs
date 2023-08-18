using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitesInventoryProperty : MonoBehaviour
{
    public int currentLevel;
    public string name;
    public Sprite sprite;
    public bool isLocked = true;

    public int currentCards;
    public int[] requireCoinsToUpgrade;
    public float[] currentFirerate;
    public int[] requireMaterialToLevelUp;
}
