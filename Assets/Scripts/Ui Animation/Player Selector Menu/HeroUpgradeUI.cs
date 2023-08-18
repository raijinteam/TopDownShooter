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
    [SerializeField] private TextMeshProUGUI txt_Cards;
    [SerializeField] private TextMeshProUGUI txt_CoinsForUpgrade;
    [SerializeField] private Slider slider_cards;
    [SerializeField] private Image img_SliderBG;
    [SerializeField] private TextMeshProUGUI txt_UpgradeButton;
    [SerializeField] private TextMeshProUGUI txt_HeroPrice;
    [SerializeField] private Button btn_Upgrade;
    [SerializeField] private Image img_UpgradeAvaliable;

    [Header("HP")]
    [SerializeField] private TextMeshProUGUI txt_HeroHealth;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeHealth;

    [Header("Arrmor")]
    [SerializeField] private TextMeshProUGUI txt_HeroArrmor;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeArrmor;


    [Header("Damage")]
    [SerializeField] private TextMeshProUGUI txt_HeroDamage;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeDamage;

    [Header("Firerate")]
    [SerializeField] private TextMeshProUGUI txt_HeroFirerate;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeFirerate;

    [Header("Knockback Force")]
    [SerializeField] private TextMeshProUGUI txt_HeroForce;
    [SerializeField] private TextMeshProUGUI txt_HeroUpgradeForce;

    [Header("Ultimate powerup Data")]
    [SerializeField] private TextMeshProUGUI txt_UltimatePowerupHeader;
    [SerializeField] private UPowerupDetailsSlotUI[] all_UltimatePowerupDetails;






    public void SetHeroUpgradeData(int _selectedIndex)
    {
        currentHeroIndex = _selectedIndex;

        img_SliderBG.color = Color.cyan;


        btn_Upgrade.gameObject.SetActive(true);

        img_UpgradeAvaliable.gameObject.SetActive(false);
        if (HeroesManager.Instance.IsHeroReachMaxLevel(currentHeroIndex))
        {
            txt_CurrentHeroLevel.text = "Max Level";
            btn_Upgrade.gameObject.SetActive(false);
        }
        else if (HeroesManager.Instance.hasEnoughCardsToUpgrade(_selectedIndex))
        {
            img_UpgradeAvaliable.gameObject.SetActive(true);
            img_SliderBG.color = Color.green;
            txt_CurrentHeroLevel.text = "Level " + HeroesManager.Instance.all_HeroData[_selectedIndex].currentLevel.ToString();
        }

        if (HeroesManager.Instance.all_HeroData[currentHeroIndex].isLocked)
        {
            txt_UpgradeButton.text = "Unlock";
            txt_CurrentHeroLevel.text = "Locked";
        }
        else
        {
            txt_UpgradeButton.text = "Upgrade";
            txt_CurrentHeroLevel.text =  HeroesManager.Instance.all_HeroData[_selectedIndex].currentLevel.ToString();
        }

        int currentHeroLevel = HeroesManager.Instance.all_HeroData[_selectedIndex].currentLevel;


        img_HeroImage.sprite = HeroesManager.Instance.all_HeroData[_selectedIndex].heroSprite;
        txt_HeroName.text = HeroesManager.Instance.all_HeroData[_selectedIndex].str_HeroName;

        int currentCards = HeroesManager.Instance.all_HeroData[_selectedIndex].currentCards;
        int requireCards = HeroesManager.Instance.all_HeroData[_selectedIndex].requireCardsToUnlock[currentHeroLevel];

        txt_Cards.text = $"{currentCards} / {requireCards}";

        slider_cards.maxValue = requireCards;
        slider_cards.value = currentCards;

        txt_HeroPrice.text = HeroesManager.Instance.all_HeroData[_selectedIndex].coinsForUpgrade[currentHeroLevel].ToString ();

        //Set Selected Hero ALl States
        SetSelectedPlayerStates(_selectedIndex);
        SetUltimatePowerupData(_selectedIndex);
    }

    private void SetUltimatePowerupData(int _selectedIndex)
    {
        txt_UltimatePowerupHeader.text = HeroesManager.Instance.all_HeroData[_selectedIndex].ultimatePowerupName;

        int powerupCount = HeroesManager.Instance.all_HeroData[_selectedIndex].ultimatePowerupData.Length;
        int currentPlayerLevel = HeroesManager.Instance.all_HeroData[_selectedIndex].currentLevel;
        for(int i = 0; i < powerupCount; i++)
        {
            all_UltimatePowerupDetails[i].gameObject.SetActive(true);
            string powerUpType = HeroesManager.Instance.all_HeroData[_selectedIndex].ultimatePowerupData[i].rageType.ToString();
            float amount = HeroesManager.Instance.all_HeroData[_selectedIndex].ultimatePowerupData[i].amount[currentPlayerLevel];

            all_UltimatePowerupDetails[i].txt_Header.text = powerUpType;
            all_UltimatePowerupDetails[i].txt_Value.text = amount.ToString("F1");
        }
    }


    private void SetSelectedPlayerStates(int _selectdIndex)
    {

        int currentHeroLevel = HeroesManager.Instance.all_HeroData[_selectdIndex].currentLevel;

        float currentHeroHealth = HeroesManager.Instance.all_HeroData[_selectdIndex].flt_MaxHealth[currentHeroLevel];
        float currentHeroDamage = HeroesManager.Instance.all_HeroData[_selectdIndex].flt_Damage[currentHeroLevel];
        float currentHeroArrmor = HeroesManager.Instance.all_HeroData[_selectdIndex].flt_Arrmor[currentHeroLevel];
        float currentHeroFirerate = HeroesManager.Instance.all_HeroData[_selectdIndex].flt_FireRate[currentHeroLevel];
        float currentHeroForce = HeroesManager.Instance.all_HeroData[_selectdIndex].flt_Force[currentHeroLevel];

        txt_HeroHealth.text = currentHeroHealth.ToString("F0");
        txt_HeroDamage.text = currentHeroDamage.ToString("F0");
        txt_HeroArrmor.text = currentHeroArrmor.ToString("F0");
        txt_HeroFirerate.text = currentHeroFirerate.ToString("F0");
        txt_HeroForce.text = currentHeroForce.ToString("F0");


        txt_HeroUpgradeHealth.gameObject.SetActive(false);
        txt_HeroUpgradeDamage.gameObject.SetActive(false);
        txt_HeroUpgradeArrmor.gameObject.SetActive(false);
        txt_HeroUpgradeFirerate.gameObject.SetActive(false);
        txt_HeroUpgradeForce.gameObject.SetActive(false);

        if (HeroesManager.Instance.hasEnoughCardsToUpgrade(_selectdIndex) && !HeroesManager.Instance.all_HeroData[_selectdIndex].isLocked)
        {
            txt_HeroUpgradeHealth.text = HeroesManager.Instance.GetHeroUpgradeHealth(_selectdIndex).ToString("F0");
            txt_HeroUpgradeDamage.text = HeroesManager.Instance.GetHeroUpgradeDamage(_selectdIndex).ToString("F0");
            txt_HeroUpgradeArrmor.text = HeroesManager.Instance.GetHeroUpgradeArrmor(_selectdIndex).ToString("F0");
            txt_HeroUpgradeFirerate.text = HeroesManager.Instance.GetHeroUpgradeFirerate(_selectdIndex).ToString("F0");
            txt_HeroUpgradeForce.text = HeroesManager.Instance.GetHeroUpgradeForce(_selectdIndex).ToString("F0");
        }else if (HeroesManager.Instance.IsHeroReachMaxLevel(_selectdIndex))
        {
            txt_HeroUpgradeHealth.gameObject.SetActive(false);
            txt_HeroUpgradeDamage.gameObject.SetActive(false);
            txt_HeroUpgradeArrmor.gameObject.SetActive(false);
            txt_HeroUpgradeFirerate.gameObject.SetActive(false);
            txt_HeroUpgradeForce.gameObject.SetActive(false);
        }


    }


    public void OnClick_SelectHero()
    {
        for (int i = 0; i < all_UltimatePowerupDetails.Length; i++)
        {
            all_UltimatePowerupDetails[i].gameObject.SetActive(false);
        }
        HeroesManager.Instance.currentActiveSelectedHeroIndex = currentHeroIndex;
        DataManager.instance.SetActiveHeroIndex(currentHeroIndex);
        UiManager.instance.ui_PlayerManager.SetActiveHero();
        UiManager.instance.ui_Home.SetActiveHeroModel();
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
            DataManager.instance.SetHeroLockState(currentHeroIndex, HeroesManager.Instance.all_HeroData[currentHeroIndex].isLocked);
            UiManager.instance.ui_PlayerManager.ui_HeroSelection.SetAllHeroDataInScrollView();
            txt_UpgradeButton.text = "Upgrade";
        }
        else
        {

            int heroLevel = HeroesManager.Instance.all_HeroData[currentHeroIndex].currentLevel;
            int useCardAmount = HeroesManager.Instance.all_HeroData[currentHeroIndex].currentCards - HeroesManager.Instance.all_HeroData[currentHeroIndex].requireCardsToUnlock[heroLevel];

            HeroesManager.Instance.all_HeroData[currentHeroIndex].currentCards = useCardAmount;

            HeroesManager.Instance.all_HeroData[currentHeroIndex].currentLevel++;

            DataManager.instance.SetHeroLevel(currentHeroIndex, HeroesManager.Instance.all_HeroData[currentHeroIndex].currentLevel);
            DataManager.instance.SetHeroCards(currentHeroIndex, HeroesManager.Instance.all_HeroData[currentHeroIndex].currentCards);


            SetHeroUpgradeData(currentHeroIndex);

            //SET PLAYER DATA IN SELECTIO UI
            UiManager.instance.ui_PlayerManager.ui_HeroSelection.SetPlayerDataWhenSelect(currentHeroIndex);
            UiManager.instance.ui_PlayerManager.ui_HeroSelection.SetAllHeroDataInScrollView();

            UiManager.instance.ui_PlayerManager.SetPlayerData(currentHeroIndex);
        }
        for (int i = 0; i < all_UltimatePowerupDetails.Length; i++)
        {
            all_UltimatePowerupDetails[i].gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public void OnClick_ClosePanel()
    {
        for(int i= 0; i < all_UltimatePowerupDetails.Length; i++)
        {
            all_UltimatePowerupDetails[i].gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
