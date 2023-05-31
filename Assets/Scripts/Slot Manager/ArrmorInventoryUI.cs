using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrmorInventoryUI : MonoBehaviour
{
    [SerializeField] private ArrmorInventoryEquipAndUpgradeUI arrmorInventoryEquipAndUpgradeUI;

    [SerializeField] private EquipmentPrefabData pf_InventoryButton;
    [SerializeField] private Transform inventoryItemParent; // inventoryItemParent

    private void OnEnable()
    {

        for (int i = 0; i < SlotArrmorManager.instance.all_ArrmorInventoryItems.Length; i++)
        {
            if (!SlotArrmorManager.instance.all_ArrmorInventoryItems[i].isLocked)
            {
                EquipmentPrefabData obj = Instantiate(pf_InventoryButton, transform.position, Quaternion.identity, inventoryItemParent);
                obj.img_EquipmentIcon.sprite = SlotArrmorManager.instance.all_ArrmorInventoryItems[i].sprite;
                obj.txt_EquipmentCurrentLevel.text = SlotArrmorManager.instance.all_ArrmorInventoryItems[i].currentLevel.ToString();
                int index = i; // test this with only i
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick_Object(index));
            }
        }
    }

    public void OnClick_Object(int index)
    {
        print(index);
        this.gameObject.SetActive(false);
        arrmorInventoryEquipAndUpgradeUI.gameObject.SetActive(true);
        arrmorInventoryEquipAndUpgradeUI.SetHeadEquipAndUpgradePanel(index);
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
