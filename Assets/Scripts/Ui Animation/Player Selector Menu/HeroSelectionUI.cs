using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroSelectionUI : MonoBehaviour
{
    [SerializeField] private HeroUpgradeUI ui_HeroUpgrade;

    [SerializeField] private GameObject[] all_Heros;
    [SerializeField] HeroSelectionData[] heroData;

    [SerializeField] private Button btn_Selection;

    [SerializeField] private TextMeshProUGUI txt_SelectedHeroName;
    [SerializeField] private TextMeshProUGUI txt_HeroDescription;
    [SerializeField] private TextMeshProUGUI txt_SelectHeroHealth;
    [SerializeField] private TextMeshProUGUI txt_SelectHeroDamage;
    [SerializeField] private TextMeshProUGUI txt_SelectHeroFirerate;

    private int currentluActiveHero;
    private void OnEnable()
    {
        //Check if Hero Upgrade avaliable
        SetAllHeroDataInScrollView();
        //Set Active hero data
        SetCurrentlyActiveHeroData();
       
    }
    private void OnDisable()
    {
        for(int i =0; i < all_Heros.Length; i++)
        {
            all_Heros[i].SetActive(false);
        }
    }

    //SET ACTIVE PLAYER DATA WHEN ENABLE
    private void SetCurrentlyActiveHeroData()
    {
        currentluActiveHero = HeroesManager.Instance.currentActiveSelectedHeroIndex;
        all_Heros[currentluActiveHero].SetActive(true);
        heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(true);
        txt_SelectedHeroName.text = HeroesManager.Instance.GetHeroName(currentluActiveHero);
        txt_HeroDescription.text = HeroesManager.Instance.GetHeroDescription(currentluActiveHero);
        txt_SelectHeroHealth.text = HeroesManager.Instance.GetHeroHealth(currentluActiveHero).ToString();
        txt_SelectHeroDamage.text = HeroesManager.Instance.GetHeroDamage(currentluActiveHero).ToString();
        txt_SelectHeroFirerate.text = HeroesManager.Instance.GetHeroFirerate(currentluActiveHero).ToString();
    }

    //SET ALL PLAYER DATA IN SCROLL VIEW
    public void SetAllHeroDataInScrollView()
    {
        for(int i =0; i < heroData.Length; i++)
        {
            if (!HeroesManager.Instance.all_HeroData[i].isLocked)
            {
                heroData[i].txt_currentHeroLevel.gameObject.SetActive(true);
                heroData[i].txt_CurrentCards.gameObject.SetActive(true);
                heroData[i].txt_RequireCards.gameObject.SetActive(true);
                heroData[i].slider_cards.gameObject.SetActive(true);
               


                heroData[i].txt_currentHeroLevel.text = "Level " + HeroesManager.Instance.all_HeroData[i].currentLevel.ToString();
                heroData[i].slider_cards.maxValue = HeroesManager.Instance.all_HeroData[i].requireCardsToUnlock;
                heroData[i].slider_cards.value = HeroesManager.Instance.all_HeroData[i].currentCards;
                heroData[i].txt_CurrentCards.text = HeroesManager.Instance.all_HeroData[i].currentCards.ToString();
                heroData[i].txt_RequireCards.text = HeroesManager.Instance.all_HeroData[i].requireCardsToUnlock.ToString();

                if (HeroesManager.Instance.all_HeroData[i].currentCards >= HeroesManager.Instance.all_HeroData[i].requireCardsToUnlock &&
                    !HeroesManager.Instance.IsHeroReachMaxLevel(i))
                {
                    heroData[i].img_upgrade.gameObject.SetActive(true);
                    heroData[i].img_SliderBG.color = Color.green;
                }
                else
                {
                    heroData[i].img_SliderBG.color = Color.cyan;
                    heroData[i].img_upgrade.gameObject.SetActive(false);
                }
                heroData[i].img_Lock.gameObject.SetActive(false);
            }
            else
            {
                heroData[i].txt_currentHeroLevel.gameObject.SetActive(false);
                heroData[i].txt_CurrentCards.gameObject.SetActive(false);
                heroData[i].txt_RequireCards.gameObject.SetActive(false);
                heroData[i].slider_cards.gameObject.SetActive(false);
                heroData[i].img_SelectedBG.gameObject.SetActive(false);
                heroData[i].img_Lock.gameObject.SetActive(true);
            }
        }
    }


    //SET PLAYER DATA WHEN CLICK 
    public void SetPlayerDataWhenSelect(int _selectionIndex)
    {
        heroData[_selectionIndex].img_SelectedBG.gameObject.SetActive(true);
        txt_SelectedHeroName.text = HeroesManager.Instance.GetHeroName(_selectionIndex);
        txt_HeroDescription.text = HeroesManager.Instance.GetHeroDescription(_selectionIndex);
        txt_SelectHeroHealth.text = HeroesManager.Instance.GetHeroHealth(_selectionIndex).ToString("F0");
        txt_SelectHeroDamage.text = HeroesManager.Instance.GetHeroDamage(_selectionIndex).ToString("F0");
        txt_SelectHeroFirerate.text = HeroesManager.Instance.GetHeroFirerate(_selectionIndex).ToString("F0");
    }


    public void OnClick_SetSelectePlayerData(int _selectionIndex)
    {
        all_Heros[currentluActiveHero].SetActive(false);
        heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(false);

        currentluActiveHero = _selectionIndex;

        all_Heros[currentluActiveHero].SetActive(true);
        heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(true);

        SetPlayerDataWhenSelect(_selectionIndex);
        
    }

    public void OnClick_SelectHero()
    {
        ui_HeroUpgrade.gameObject.SetActive(true);
        ui_HeroUpgrade.SetHeroUpgradeData(currentluActiveHero);
    }

    public void OnClick_ClosePanel()
    {

        UiManager.instance.ui_PlayerManager.SetActiveHero();
        this.gameObject.SetActive(false);
    }

    
}
