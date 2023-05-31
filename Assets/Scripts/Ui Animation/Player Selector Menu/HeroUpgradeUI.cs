using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroUpgradeUI : MonoBehaviour
{
    private int currentHeroIndex;

    [SerializeField] private Image img_HeroImage;
    [SerializeField] private TextMeshProUGUI txt_HeroName;
    [SerializeField] private TextMeshProUGUI txt_CurrentHeroLevel;
    [SerializeField] private TextMeshProUGUI txt_CurrentHeroCards;
    [SerializeField] private TextMeshProUGUI txt_RequireCardsToUpgrade;
    [SerializeField] private TextMeshProUGUI txt_DescriptionHeading;
    [SerializeField] private TextMeshProUGUI txt_Desctioptin;
    [SerializeField] private TextMeshProUGUI txt_CoinsForUpgrade;
    [SerializeField] private Slider slider_cards;
    [SerializeField] private Image img_SliderBG;
    [SerializeField] private Button btn_Upgrade;
    [SerializeField] private TextMeshProUGUI txt_UpgradeButton;

    [Header("Current Hero State")]
    [SerializeField] private TextMeshProUGUI txt_HeroHealth;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeHealth;
    [SerializeField] private TextMeshProUGUI txt_HeroDamage;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeDamage;
    [SerializeField] private TextMeshProUGUI txt_HeroFirerate;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeFirerate;


    public void SetHeroUpgradeData(int _selectedIndex)
    {
        currentHeroIndex = _selectedIndex;

        txt_HeroUpgradeHealth.gameObject.SetActive(false);
        txt_HeroUpgradeDamage.gameObject.SetActive(false);
        txt_HeroUpgradeFirerate.gameObject.SetActive(false);
        img_SliderBG.color = Color.cyan;


        btn_Upgrade.gameObject.SetActive(true);


        if (HeroesManager.Instance.IsHeroReachMaxLevel(currentHeroIndex))
        {
            txt_CurrentHeroLevel.text = "Max Level";
            txt_HeroUpgradeHealth.gameObject.SetActive(false);
            txt_HeroUpgradeDamage.gameObject.SetActive(false);
            txt_HeroUpgradeFirerate.gameObject.SetActive(false);
            btn_Upgrade.gameObject.SetActive(false);
        }
        else if (HeroesManager.Instance.hasEnoughCardsToUpgrade(_selectedIndex))
        {
            img_SliderBG.color = Color.green;
            txt_HeroUpgradeHealth.gameObject.SetActive(true);
            txt_HeroUpgradeDamage.gameObject.SetActive(true);
            txt_HeroUpgradeFirerate.gameObject.SetActive(true);
            txt_CurrentHeroLevel.text = "Level " + HeroesManager.Instance.all_HeroData[_selectedIndex].currentLevel.ToString();
        }

        if (HeroesManager.Instance.all_HeroData[currentHeroIndex].isLocked)
        {
            txt_UpgradeButton.text = "Unlock";
            txt_CurrentHeroLevel.text = "Level " + HeroesManager.Instance.all_HeroData[_selectedIndex].currentLevel.ToString();
        }


        
        img_HeroImage.sprite = HeroesManager.Instance.all_HeroData[_selectedIndex].heroSprite;
        txt_HeroName.text = HeroesManager.Instance.all_HeroData[_selectedIndex].str_HeroName;
        txt_CurrentHeroCards.text = HeroesManager.Instance.all_HeroData[_selectedIndex].currentCards.ToString();
        txt_RequireCardsToUpgrade.text = HeroesManager.Instance.all_HeroData[_selectedIndex].requireCardsToUnlock.ToString();
        slider_cards.maxValue = HeroesManager.Instance.all_HeroData[_selectedIndex].requireCardsToUnlock;
        slider_cards.value = HeroesManager.Instance.all_HeroData[_selectedIndex].currentCards;
        txt_DescriptionHeading.text = HeroesManager.Instance.all_HeroData[_selectedIndex].str_HeroDescription;

        txt_HeroHealth.text = HeroesManager.Instance.all_HeroData[_selectedIndex].flt_MaxHealth.ToString("F0");
        txt_HeroUpgradeHealth.text = $"(+{HeroesManager.Instance.SetHealthDataVisPer(_selectedIndex).ToString("F0")})";
        txt_HeroDamage.text = HeroesManager.Instance.all_HeroData[_selectedIndex].flt_Damage.ToString("F0");
        txt_HeroUpgradeDamage.text = $"(+{HeroesManager.Instance.SetHeroDamagePer(_selectedIndex).ToString("F0")})";
        txt_HeroFirerate.text = HeroesManager.Instance.all_HeroData[_selectedIndex].flt_FireRate.ToString("F0");
        txt_HeroUpgradeFirerate.text = $"(+{HeroesManager.Instance.SetHeroFireratePer(_selectedIndex).ToString("F0")})";
    }



    public void OnClick_SelectHero()
    {
        HeroesManager.Instance.currentActiveSelectedHeroIndex = currentHeroIndex;
        UiManager.instance.ui_PlayerManager.SetActiveHero();
        this.gameObject.SetActive(false);
        UiManager.instance.ui_PlayerManager.ui_HeroSelection.gameObject.SetActive(false);
        UiManager.instance.ui_PlayerManager.SetPlayerData(currentHeroIndex);
    }

    public void OnClick_UpgradeHero()
    {
        if (!HeroesManager.Instance.hasEnoughCardsToUpgrade(currentHeroIndex))
        {
            print("Not Enough Cards to upgrade");
            return;
        }

        if (!HeroesManager.Instance.hasEnoughCoinsToUpgrade(currentHeroIndex))
        {
            print("Not Enough Coins To upgrade");
            return;
        }

        if (HeroesManager.Instance.all_HeroData[currentHeroIndex].isLocked)
        {

            HeroesManager.Instance.all_HeroData[currentHeroIndex].isLocked = false;
            UiManager.instance.ui_PlayerManager.ui_HeroSelection.SetAllHeroDataInScrollView();
            txt_UpgradeButton.text = "Upgrade";
        }
        else
        {

            HeroesManager.Instance.UpgradeSelectedHero(currentHeroIndex);

            SetHeroUpgradeData(currentHeroIndex);

            //SET PLAYER DATA IN SELECTIO UI
            UiManager.instance.ui_PlayerManager.ui_HeroSelection.SetPlayerDataWhenSelect(currentHeroIndex);
            UiManager.instance.ui_PlayerManager.ui_HeroSelection.SetAllHeroDataInScrollView();

            UiManager.instance.ui_PlayerManager.SetPlayerData(currentHeroIndex);
        }

    }

    public void OnClick_ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
