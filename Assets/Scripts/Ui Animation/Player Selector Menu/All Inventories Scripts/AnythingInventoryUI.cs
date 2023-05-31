using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnythingInventoryUI : MonoBehaviour
{
    [SerializeField] private AnythingInventoryEquipAndUpgradeUI anythingInventoryEquipAndUpgradeUI;

    [SerializeField] private EquipmentPrefabData pf_InventoryButton;
    [SerializeField] private Transform inventoryItemParent; // inventoryItemParent

    private void OnEnable()
    {

        for (int i = 0; i < SlotAnythingManager.instance.all_AnythingInventoryItems.Length; i++)
        {
            if (!SlotAnythingManager.instance.all_AnythingInventoryItems[i].isLocked)
            {
                EquipmentPrefabData obj = Instantiate(pf_InventoryButton, transform.position, Quaternion.identity, inventoryItemParent);
                obj.img_EquipmentIcon.sprite = SlotAnythingManager.instance.all_AnythingInventoryItems[i].sprite;
                obj.txt_EquipmentCurrentLevel.text = SlotAnythingManager.instance.all_AnythingInventoryItems[i].currentLevel.ToString();
                int index = i; // test this with only i
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick_Object(index));
            }
        }
    }

    public void OnClick_Object(int index)
    {
        print(index);
        this.gameObject.SetActive(false);
        anythingInventoryEquipAndUpgradeUI.gameObject.SetActive(true);
        anythingInventoryEquipAndUpgradeUI.SetHeadEquipAndUpgradePanel(index);
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
