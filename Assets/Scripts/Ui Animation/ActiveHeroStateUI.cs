using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveHeroStateUI : MonoBehaviour
{

    [Header("State Data")]
    [SerializeField] private TextMeshProUGUI txt_HealthAmount;
    [SerializeField] private TextMeshProUGUI txt_DamageAmount;
    [SerializeField] private TextMeshProUGUI txt_FirerateAmount;


    private void Start()
    {
        SetActiveHeroState(0);
    }

    public void SetActiveHeroState(int _ActiveHeroIndex)
    {
        txt_HealthAmount.text = HeroesManager.Instance.GetHeroHealth(_ActiveHeroIndex).ToString();
        txt_DamageAmount.text = HeroesManager.Instance.GetHeroDamage(_ActiveHeroIndex).ToString();
        txt_FirerateAmount.text = HeroesManager.Instance.GetHeroFirerate(_ActiveHeroIndex).ToString();
    }

    public void SetActiveHeroHealth(int _ActiveHeroIndex , float _value)
    {
        HeroesManager.Instance.SetHeroHealth(_ActiveHeroIndex, _value);
        txt_HealthAmount.text = HeroesManager.Instance.GetHeroHealth(_ActiveHeroIndex).ToString() ;
    }

    public void DeceraceHeroHealth(int _ActiveHeroIndex, float _value)
    {
        HeroesManager.Instance.DeceraceHeroHealth(_ActiveHeroIndex, _value);
        txt_HealthAmount.text = HeroesManager.Instance.GetHeroHealth(_ActiveHeroIndex).ToString();
    }

    public void SetActiveHeroDamage(int _ActiveHeroIndex, float _value)
    {
        HeroesManager.Instance.SetHeroDamage(_ActiveHeroIndex, _value);
        txt_DamageAmount.text = HeroesManager.Instance.GetHeroDamage(_ActiveHeroIndex).ToString();
    }

    public void DeceraceHeroDamage(int _ActiveHeroIndex, float _value)
    {
        HeroesManager.Instance.DeceraceHeroDamage(_ActiveHeroIndex, _value);
        txt_HealthAmount.text = HeroesManager.Instance.GetHeroHealth(_ActiveHeroIndex).ToString();
    }

    public void SetActiveHeroFirerate(int _ActiveHeroIndex, float _value)
    {
        HeroesManager.Instance.SetHeroFirerate(_ActiveHeroIndex, _value);
        txt_FirerateAmount.text = HeroesManager.Instance.GetHeroFirerate(_ActiveHeroIndex).ToString();
    }

    public void DeceraceHeroFirerate(int _ActiveHeroIndex, float _value)
    {
        HeroesManager.Instance.DeceraceHeroFirerate(_ActiveHeroIndex, _value);
        txt_HealthAmount.text = HeroesManager.Instance.GetHeroHealth(_ActiveHeroIndex).ToString();
    }
}
