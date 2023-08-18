using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AnythingInventoryEquipAndUpgradeUI : MonoBehaviour
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


        txt_EquipmentLevel.text ="Level " + SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentLevel.ToString();


        //when Reach Full level
        if (SlotAnythingManager.instance.all_AnythingInventoryItems[currentItemSelectedIndex].currentLevel == SlotGlovesManager.instance.maxLevel)
        {
            print("Disable update");
            txt_EquipmentLevel.text = "Max Level";
            btn_Upgrade.gameObject.SetActive(false);
           // UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedAnythingItem();
        }


        img_EquipmentIcon.sprite = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].name;


        int currentLevel = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentLevel;

        float upgradeValue = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentFirerate[currentLevel + 1] - SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentFirerate[currentLevel];


        txt_EquipmentCurrentValue.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentFirerate[currentLevel].ToString();
        txt_EquipmentIncreaseValue.text = upgradeValue.ToString("F0");


        int currentMaterials = SlotAnythingManager.instance.currentMaterialCount;
        int requireMaterial = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentLevel];


        txt_UpgradePrice.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel].ToString();

        txt_EquipmentMaterial.text = $"{currentMaterials} / {requireMaterial}";



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isAnythingItemEquipped = true;

        DataManager.instance.SetAnytimeEQState(PlayerSlotManager.instance.isAnythingItemEquipped);

        SlotAnythingManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;

        DataManager.instance.SetAnythingActiveIndex(currentItemSelectedIndex);

        UiManager.instance.ui_PlayerManager.ui_Equipment.Assign_AnythingEquippedItem();


        UiManager.instance.ui_PlayerManager.SetAnythingSlotState();
       


        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotAnythingManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotAnythingManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotAnythingManager.instance.UpgradeEquipnent(currentItemSelectedIndex);

        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
       // UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetAnythingSlotLevelText().text = SlotGlovesManager.instance.all_GlovesInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}

