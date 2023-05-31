using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerManagerUI : MonoBehaviour
{

    // Change the name of the script to : PlayerManagerUI

    [Header("Sub Scripts")]
    public EquipmentsSlotUI ui_EquipmentSlots;
    public HeadInventoryUI ui_InvetoryView;
    public HeroSelectionUI ui_HeroSelection;

    [SerializeField] private GameObject[] all_HerosModel;

    [Header("Current Player State")]
    [SerializeField] private TextMeshProUGUI txt_Health;
    [SerializeField] private TextMeshProUGUI txt_Damage;
    [SerializeField] private TextMeshProUGUI txt_Firerate;

    private int characterIndex = 0;
    private int headItemIndex = 0; // increases health
    private int gunItemIndex = 0;
    private int arrmorItemIndex = 0;
    private int gloveItemIndex = 0;
    private int anythigItemIndex = 0;
    private int abilitiesItemIndex = 0;

    /// <summary>
    // TEMPORARAYYY
    [Header("Temporary")]
    public GameObject panel_Upgrade;


    private void OnEnable()
    {
        SetActiveHero();
    }


    private void Start()
    {
        panel_Upgrade.SetActive(true);

        SetPlayerData(characterIndex);
    }

    public void SetActiveHero()
    {
        for(int i =0; i < all_HerosModel.Length; i++){

            if(i == HeroesManager.Instance.currentActiveSelectedHeroIndex)
            {
                all_HerosModel[HeroesManager.Instance.currentActiveSelectedHeroIndex].SetActive(true);
            }
            else
            {
                all_HerosModel[i].SetActive(false);

            }
        }
    }

    public void SetPlayerData(int _heroIndex)
    {
        if(PlayerSlotManager.instance.isHeadItemEquipped || PlayerSlotManager.instance.isGunItemEquipped ||
            PlayerSlotManager.instance.isArrmorItemEquipped || PlayerSlotManager.instance.isGlovesItemEquipped ||
            PlayerSlotManager.instance.isAnythingItemEquipped || PlayerSlotManager.instance.isAblitiesItemEquipped)
        {
            SetHeadState();

            SetGunSlotState();

            SetArrmorSlotState();

            SetGlovesSlotState();

            SetAnythingSlotState();

            SetAbilitiesSlotState();
        }
        else
        {
            txt_Health.text = HeroesManager.Instance.GetHeroHealth(_heroIndex).ToString("F0");
            txt_Damage.text = HeroesManager.Instance.GetHeroDamage(_heroIndex).ToString("F0");
            txt_Firerate.text = HeroesManager.Instance.GetHeroFirerate(_heroIndex).ToString("F0");
        }
    }

    #region All SlotsSetting Data


    public void SetHeadState()
    {
        if (!PlayerSlotManager.instance.isHeadItemEquipped)
        {
            return;
        }


        // Calculate health only
        // calculate base health
        float baseHealth = HeroesManager.Instance.GetHeroHealth(HeroesManager.Instance.currentActiveSelectedHeroIndex);
        float headSlotHealthBonus = SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex].currentHealth;

        if (PlayerSlotManager.instance.isArrmorItemEquipped)
        {
            headSlotHealthBonus += SlotArrmorManager.instance.all_ArrmorInventoryItems[SlotArrmorManager.instance.currentEquippmentSelectedIndex].currentHealth;
        }

        float totalHealth = baseHealth + headSlotHealthBonus;
        txt_Health.text = totalHealth.ToString("F0");

    }

    public void SetGunSlotState()
    {
        if (!PlayerSlotManager.instance.isGunItemEquipped)
        {
            return;
        }

        float baseDamage = HeroesManager.Instance.GetHeroDamage(HeroesManager.Instance.currentActiveSelectedHeroIndex);
        float headSlotDamageBonus = SlotGunsManager.instance.all_GunInventoryItems[SlotGunsManager.instance.currentEquippmentSelectedIndex].currentDamage;

        


        if (PlayerSlotManager.instance.isGlovesItemEquipped)
        {
            headSlotDamageBonus += SlotGlovesManager.instance.all_GlovesInventoryItems[SlotGlovesManager.instance.currentEquippmentSelectedIndex].currentDamage;
        }

        float totalDamage = baseDamage + headSlotDamageBonus;

        txt_Damage.text = totalDamage.ToString("F0");
    }

    public void SetArrmorSlotState()
    {
        if (!PlayerSlotManager.instance.isArrmorItemEquipped)
        {
            return;
        }

        float baseHealth = HeroesManager.Instance.GetHeroHealth(HeroesManager.Instance.currentActiveSelectedHeroIndex);
        float arrmorSlotHealthBonus = SlotArrmorManager.instance.all_ArrmorInventoryItems[SlotArrmorManager.instance.currentEquippmentSelectedIndex].currentHealth;

        if (PlayerSlotManager.instance.isHeadItemEquipped)
        {
            arrmorSlotHealthBonus += SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex].currentHealth;
        }

        float totalHealth = baseHealth + arrmorSlotHealthBonus;

        txt_Health.text = totalHealth.ToString("F0");
    }

    public void SetGlovesSlotState()
    {

        if (!PlayerSlotManager.instance.isGlovesItemEquipped)
        {
            return;
        }

        float baseDamage = HeroesManager.Instance.GetHeroDamage(HeroesManager.Instance.currentActiveSelectedHeroIndex);
        float headSlotDamageBonus = SlotGlovesManager.instance.all_GlovesInventoryItems[SlotGlovesManager.instance.currentEquippmentSelectedIndex].currentDamage;


        if (PlayerSlotManager.instance.isGunItemEquipped)
        {
            headSlotDamageBonus += SlotGunsManager.instance.all_GunInventoryItems[SlotGlovesManager.instance.currentEquippmentSelectedIndex].currentDamage;
        }


        float totalDamage = baseDamage + headSlotDamageBonus;

        txt_Damage.text = totalDamage.ToString("F0");
    }

    public void SetAnythingSlotState()
    {

        if (!PlayerSlotManager.instance.isAnythingItemEquipped)
        {
            return;
        }

        float baseFirerate = HeroesManager.Instance.GetHeroFirerate(HeroesManager.Instance.currentActiveSelectedHeroIndex);
        float anythingSlotFirerateBonus = SlotAnythingManager.instance.all_AnythingInventoryItems[SlotAnythingManager.instance.currentEquippmentSelectedIndex].currentFirerate;

        if (PlayerSlotManager.instance.isAblitiesItemEquipped)
        {
            anythingSlotFirerateBonus += SlotAblitiesManager.instance.all_AbilitesInventoryItems[SlotAblitiesManager.instance.currentEquippmentSelectedIndex].currentFirerate;
        }

        float totalFirearate = baseFirerate + anythingSlotFirerateBonus;
        txt_Firerate.text = totalFirearate.ToString("F0");
    }

    public void SetAbilitiesSlotState()
    {

        if (!PlayerSlotManager.instance.isAblitiesItemEquipped)
        {
            return;
        }

        float baseFirerate = HeroesManager.Instance.GetHeroFirerate(HeroesManager.Instance.currentActiveSelectedHeroIndex);
        float abilitiesSlotFirerateBonus = SlotAblitiesManager.instance.all_AbilitesInventoryItems[SlotAblitiesManager.instance.currentEquippmentSelectedIndex].currentFirerate;

        if (PlayerSlotManager.instance.isAnythingItemEquipped)
        {
            abilitiesSlotFirerateBonus += SlotAnythingManager.instance.all_AnythingInventoryItems[SlotAnythingManager.instance.currentEquippmentSelectedIndex].currentFirerate;
        }

        float totalFirearate = baseFirerate + abilitiesSlotFirerateBonus;
        txt_Firerate.text = totalFirearate.ToString("F0");
    }


    #endregion

    public void OnClick_OpenHeroSelectionPanel()
    {
        for(int i =0; i < all_HerosModel.Length; i++)
        {
            all_HerosModel[i].SetActive(false);
        }
        ui_HeroSelection.gameObject.SetActive(true);
    }

    public void OnClick_OpenInventoryPanel()
    {
        //inventoryPenal.transform.DOScale(new Vector3(1, 1, 1), openMenuAnimationDuration).SetEase(Ease.OutBounce);
    }


    public void OnOpenInventoryDetailsPenalButtonCallbak()
    {
        //inventoryObjectDetailsPenal.transform.DOScale(new Vector3(1, 1, 1), .01f).SetEase(Ease.InBounce);
    }


    public void OnClick_PlayerStatePanel()
    {
        //playerStatePenal.transform.DOScale(new Vector3(1, 1, 1), openMenuAnimationDuration).SetEase(Ease.OutBounce);
    }

}
