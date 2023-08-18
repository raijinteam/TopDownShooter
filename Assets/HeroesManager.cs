using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    public static HeroesManager Instance;

    public int currentActiveSelectedHeroIndex; // currentActiveSelectedHero

    public int heroMaxLevel;

    public HeroData[] all_HeroData;


    [Header("For Testing")]
    public int increaseCardIndex = 0;




    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Increase Player Cards in Hero Manager");
            IncreasePlayerCards(increaseCardIndex, 1);
        }
    }


    public void SetHeroDataWhenFirstLoad()
    {
        for (int i = 0; i < all_HeroData.Length; i++)
        {
            DataManager.instance.SetHeroLevel(i, 1);
            DataManager.instance.SetHeroCards(i, 0);
            if(i == 0)
            {
                DataManager.instance.SetHeroLockState(i, false);
            }
            else
            {
                DataManager.instance.SetHeroLockState(i, true);

            }
        }

        GetHeroData();
    }


    public void GetHeroData()
    {
        for(int i = 0; i < all_HeroData.Length; i++)
        {
            all_HeroData[i].currentLevel = DataManager.instance.GetHeroLevel(i);
            all_HeroData[i].currentCards = DataManager.instance.GetHeroCards(i);
            all_HeroData[i].isLocked = DataManager.instance.GetHeroLockState(i);
        }
    }




    private void IncreasePlayerCards(int index , int amouubt)
    {
        all_HeroData[index].currentCards += amouubt;
        DataManager.instance.SetHeroCards(index, all_HeroData[index].currentCards);
        UiManager.instance.ui_PlayerManager.ui_HeroSelection.IncreaseCards(index);
    }
   

    public bool hasEnoughCardsToUpgrade(int _heroIndex)
    {
        int currentHeroLevel = all_HeroData[_heroIndex].currentLevel;
        if(all_HeroData[_heroIndex].currentCards >= all_HeroData[_heroIndex].requireCardsToUnlock[currentHeroLevel])
        {
            return true;
        }
        return false;
    }

    public bool hasEnoughCoinsToUpgrade(int _heroIndex)
    {
        int currentHeroLevel = all_HeroData[_heroIndex].currentLevel;
        if (DataManager.instance.coins >= all_HeroData[_heroIndex].coinsForUpgrade[currentHeroLevel])
        {
            return true;
        }

        return false;
    }

    public bool IsHeroReachMaxLevel(int _heroIndex)
    {
        if(all_HeroData[_heroIndex].currentLevel == heroMaxLevel)
        {
            return true;
        }

        return false;
    }



    #region Get Selected Player Updated States

    public float GetHeroUpgradeHealth(int index)
    {
        float health = all_HeroData[index].flt_MaxHealth[all_HeroData[index].currentLevel + 1] - all_HeroData[index].flt_MaxHealth[all_HeroData[index].currentLevel];

        return health;
    }
    public float GetHeroUpgradeDamage(int index)
    {
        float value = all_HeroData[index].flt_Damage[all_HeroData[index].currentLevel + 1] - all_HeroData[index].flt_Damage[all_HeroData[index].currentLevel];

        return value;
    }

    public float GetHeroUpgradeArrmor(int index)
    {
        float value = all_HeroData[index].flt_Arrmor[all_HeroData[index].currentLevel + 1] - all_HeroData[index].flt_Arrmor[all_HeroData[index].currentLevel];

        return value;
    }

    public float GetHeroUpgradeFirerate(int index)
    {
        float value = all_HeroData[index].flt_FireRate[all_HeroData[index].currentLevel + 1] - all_HeroData[index].flt_FireRate[all_HeroData[index].currentLevel];

        return value;
    }

    public float GetHeroUpgradeForce(int index)
    {
        float value = all_HeroData[index].flt_Force[all_HeroData[index].currentLevel + 1] - all_HeroData[index].flt_Force[all_HeroData[index].currentLevel];

        return value;
    }


    #endregion


    // GETTER FOR PROFILE

    public int GetHeroLevel(int _heroIndex)
    {
        return all_HeroData[_heroIndex].currentLevel;
    }

    public string GetHeroName(int _heroIndex)
    {
        return all_HeroData[_heroIndex].str_HeroName;
    }

    public string GetHeroDescription(int _heroIndex)
    {
        return all_HeroData[_heroIndex].str_HeroDescription;
    }

    //GETTER FOR STATES

  
}
