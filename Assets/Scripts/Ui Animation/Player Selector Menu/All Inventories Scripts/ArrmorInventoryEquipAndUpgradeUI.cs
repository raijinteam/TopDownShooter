using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrmorInventoryEquipAndUpgradeUI : MonoBehaviour
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
        if (SlotArrmorManager.instance.all_ArrmorInventoryItems[currentItemSelectedIndex].currentLevel == SlotArrmorManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedArrmorItem();
        }


        img_EquipmentIcon.sprite = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].name;
        txt_EquipmentCurrentLevel.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentLevel.ToString();
        txt_EquipmentMaxLevel.text = SlotArrmorManager.instance.maxLevel.ToString();
        txt_EquipmentCurrentValue.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentHealth.ToString();
        txt_EquipmentIncreaseValue.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].healthIncrease.ToString();

        txt_EquipmentcurrentMaterial.text = SlotArrmorManager.instance.currentMaterialCount.ToString();
        txt_EquipmentRequireMaterial.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentLevel].ToString();



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isArrmorItemEquipped = true;
        SlotArrmorManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.Assign_ArrmorEquippedItem();

        UiManager.instance.ui_PlayerManager.SetArrmorSlotState();



        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotArrmorManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotArrmorManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotArrmorManager.instance.UpgradeEquipnent(currentItemSelectedIndex);




        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetArrmorSlotLevelText().text = SlotArrmorManager.instance.all_ArrmorInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
