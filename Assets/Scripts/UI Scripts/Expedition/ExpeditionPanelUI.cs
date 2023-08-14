using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionPanelUI : MonoBehaviour
{
    public int panelIndex;
    public int expeditionStateIndex;

    public ExpeditionItemUI[] all_ExpeditionItem;

    public ExpeditionSelectionUI ui_ExpeditionSelection;
    public ExpeditionProcressUI ui_Expedition;


    private void OnEnable()
    {
        if(UiManager.instance.ui_Home.ui_Expedition[panelIndex].expeditionState == ExpeditionState.running)
        {
            ui_Expedition.gameObject.SetActive(true);
            ui_ExpeditionSelection.gameObject.SetActive(false);
            ui_Expedition.index = panelIndex;
        }
        else
        {
            ui_ExpeditionSelection.gameObject.SetActive(true);
            ui_Expedition.gameObject.SetActive(false);
        }
    }


    public void OnClick_StartExpedition(int index)
    {
        DataManager.instance.SetExpeditionState(panelIndex, ExpeditionState.running);
        UiManager.instance.ui_Home.ui_Expedition[panelIndex].expeditionState =(ExpeditionState) DataManager.instance.GetExpeditionState(panelIndex);
        UiManager.instance.ui_Home.ui_Expedition[panelIndex].SetExpeditionPanels();
        TimeCalculation.instance.expeditionTimer[panelIndex] = all_ExpeditionItem[index].time;
        expeditionStateIndex = index;
        this.gameObject.SetActive(false);
    }

    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}
