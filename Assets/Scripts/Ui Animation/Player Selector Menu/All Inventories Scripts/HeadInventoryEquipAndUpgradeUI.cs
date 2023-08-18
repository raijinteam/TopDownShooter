using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HeadInventoryEquipAndUpgradeUI : MonoBehaviour
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


        txt_EquipmentLevel.text = "Level " + SlotHeadEquipmentManager.instance.all_HeadInventory[currentItemSelectedIndex].currentLevel;
        //when Reach Full level
        if (SlotHeadEquipmentManager.instance.all_HeadInventory[currentItemSelectedIndex].currentLevel == SlotHeadEquipmentManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            txt_EquipmentLevel.text = "Max Level";
          //  UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedHeadItem();
        }


        img_EquipmentIcon.sprite = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].sprite;
        txt_EquipmentName.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].name;

        int currentLevel = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentLevel;
        int maxLevel = SlotHeadEquipmentManager.instance.maxLevel;
        // txt_EquipmentLevel.text = $"{currentLevel} / {maxLevel}";


        float upgradeValue = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentHealth[currentLevel + 1] - SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentHealth[currentLevel];

        txt_EquipmentCurrentValue.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentHealth[currentLevel].ToString();
        txt_EquipmentIncreaseValue.text = upgradeValue.ToString("F0");

        int currentMaterial = SlotHeadEquipmentManager.instance.currentMaterialCount;
        int requireMaterial = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex]
            .requireMaterialToLevelUp[SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentLevel];

        txt_UpgradePrice.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].requireCoinsToUpgrade[currentLevel].ToString();

        txt_EquipmentMaterial.text = $"{currentMaterial} / {requireMaterial}";



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isHeadItemEquipped = true;
        DataManager.instance.SetHeadEQState(PlayerSlotManager.instance.isHeadItemEquipped);

        SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;

        DataManager.instance.SetHeadActiveIndex(SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex);

        UiManager.instance.ui_PlayerManager.ui_Equipment.Assign_HeadEquippedItem();
        UiManager.instance.ui_PlayerManager.SetHeadState();

        
       

        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotHeadEquipmentManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotHeadEquipmentManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotHeadEquipmentManager.instance.UpgradeEquipnent(currentItemSelectedIndex);


        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
        this.gameObject.SetActive(false);

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
