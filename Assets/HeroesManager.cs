using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    public static HeroesManager Instance;

    public int currentActiveSelectedHeroIndex; // currentActiveSelectedHero

    public int heroMaxLevel;

    public HeroData[] all_HeroData;


    private void Awake()
    {
        Instance = this;
    }


    public float SetHealthDataVisPer(int _heroIndex)
    {
        float perHealth = all_HeroData[_heroIndex].flt_MaxHealth * (all_HeroData[_heroIndex].flt_UpgradeHealth / 100f);
        return perHealth;
    }

    public float SetHeroDamagePer(int _heroIndex)
    {
        float perDamage = all_HeroData[_heroIndex].flt_Damage * (all_HeroData[_heroIndex].flt_UpgradeDamage / 100f);
        return perDamage;
    }

    public float SetHeroFireratePer(int _heroIndex)
    {
        float perDamage = all_HeroData[_heroIndex].flt_FireRate * (all_HeroData[_heroIndex].flt_UpgradeFirerate / 100f);
        return perDamage;
    }

    public bool hasEnoughCardsToUpgrade(int _heroIndex)
    {
        if(all_HeroData[_heroIndex].currentCards >= all_HeroData[_heroIndex].requireCardsToUnlock)
        {
            return true;
        }
        return false;
    }

    public bool hasEnoughCoinsToUpgrade(int _heroIndex)
    {
        if(DataManager.instance.coins >= all_HeroData[_heroIndex].coinsForUpgrade)
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

    public void UpgradeSelectedHero(int _selectedHeroIndex)
    {
        print("Upgrade Herp");
        all_HeroData[_selectedHeroIndex].currentCards -= all_HeroData[_selectedHeroIndex].requireCardsToUnlock;
        DataManager.instance.coins -= all_HeroData[_selectedHeroIndex].coinsForUpgrade;
        all_HeroData[_selectedHeroIndex].currentLevel++;
        all_HeroData[_selectedHeroIndex].flt_MaxHealth += SetHealthDataVisPer(_selectedHeroIndex);
        all_HeroData[_selectedHeroIndex].flt_Damage += SetHeroDamagePer(_selectedHeroIndex);
        all_HeroData[_selectedHeroIndex].flt_FireRate += SetHeroFireratePer(_selectedHeroIndex);
    }


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

    public float GetHeroHealth(int _heroIndex)
    {
        return all_HeroData[_heroIndex].flt_MaxHealth;
    }

    public void SetHeroHealth(int _heroIndex, float _value)
    {
        all_HeroData[_heroIndex].flt_MaxHealth += _value;
    }

    public void DeceraceHeroHealth(int _heroIndex, float _value)
    {
        all_HeroData[_heroIndex].flt_MaxHealth -= _value;
    }

    public float GetHeroDamage(int _heroIndex)
    {
        return all_HeroData[_heroIndex].flt_Damage;
    }

    public void DeceraceHeroDamage(int _heroIndex, float _value)
    {
        all_HeroData[_heroIndex].flt_Damage -= _value;
    }

    public void SetHeroDamage(int _heroIndex, float _value)
    {
        all_HeroData[_heroIndex].flt_Damage += _value;
    }

    public void DeceraceHeroFirerate(int _heroIndex, float _value)
    {
        all_HeroData[_heroIndex].flt_FireRate -= _value;
    }

    public float GetHeroFirerate(int _heroIndex)
    {
        return all_HeroData[_heroIndex].flt_FireRate;
    }

    public void SetHeroFirerate(int _heroIndex, float _value)
    {
        all_HeroData[_heroIndex].flt_FireRate += _value;
    }
}
