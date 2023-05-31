using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunInventoryUI : MonoBehaviour
{
    [SerializeField] private GunInventoryEquipAndUpgradeUI gunInventoryEquipAndUpgradeUI;

    [SerializeField] private EquipmentPrefabData pf_InventoryButton;
    [SerializeField] private Transform inventoryItemParent; // inventoryItemParent

    private void OnEnable()
    {

        for (int i = 0; i < SlotGunsManager.instance.all_GunInventoryItems.Length; i++)
        {
            if (!SlotGunsManager.instance.all_GunInventoryItems[i].isLocked)
            {
                EquipmentPrefabData obj = Instantiate(pf_InventoryButton, transform.position, Quaternion.identity, inventoryItemParent);
                obj.img_EquipmentIcon.sprite = SlotGunsManager.instance.all_GunInventoryItems[i].sprite;
                obj.txt_EquipmentCurrentLevel.text = SlotGunsManager.instance.all_GunInventoryItems[i].currentLevel.ToString();
                int index = i; // test this with only i
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick_Object(index));
            }
        }
    }

    public void OnClick_Object(int index)
    {
        this.gameObject.SetActive(false);
        gunInventoryEquipAndUpgradeUI.gameObject.SetActive(true);
        gunInventoryEquipAndUpgradeUI.SetHeadEquipAndUpgradePanel(index);
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
