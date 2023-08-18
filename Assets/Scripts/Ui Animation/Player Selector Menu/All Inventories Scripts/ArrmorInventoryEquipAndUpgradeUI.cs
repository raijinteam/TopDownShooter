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


        txt_EquipmentLevel.text = "Level " + SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentLevel.ToString();

        btn_Upgrade.gameObject.SetActive(true);
        //when Reach Full level
        if (SlotArrmorManager.instance.all_ArrmorInventoryItems[currentItemSelectedIndex].currentLevel == SlotArrmorManager.instance.maxLevel)
        {
            print("Disable update");
            txt_EquipmentLevel.text = "Max Level";
            btn_Upgrade.gameObject.SetActive(false);
            UiManager.instance.ui_PlayerManager.ui_Equipment.CheckIfUpgradeAvailableForEquippedArrmorItem();
        }

        int currentLevel = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentLevel;


        img_EquipmentIcon.sprite = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].name;


        float upgradeValue = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentHealth[currentLevel + 1] - SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentHealth[currentLevel];


        txt_EquipmentCurrentValue.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentHealth[currentLevel].ToString();
        txt_EquipmentIncreaseValue.text = upgradeValue.ToString("F0");



        int currentMaterial = SlotArrmorManager.instance.currentMaterialCount;
        int requireMaterial = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].currentLevel];

        txt_UpgradePrice.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel].ToString();


        txt_EquipmentMaterial.text = $"{currentMaterial} / {requireMaterial}";



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isArrmorItemEquipped = true;
        DataManager.instance.SetArrmorEQState(PlayerSlotManager.instance.isArrmorItemEquipped);


        SlotArrmorManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;
        DataManager.instance.SetArrmorActiveIndex(currentItemSelectedIndex); ;
       
        UiManager.instance.ui_PlayerManager.ui_Equipment.Assign_ArrmorEquippedItem();

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
       // UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetArrmorSlotLevelText().text = SlotArrmorManager.instance.all_ArrmorInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
