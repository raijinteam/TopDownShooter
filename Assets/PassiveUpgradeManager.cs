using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgradeManager : MonoBehaviour
{
    public static PassiveUpgradeManager Instance;


    [HideInInspector] public int maxPassiveLevel = 10;

    [SerializeField] private PassiveUpgradeData[] all_PassiveData;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }

    }



    public void IncreasePassiveUpgradeCurrentLevel(int _passiveIndex)
    {
        all_PassiveData[_passiveIndex].currentLevel++;
    }

    public int GetCurrentPassivesLevel(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].currentLevel;
    }

    public string GetCurrentPassiveUpgradeName(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].passiveName;
    }

    public string GetCurrentPassiveUpgradeDesc(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].passiveDescription;
    }

}
