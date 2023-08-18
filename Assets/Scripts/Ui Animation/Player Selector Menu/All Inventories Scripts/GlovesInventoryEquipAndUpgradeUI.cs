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

        txt_EquipmentLevel.text = "Level " + SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentLevel.ToString();
        btn_Upgrade.gameObject.SetActive(true);
        //when Reach Full level
        if (SlotGlovesManager.instance.all_GlovesInventoryItems[currentItemSelectedIndex].currentLevel == SlotGlovesManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            txt_EquipmentLevel.text = "Max Level";
        //    UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedGlovesItem();
        }


        img_EquipmentIcon.sprite = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].name;


        int currentLevel = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentLevel;

        float upgradeValue = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentDamage[currentLevel + 1] - SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentDamage[currentLevel];

        txt_EquipmentCurrentValue.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentDamage[currentLevel].ToString();
        txt_EquipmentIncreaseValue.text = upgradeValue.ToString("F0");


        int currentMaterial = SlotGlovesManager.instance.currentMaterialCount;
        int requireMaterial = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].currentLevel];


        txt_UpgradePrice.text = SlotGlovesManager.instance.all_GlovesInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel].ToString();


        txt_EquipmentMaterial.text = $"{currentMaterial} / {requireMaterial}";



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isGlovesItemEquipped = true;

        DataManager.instance.SetGlovesEQState(PlayerSlotManager.instance.isGlovesItemEquipped);

        SlotGlovesManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;

        DataManager.instance.SetGloveActiveIndex(currentItemSelectedIndex);

        UiManager.instance.ui_PlayerManager.ui_Equipment.Assign_GlovesEquippedItem();


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
       // UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetGloveSlotLevelText().text = SlotGlovesManager.instance.all_GlovesInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
