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
    public TextMeshProUGUI txt_EquipmentLevel;
    public TextMeshProUGUI txt_EquipmentCurrentValue;
    public TextMeshProUGUI txt_EquipmentIncreaseValue;
    public Button btn_Upgrade;
    public TextMeshProUGUI txt_UpgradePrice;


    [Header("Materials Property")]
    [SerializeField] private Image img_EquipmentMaterialIcon;
    public TextMeshProUGUI txt_EquipmentMaterial;


    public void SetHeadEquipAndUpgradePanel(int _itemIndex)
    {
        currentItemSelectedIndex = _itemIndex;

        btn_Upgrade.gameObject.SetActive(true);

        txt_EquipmentLevel.text = "Level " + SlotGunsManager.instance.all_GunInventoryItems[currentItemSelectedIndex].currentLevel;

        //when Reach Full level
        if (SlotGunsManager.instance.all_GunInventoryItems[currentItemSelectedIndex].currentLevel == SlotGunsManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            txt_EquipmentLevel.text = "Max Level";
        //    UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedGunItem();
        }


        img_EquipmentIcon.sprite = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].name;


        int currentLevel = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentLevel;

        float upgradeValue = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentDamage[currentLevel + 1] - SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentDamage[currentLevel];

        txt_EquipmentCurrentValue.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentDamage[currentLevel].ToString();
        txt_EquipmentIncreaseValue.text = upgradeValue.ToString("F0");


        int currentMaterial = SlotGunsManager.instance.currentMaterialCount;
        int requireMaterial = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].currentLevel];


        txt_UpgradePrice.text = SlotGunsManager.instance.all_GunInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel].ToString();


        txt_EquipmentMaterial.text = $"{currentMaterial} / {requireMaterial}";


    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isGunItemEquipped = true;

        DataManager.instance.SetGunEQState(PlayerSlotManager.instance.isGunItemEquipped);

        SlotGunsManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;

        DataManager.instance.SetGunActiveIndex(SlotGunsManager.instance.currentEquippmentSelectedIndex);

        UiManager.instance.ui_PlayerManager.ui_Equipment.Assign_GunEquippedItem();

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
     //   UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetGunSlotLevelText().text = SlotGunsManager.instance.all_GunInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
