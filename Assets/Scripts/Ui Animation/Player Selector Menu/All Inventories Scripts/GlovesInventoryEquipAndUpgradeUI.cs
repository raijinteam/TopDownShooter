using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlovesInventoryEquipAndUpgradeUI : MonoBehaviour
{
    private int currentItemSelectedIndex;


    public Image img_EquipmentIcon;
    public TextMeshProUGUI txt_EquipmentName;
    public TextMeshProUGUI txt_EquipmentCurrentLevel;
    public TextMeshProUGUI txt_EquipmentMaxLevel;
    public TextMeshProUGUI txt_EquipmentCurrentValue;
    public TextMeshProUGUI txt_EquipmentIncreaseValue;
    public Button btn_Upgrade;


    [Header("Materials Property")]
    [SerializeField] private Image img_EquipmentMaterialIcon;
    public TextMeshProUGUI txt_EquipmentcurrentMaterial;
    public TextMeshProUGUI txt_EquipmentRequireMaterial;


    public void SetHeadEquipAndUpgradePanel(int _itemIndex)
    {
        currentItemSelectedIndex = _itemIndex;

        btn_Upgrade.gameObject.SetActive(true);
        //when Reach Full level
        if (SlotGlovesManager.instance.all_GlovesInventoryItems[currentItemSelectedIndex].currentLevel == SlotGlovesManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedGlovesItem();
        }


        img_EquipmentIcon.sprite = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].name;
        txt_EquipmentCurrentLevel.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentLevel.ToString();
        txt_EquipmentMaxLevel.text = SlotGlovesManager.instance.maxLevel.ToString();
        txt_EquipmentCurrentValue.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentDamage.ToString();
        txt_EquipmentIncreaseValue.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].damageIncrease.ToString();

        txt_EquipmentcurrentMaterial.text = SlotGlovesManager.instance.currentMaterialCount.ToString();
        txt_EquipmentRequireMaterial.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentLevel].ToString();



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isGlovesItemEquipped = true;
        SlotGlovesManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.Assign_GlovesEquippedItem();


        UiManager.instance.ui_PlayerManager.SetGlovesSlotState();
       


        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotGlovesManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotGlovesManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotGlovesManager.instance.UpgradeEquipnent(currentItemSelectedIndex);


        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetGloveSlotLevelText().text = SlotGlovesManager.instance.all_GlovesInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
