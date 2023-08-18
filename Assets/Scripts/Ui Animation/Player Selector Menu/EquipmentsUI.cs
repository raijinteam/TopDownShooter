using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EquipmentsUI : MonoBehaviour
{
    [Header("Head Slot")]
    [SerializeField] private RectTransform slot_Empty;
    [SerializeField] private RectTransform slot_Fill;
    [SerializeField] private Image img_HeadItemAvaliable;
    [SerializeField] private Image img_HeadUpgradeAvaliable;
    [SerializeField] private Image img_HeadIcon;
    [SerializeField] private TextMeshProUGUI txt_CurrentLevel;
    [SerializeField] private GameObject headEquipmentScrollView;

    [Header("Gun Slot")]
    [SerializeField] private RectTransform slot_GunEmpty;
    [SerializeField] private RectTransform slot_GunFill;
    [SerializeField] private Image img_GunItemAvaliable;
    [SerializeField] private Image img_GunUpgradeAvaliable;
    [SerializeField] private Image img_GunIcon;
    [SerializeField] private TextMeshProUGUI txt_GunCurrentLevel;
    [SerializeField] private GameObject gunEquipmentScrollView;


    [Header("Arrmor Slot")]
    [SerializeField] private RectTransform slot_ArrmorEmpty;
    [SerializeField] private RectTransform slot_ArrmorFill;
    [SerializeField] private Image img_ArrmorItemAvaliable;
    [SerializeField] private Image img_ArrmorUpgradeAvaliable;
    [SerializeField] private Image img_ArrmorIcon;
    [SerializeField] private TextMeshProUGUI txt_ArrmorCurrentLevel;
    [SerializeField] private GameObject arrmorEquipmentScrollView;


    [Header("Glove Slot")]
    [SerializeField] private RectTransform slot_GloveEmpty;
    [SerializeField] private RectTransform slot_GloveFill;
    [SerializeField] private Image img_GloveItemAvaliable;
    [SerializeField] private Image img_GloveUpgradeAvaliable;
    [SerializeField] private Image img_GlveIcon;
    [SerializeField] private TextMeshProUGUI txt_GloveCurrentLevel;
    [SerializeField] private GameObject gloveEquipmentScrollView;


    [Header("Anything Slot")]
    [SerializeField] private RectTransform slot_AnythingEmpty;
    [SerializeField] private RectTransform slot_AnythingFill;
    [SerializeField] private Image img_AnythingItemAvaliable;
    [SerializeField] private Image img_AnythingUpgradeAvaliable;
    [SerializeField] private Image img_AnythingIcon;
    [SerializeField] private TextMeshProUGUI txt_AnythingCurrentLevel;
    [SerializeField] private GameObject anythingEquipmentScrollView;


    [Header("Ablities Slot")]
    [SerializeField] private RectTransform slot_AblitiesEmpty;
    [SerializeField] private RectTransform slot_AblitiesFill;
    [SerializeField] private Image img_AblitiesItemAvaliable;
    [SerializeField] private Image img_AblitiesUpgradeAvaliable;
    [SerializeField] private Image img_AblitiesIcon;
    [SerializeField] private TextMeshProUGUI txt_AblitiesCurrentLevel;
    [SerializeField] private GameObject ablitiesEquipmentScrollView;


    private void OnEnable()
    {
        //CHECK HEAD ITEM EQUPPED
        if (PlayerSlotManager.instance.isHeadItemEquipped)
        {
            Assign_HeadEquippedItem(); // AssignEquippedItem();
        }
        else
        {
            CheckIfWeHaveAnyHeadItemAvailable(); // CheckIfWeHaveAnyHeadItemAvailable
        }

        //CHECK GUN ITEM EQUPPED
        if (PlayerSlotManager.instance.isGunItemEquipped)
        {
            Assign_GunEquippedItem(); // AssignEquippedItem();
        }
        else
        {
            CheckIfWeHaveAnyGunItemAvailable(); // CheckIfWeHaveAnyHeadItemAvailable
        }

        //CHECK ARRMOR ITEM EQUPPED
        if (PlayerSlotManager.instance.isArrmorItemEquipped)
        {
            Assign_ArrmorEquippedItem(); // AssignEquippedItem();
        }
        else
        {
            CheckIfWeHaveAnyArrmorItemAvailable(); // CheckIfWeHaveAnyHeadItemAvailable
        }

        ////CHECK GLOVES ITEM EQUPPED
        if (PlayerSlotManager.instance.isGlovesItemEquipped)
        {
            Assign_GlovesEquippedItem(); // AssignEquippedItem();
        }
        else
        {
            CheckIfWeHaveAnyGlovesItemAvailable(); // CheckIfWeHaveAnyHeadItemAvailable
        }

        ////CHECK ANYTHING ITEM EQUPPED
        if (PlayerSlotManager.instance.isAnythingItemEquipped)
        {
            Assign_AnythingEquippedItem(); // AssignEquippedItem();
        }
        else
        {
            CheckIfWeHaveAnyAnythingItemAvailable(); // CheckIfWeHaveAnyHeadItemAvailable
        }

        ////CHECK ABLITIES ITEM EQUPPED
        if (PlayerSlotManager.instance.isAblitiesItemEquipped)
        {
            Assign_AbilitiesEquippedItem(); // AssignEquippedItem();
        }
        else
        {
            CheckIfWeHaveAnyAbilitiesItemAvailable(); // CheckIfWeHaveAnyHeadItemAvailable
        }
    }


    #region All Head Items


    public void Assign_HeadEquippedItem()
    {
        slot_Empty.gameObject.SetActive(false);
        slot_Fill.gameObject.SetActive(true);
        img_HeadIcon.sprite = SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex].sprite;
        txt_CurrentLevel.text = "LV. " + SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex]
            .currentLevel.ToString();


        // Check For Updgrade
        CheckIfUpgradeAvailableForEquippedHeadItem();
    }

    public void CheckIfUpgradeAvailableForEquippedHeadItem()
    {
        // IF CURRENT ITEM LEVEL IS GREATER OR EQUAL TO MAX LEVEL


        if (!SlotHeadEquipmentManager.instance.hasEnoughMaterialsForUpgrade(SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex) ||
            (SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex].currentLevel == SlotHeadEquipmentManager.instance.maxLevel))
        {
            img_HeadUpgradeAvaliable.gameObject.SetActive(false);
        }
        else
        {
            img_HeadUpgradeAvaliable.gameObject.SetActive(true);
        }
    }


    public void CheckIfWeHaveAnyHeadItemAvailable()
    {
        slot_Empty.gameObject.SetActive(true);
        img_HeadItemAvaliable.gameObject.SetActive(false);

        for (int i = 0; i < SlotHeadEquipmentManager.instance.all_HeadInventory.Length; i++)
        {
            if (!SlotHeadEquipmentManager.instance.all_HeadInventory[i].isLocked)
            {
                img_HeadItemAvaliable.gameObject.SetActive(true);
                break;
            }
        }
    }

    #endregion



    #region All Guns Items

    public void Assign_GunEquippedItem()
    {
        slot_GunEmpty.gameObject.SetActive(false);
        slot_GunFill.gameObject.SetActive(true);
        img_GunIcon.sprite = SlotGunsManager.instance.all_GunInventoryItems[SlotGunsManager.instance.currentEquippmentSelectedIndex].sprite;
        txt_GunCurrentLevel.text = "LV. " + SlotGunsManager.instance.all_GunInventoryItems[SlotGunsManager.instance.currentEquippmentSelectedIndex]
            .currentLevel.ToString();


        // Check For Updgrade
        CheckIfUpgradeAvailableForEquippedGunItem();
    }





    public void CheckIfUpgradeAvailableForEquippedGunItem()
    {
        // IF CURRENT ITEM LEVEL IS GREATER OR EQUAL TO MAX LEVEL


        if (!SlotGunsManager.instance.hasEnoughMaterialsForUpgrade(SlotGunsManager.instance.currentEquippmentSelectedIndex) ||
            (SlotGunsManager.instance.all_GunInventoryItems[SlotGunsManager.instance.currentEquippmentSelectedIndex].currentLevel == SlotGunsManager.instance.maxLevel))
        {
            img_GunUpgradeAvaliable.gameObject.SetActive(false);
        }
        else
        {
            img_GunUpgradeAvaliable.gameObject.SetActive(true);
        }
    }
    public void CheckIfWeHaveAnyGunItemAvailable()
    {
        img_GunItemAvaliable.gameObject.SetActive(false);
        slot_GunEmpty.gameObject.SetActive(true);

        for (int i = 0; i < SlotGunsManager.instance.all_GunInventoryItems.Length; i++)
        {
            if (!SlotGunsManager.instance.all_GunInventoryItems[i].isLocked)
            {
                img_GunItemAvaliable.gameObject.SetActive(true);
                break;
            }
        }

    }

    #endregion


    #region All Arrmor Items


    public void Assign_ArrmorEquippedItem()
    {
        slot_ArrmorFill.gameObject.SetActive(true);
        slot_ArrmorEmpty.gameObject.SetActive(false);
        img_ArrmorIcon.sprite = SlotArrmorManager.instance.all_ArrmorInventoryItems[SlotArrmorManager.instance.currentEquippmentSelectedIndex].sprite;
        txt_ArrmorCurrentLevel.text = "LV. " + SlotArrmorManager.instance.all_ArrmorInventoryItems[SlotArrmorManager.instance.currentEquippmentSelectedIndex]
            .currentLevel.ToString();

        // Check For Updgrade
        CheckIfUpgradeAvailableForEquippedArrmorItem();
    }





    public void CheckIfUpgradeAvailableForEquippedArrmorItem()
    {
        // IF CURRENT ITEM LEVEL IS GREATER OR EQUAL TO MAX LEVEL


        if (!SlotArrmorManager.instance.hasEnoughMaterialsForUpgrade(SlotArrmorManager.instance.currentEquippmentSelectedIndex) ||
            (SlotArrmorManager.instance.all_ArrmorInventoryItems[SlotArrmorManager.instance.currentEquippmentSelectedIndex].currentLevel == SlotArrmorManager.instance.maxLevel))
        {
            img_ArrmorUpgradeAvaliable.gameObject.SetActive(false);
        }
        else
        {
            img_ArrmorUpgradeAvaliable.gameObject.SetActive(true);
        }
    }
    public void CheckIfWeHaveAnyArrmorItemAvailable()
    {
        img_ArrmorItemAvaliable.gameObject.SetActive(false);
        slot_ArrmorEmpty.gameObject.SetActive(true);

        for (int i = 0; i < SlotArrmorManager.instance.all_ArrmorInventoryItems.Length; i++)
        {
            if (!SlotArrmorManager.instance.all_ArrmorInventoryItems[i].isLocked)
            {
                img_ArrmorItemAvaliable.gameObject.SetActive(true);
                break;
            }
        }
    }



    #endregion



    #region All GLoves Items 


    public void Assign_GlovesEquippedItem()
    {
        slot_GloveEmpty.gameObject.SetActive(false);
        slot_GloveFill.gameObject.SetActive(true);
        img_GlveIcon.sprite = SlotGlovesManager.instance.all_GlovesInventoryItems[SlotGlovesManager.instance.currentEquippmentSelectedIndex].sprite;
        txt_GloveCurrentLevel.text = "LV. " + SlotGlovesManager.instance.all_GlovesInventoryItems[SlotGlovesManager.instance.currentEquippmentSelectedIndex]
            .currentLevel.ToString();
        txt_GloveCurrentLevel.gameObject.SetActive(true);



        // Check For Updgrade
        CheckIfUpgradeAvailableForEquippedGlovesItem();
    }





    public void CheckIfUpgradeAvailableForEquippedGlovesItem()
    {
        // IF CURRENT ITEM LEVEL IS GREATER OR EQUAL TO MAX LEVEL


        if (!SlotGlovesManager.instance.hasEnoughMaterialsForUpgrade(SlotGlovesManager.instance.currentEquippmentSelectedIndex) ||
            (SlotGlovesManager.instance.all_GlovesInventoryItems[SlotGlovesManager.instance.currentEquippmentSelectedIndex].currentLevel == SlotGlovesManager.instance.maxLevel))
        {
            img_GloveUpgradeAvaliable.gameObject.SetActive(false);
        }
        else
        {
            img_GloveUpgradeAvaliable.gameObject.SetActive(true);
        }
    }
    public void CheckIfWeHaveAnyGlovesItemAvailable()
    {
        img_GloveItemAvaliable.gameObject.SetActive(false);
        slot_GloveEmpty.gameObject.SetActive(true);

        for (int i = 0; i < SlotGlovesManager.instance.all_GlovesInventoryItems.Length; i++)
        {
            if (!SlotGlovesManager.instance.all_GlovesInventoryItems[i].isLocked)
            {
                img_GloveItemAvaliable.gameObject.SetActive(true);
                break;
            }
        }
    }

    #endregion



    #region All Anythinng Items


    public void Assign_AnythingEquippedItem()
    {
        slot_AnythingEmpty.gameObject.SetActive(false);
        slot_AnythingFill.gameObject.SetActive(true);
        img_AnythingIcon.sprite = SlotAnythingManager.instance.all_AnythingInventoryItems[SlotAnythingManager.instance.currentEquippmentSelectedIndex].sprite;
        txt_AnythingCurrentLevel.text = "LV. " + SlotAnythingManager.instance.all_AnythingInventoryItems[SlotAnythingManager.instance.currentEquippmentSelectedIndex]
            .currentLevel.ToString();
        txt_AnythingCurrentLevel.gameObject.SetActive(true);


        // Check For Updgrade
        CheckIfUpgradeAvailableForEquippedAnythingItem();
    }





    public void CheckIfUpgradeAvailableForEquippedAnythingItem()
    {
        // IF CURRENT ITEM LEVEL IS GREATER OR EQUAL TO MAX LEVEL


        if (!SlotAnythingManager.instance.hasEnoughMaterialsForUpgrade(SlotAnythingManager.instance.currentEquippmentSelectedIndex) ||
            (SlotAnythingManager.instance.all_AnythingInventoryItems[SlotAnythingManager.instance.currentEquippmentSelectedIndex].currentLevel == SlotAnythingManager.instance.maxLevel))
        {
            img_AnythingUpgradeAvaliable.gameObject.SetActive(false);
        }
        else
        {
            img_AnythingUpgradeAvaliable.gameObject.SetActive(true);
        }
    }
    public void CheckIfWeHaveAnyAnythingItemAvailable()
    {
        img_AnythingItemAvaliable.gameObject.SetActive(false);
        slot_AnythingEmpty.gameObject.SetActive(true);

        for (int i = 0; i < SlotAnythingManager.instance.all_AnythingInventoryItems.Length; i++)
        {
            if (!SlotAnythingManager.instance.all_AnythingInventoryItems[i].isLocked)
            {
                img_AnythingItemAvaliable.gameObject.SetActive(true);
                break;
            }
        }
    }


    #endregion


    #region All Abilities Items

    public void Assign_AbilitiesEquippedItem()
    {
        slot_AblitiesEmpty.gameObject.SetActive(false);
        slot_AblitiesFill.gameObject.SetActive(true);

        img_AblitiesIcon.sprite = SlotAblitiesManager.instance.all_AbilitesInventoryItems[SlotAblitiesManager.instance.currentEquippmentSelectedIndex].sprite;
        txt_AblitiesCurrentLevel.text = "LV. " + SlotAblitiesManager.instance.all_AbilitesInventoryItems[SlotAblitiesManager.instance.currentEquippmentSelectedIndex]
            .currentLevel.ToString();
        txt_AblitiesCurrentLevel.gameObject.SetActive(true);

        // Check For Updgrade
        CheckIfUpgradeAvailableForEquippedAbilitiesItem();
    }





    public void CheckIfUpgradeAvailableForEquippedAbilitiesItem()
    {
        // IF CURRENT ITEM LEVEL IS GREATER OR EQUAL TO MAX LEVEL


        if (!SlotAblitiesManager.instance.hasEnoughMaterialsForUpgrade(SlotAblitiesManager.instance.currentEquippmentSelectedIndex) ||
            (SlotAblitiesManager.instance.all_AbilitesInventoryItems[SlotAblitiesManager.instance.currentEquippmentSelectedIndex].currentLevel == SlotAblitiesManager.instance.maxLevel))
        {
            img_AblitiesUpgradeAvaliable.gameObject.SetActive(false);
        }
        else
        {
            img_AblitiesUpgradeAvaliable.gameObject.SetActive(true);
        }
    }
    public void CheckIfWeHaveAnyAbilitiesItemAvailable()
    {
        img_AblitiesItemAvaliable.gameObject.SetActive(false);
        slot_AnythingEmpty.gameObject.SetActive(true);

        for (int i = 0; i < SlotAblitiesManager.instance.all_AbilitesInventoryItems.Length; i++)
        {
            if (!SlotAblitiesManager.instance.all_AbilitesInventoryItems[i].isLocked)
            {
                img_AblitiesItemAvaliable.gameObject.SetActive(true);
                break;
            }
        }
    }

    #endregion



    public void OnClick_OpenHeadSlot()
    {
        headEquipmentScrollView.SetActive(true);
    }

    public void OnClick_OpenGunsSlot()
    {
        gunEquipmentScrollView.SetActive(true);
    }

    public void OnClick_OpenArrmoSlot()
    {
        arrmorEquipmentScrollView.SetActive(true);
    }

    public void OnClick_OpenGlovesSlot()
    {
        gloveEquipmentScrollView.SetActive(true);
    }

    public void OnClick_OpenAbilitesSlot()
    {
        ablitiesEquipmentScrollView.SetActive(true);
    }

    public void OnClick_OpenAnythigSlot()
    {
        anythingEquipmentScrollView.SetActive(true);
    }
}
