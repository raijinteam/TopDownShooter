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

    [Header("BG")]
    [SerializeField] private Sprite sprite_BGCommon;
    [SerializeField] private Sprite sprite_BGRare;
    [SerializeField] private Sprite sprite_BGEpic;
    [SerializeField] private Color color_GlowCommon;
    [SerializeField] private Color color_GlowRare;
    [SerializeField] private Color color_GlowEpic;



    [Header("Selected Hero Data")]
    [SerializeField] private TextMeshProUGUI txt_SelectedHeroName;
    [SerializeField] private TextMeshProUGUI txt_HeroDescription;
    [SerializeField] private TextMeshProUGUI txt_SelectHeroHealth;
    [SerializeField] private TextMeshProUGUI txt_SelectHeroDamage;
    [SerializeField] private TextMeshProUGUI txt_SelectHeroFirerate;
    [SerializeField] private Image img_UnclockAvaliable;

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
        for (int i = 0; i < all_Heros.Length; i++)
        {
            all_Heros[i].SetActive(false);
        }
    }

    //SET ACTIVE PLAYER DATA WHEN ENABLE
    private void SetCurrentlyActiveHeroData()
    {
        currentluActiveHero = HeroesManager.Instance.currentActiveSelectedHeroIndex;
        int activeHeroLevel = HeroesManager.Instance.all_HeroData[currentluActiveHero].currentLevel;
        all_Heros[currentluActiveHero].SetActive(true);
        heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(true);
        txt_SelectedHeroName.text = HeroesManager.Instance.GetHeroName(currentluActiveHero);
        txt_HeroDescription.text = HeroesManager.Instance.GetHeroDescription(currentluActiveHero);

        float currentHeroHealth = HeroesManager.Instance.all_HeroData[currentluActiveHero].flt_MaxHealth[activeHeroLevel];
        float currentHeroDamage = HeroesManager.Instance.all_HeroData[currentluActiveHero].flt_Damage[activeHeroLevel];
        float currentHeroFirerate = HeroesManager.Instance.all_HeroData[currentluActiveHero].flt_FireRate[activeHeroLevel];

        txt_SelectHeroHealth.text = currentHeroHealth.ToString("F0");
        txt_SelectHeroDamage.text = currentHeroDamage.ToString("F0");
        txt_SelectHeroFirerate.text = currentHeroFirerate.ToString("F0");
    }

    //SET ALL PLAYER DATA IN SCROLL VIEW
    public void SetAllHeroDataInScrollView()
    {

        SetPlayerDataWhenSelect(HeroesManager.Instance.currentActiveSelectedHeroIndex);

        for (int i = 0; i < heroData.Length; i++)
        {

            SetPlayerBG(i);


            heroData[i].img_Hero.sprite = HeroesManager.Instance.all_HeroData[i].heroSprite;

            int currentHeroLevel = HeroesManager.Instance.all_HeroData[i].currentLevel;
            int currentCards = HeroesManager.Instance.all_HeroData[i].currentCards;
            int requireCards = HeroesManager.Instance.all_HeroData[i].requireCardsToUnlock[currentHeroLevel];

            heroData[i].txt_currentHeroLevel.text = "Level " + HeroesManager.Instance.all_HeroData[i].currentLevel.ToString();
            heroData[i].slider_cards.maxValue = requireCards;
            heroData[i].slider_cards.value = currentCards;
            heroData[i].txt_Cards.text = $"{currentCards} / {requireCards}";

            if (!HeroesManager.Instance.all_HeroData[i].isLocked)
            {
                heroData[i].txt_currentHeroLevel.gameObject.SetActive(true);
                heroData[i].img_Lock.gameObject.SetActive(false);


                if (HeroesManager.Instance.all_HeroData[i].currentCards >= HeroesManager.Instance.all_HeroData[i].requireCardsToUnlock[currentHeroLevel] &&
                    !HeroesManager.Instance.IsHeroReachMaxLevel(i))
                {
                    heroData[i].img_upgradeAvaliable.gameObject.SetActive(true);
                    heroData[i].img_SliderBG.color = Color.green;
                }
                else
                {
                    heroData[i].img_SliderBG.color = Color.cyan;
                    heroData[i].img_upgradeAvaliable.gameObject.SetActive(false);
                }
            }
            else
            {
                heroData[i].img_upgradeAvaliable.gameObject.SetActive(false);
                heroData[i].txt_currentHeroLevel.gameObject.SetActive(false);
                heroData[i].img_Lock.gameObject.SetActive(true);
                heroData[i].img_SelectedBG.gameObject.SetActive(false);
            }



        }
    }




    //THis is for Testing only method
    public void IncreaseCards(int index)
    {
        SetAllHeroDataInScrollView();
    }

    private void SetPlayerBG(int index)
    {
        if (HeroesManager.Instance.all_HeroData[index].heroType == HeroType.Common)
        {
            heroData[index].SetPlayerBGForItsType(sprite_BGCommon, color_GlowCommon);
        }
        else if (HeroesManager.Instance.all_HeroData[index].heroType == HeroType.Rare)
        {
            heroData[index].SetPlayerBGForItsType(sprite_BGRare, color_GlowRare);
        }
        else
        {
            heroData[index].SetPlayerBGForItsType(sprite_BGEpic, color_GlowEpic);
        }
    }

    //SET PLAYER DATA WHEN CLICK 
    public void SetPlayerDataWhenSelect(int _selectionIndex)
    {

        if (HeroesManager.Instance.all_HeroData[_selectionIndex].isLocked)
        {
            btn_Selection.gameObject.SetActive(false);
        }
        else
        {
            btn_Selection.gameObject.SetActive(true);
        }

        if (HeroesManager.Instance.hasEnoughCardsToUpgrade(_selectionIndex))
        {
            img_UnclockAvaliable.gameObject.SetActive(true);
        }
        else
        {
            img_UnclockAvaliable.gameObject.SetActive(false);
        }


        int activeHeroLevel = HeroesManager.Instance.all_HeroData[_selectionIndex].currentLevel;

        heroData[_selectionIndex].img_SelectedBG.gameObject.SetActive(true);
        txt_SelectedHeroName.text = HeroesManager.Instance.GetHeroName(_selectionIndex);
        txt_HeroDescription.text = HeroesManager.Instance.GetHeroDescription(_selectionIndex);


        float currentHeroHealth = HeroesManager.Instance.all_HeroData[currentluActiveHero].flt_MaxHealth[activeHeroLevel];
        float currentHeroDamage = HeroesManager.Instance.all_HeroData[currentluActiveHero].flt_Damage[activeHeroLevel];
        float currentHeroFirerate = HeroesManager.Instance.all_HeroData[currentluActiveHero].flt_FireRate[activeHeroLevel];

        txt_SelectHeroHealth.text = currentHeroHealth.ToString("F0");
        txt_SelectHeroDamage.text = currentHeroDamage.ToString("F0");
        txt_SelectHeroFirerate.text = currentHeroFirerate.ToString("F0");
    }


    public void OnClick_SetSelectePlayerData(int _selectionIndex)
    {
        if(DataManager.instance.isTutorialPlaying && _selectionIndex == 0)
        {
            all_Heros[currentluActiveHero].SetActive(false);
            heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(false);

            currentluActiveHero = _selectionIndex;
            all_Heros[currentluActiveHero].SetActive(true);
            heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(true);

            SetPlayerDataWhenSelect(_selectionIndex);
        }else if (!DataManager.instance.isTutorialPlaying)
        {
            all_Heros[currentluActiveHero].SetActive(false);
            heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(false);

            currentluActiveHero = _selectionIndex;
            all_Heros[currentluActiveHero].SetActive(true);
            heroData[currentluActiveHero].img_SelectedBG.gameObject.SetActive(true);

            SetPlayerDataWhenSelect(_selectionIndex);
        }

        
    }

    public void OnClick_SelectHero()
    {
        if (DataManager.instance.isTutorialPlaying)
            return;

        HeroesManager.Instance.currentActiveSelectedHeroIndex = currentluActiveHero;
        DataManager.instance.SetActiveHeroIndex(currentluActiveHero);
        UiManager.instance.ui_PlayerManager.SetActiveHero();
        UiManager.instance.ui_Home.SetActiveHeroModel();
        this.gameObject.SetActive(false);
        UiManager.instance.ui_PlayerManager.ui_HeroSelection.gameObject.SetActive(false);
        UiManager.instance.ui_PlayerManager.SetPlayerData(currentluActiveHero);
        UiManager.instance.ui_Navbar.gameObject.SetActive(true);
    }

    public void OnClick_OpenSelectedPlayerInfo()
    {
        if (!DataManager.instance.isTutorialPlaying || currentluActiveHero == 0)
        {
            if (DataManager.instance.isTutorialPlaying)
            {
                UiManager.instance.ui_tutorial.tutorialState = TutorialState.UpgradePlayer;
            }
            ui_HeroUpgrade.gameObject.SetActive(true);
            ui_HeroUpgrade.SetHeroUpgradeData(currentluActiveHero);
        }
    }

    public void OnClick_ClosePanel()
    {
        if (!DataManager.instance.isTutorialPlaying)
        {
            UiManager.instance.ui_PlayerManager.SetActiveHero();
            UiManager.instance.ui_Navbar.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }


}
