using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitesInventoryEquipAndUpgradeUI : MonoBehaviour
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



        txt_EquipmentLevel.text = "Level " + SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentLevel.ToString();



        //when Reach Full level
        if (SlotAblitiesManager.instance.all_AbilitesInventoryItems[currentItemSelectedIndex].currentLevel == SlotAblitiesManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            txt_EquipmentLevel.text = "Max Level";
            //UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedAbilitiesItem();
        }


        img_EquipmentIcon.sprite = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].name;


        int currentLevel = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentLevel;

        float upgradeValue = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentFirerate[currentLevel + 1] - SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentFirerate[currentLevel];


        txt_EquipmentCurrentValue.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentFirerate[currentLevel].ToString();
        txt_EquipmentIncreaseValue.text = upgradeValue.ToString("F0");


        int currentMaterial = SlotAblitiesManager.instance.currentMaterialCount;
        int requireMaterial = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentLevel];


        txt_UpgradePrice.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel].ToString();


        txt_EquipmentMaterial.text = $"{currentMaterial} / {requireMaterial}";



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isAblitiesItemEquipped = true;

        DataManager.instance.SetAblitiesEQState(PlayerSlotManager.instance.isAblitiesItemEquipped);

        SlotAblitiesManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;
        DataManager.instance.SetAblititesActiveIndex(currentItemSelectedIndex);

        UiManager.instance.ui_PlayerManager.ui_Equipment.Assign_AbilitiesEquippedItem();

        UiManager.instance.ui_PlayerManager.SetAbilitiesSlotState();

        


        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotAblitiesManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotAblitiesManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotAblitiesManager.instance.UpgradeEquipnent(currentItemSelectedIndex);


        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
       // UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetAbilitiesSlotLevelText().text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
