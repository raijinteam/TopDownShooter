using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunInventoryEquipAndUpgradeUI : MonoBehaviour
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
        if (SlotGunsManager.instance.all_GunInventoryItems[currentItemSelectedIndex].currentLevel == SlotGunsManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedGunItem();
        }


        img_EquipmentIcon.sprite = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].name;
        txt_EquipmentCurrentLevel.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentLevel.ToString();
        txt_EquipmentMaxLevel.text = SlotGunsManager.instance.maxLevel.ToString();
        txt_EquipmentCurrentValue.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentDamage.ToString();
        txt_EquipmentIncreaseValue.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].damageIncrease.ToString();

        txt_EquipmentcurrentMaterial.text = SlotGunsManager.instance.currentMaterialCount.ToString();
        txt_EquipmentRequireMaterial.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentLevel].ToString();



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isGunItemEquipped = true;

        SlotGunsManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.Assign_GunEquippedItem();

        UiManager.instance.ui_PlayerManager.SetGunSlotState();


        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotGunsManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotGunsManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotGunsManager.instance.UpgradeEquipnent(currentItemSelectedIndex);

        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetGunSlotLevelText().text = SlotGunsManager.instance.all_GunInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
