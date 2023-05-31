using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeadInventoryItemData
{
    public int id;
    public int currentLevel;
    public string name;
    public Sprite sprite;


    public void SetHeadInventoryData(int _currentLevel , Sprite _sprite)
    {
        currentLevel = _currentLevel;
        sprite = _sprite;
    }
}
