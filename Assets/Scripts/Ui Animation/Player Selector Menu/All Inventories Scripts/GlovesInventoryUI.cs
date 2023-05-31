using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlovesInventoryUI : MonoBehaviour
{
    [SerializeField] private GlovesInventoryEquipAndUpgradeUI glovesInventoryEquipAndUpgradeUI;

    [SerializeField] private EquipmentPrefabData pf_InventoryButton;
    [SerializeField] private Transform inventoryItemParent; // inventoryItemParent

    private void OnEnable()
    {

        for (int i = 0; i < SlotGlovesManager.instance.all_GlovesInventoryItems.Length; i++)
        {
            if (!SlotGlovesManager.instance.all_GlovesInventoryItems[i].isLocked)
            {
                EquipmentPrefabData obj = Instantiate(pf_InventoryButton, transform.position, Quaternion.identity, inventoryItemParent);
                obj.img_EquipmentIcon.sprite = SlotGlovesManager.instance.all_GlovesInventoryItems[i].sprite;
                obj.txt_EquipmentCurrentLevel.text = SlotGlovesManager.instance.all_GlovesInventoryItems[i].currentLevel.ToString();
                int index = i; // test this with only i
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick_Object(index));
            }
        }
    }

    public void OnClick_Object(int index)
    {
        this.gameObject.SetActive(false);
        glovesInventoryEquipAndUpgradeUI.gameObject.SetActive(true);
        glovesInventoryEquipAndUpgradeUI.SetHeadEquipAndUpgradePanel(index);
    }

    private void OnDisable()
    {
        int childCount = inventoryItemParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(inventoryItemParent.transform.GetChild(i).gameObject);
        }
    }

    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
