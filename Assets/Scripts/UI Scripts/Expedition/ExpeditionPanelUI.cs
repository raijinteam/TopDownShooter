using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionPanelUI : MonoBehaviour
{
    public int panelIndex;
    public int expeditionStateIndex;

    public ExpeditionItemUI[] all_ExpeditionItem;
    public Expediton2ItemUI[] all_ExpeditionItems;

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

    public void OnClick_StateExpedition2(int index)
    {
        TimeCalculation.instance.expeditionTimer[panelIndex] = all_ExpeditionItems[index].time;
        TimeCalculation.instance.currentExpeditionTimer[panelIndex] = all_ExpeditionItems[index].time;
        DataManager.instance.SetExpeditionState(panelIndex, ExpeditionState.running);
        UiManager.instance.ui_Home.ui_Expedition[panelIndex].expeditionState = (ExpeditionState)DataManager.instance.GetExpeditionState(panelIndex);
        UiManager.instance.ui_Home.ui_Expedition[panelIndex].SetExpeditionPanels();
        expeditionStateIndex = index;
        this.gameObject.SetActive(false);
    }

    public void OnClick_StartExpedition(int index)
    {
        Debug.Log("Index is : " + index);
        if(DataManager.instance.isTutorialPlaying && index == 3)
        {
            TimeCalculation.instance.expeditionTimer[panelIndex] = all_ExpeditionItem[index].time;
            TimeCalculation.instance.currentExpeditionTimer[panelIndex] = all_ExpeditionItem[index].time;
            DataManager.instance.SetExpeditionState(panelIndex, ExpeditionState.running);
            UiManager.instance.ui_Home.ui_Expedition[panelIndex].expeditionState = (ExpeditionState)DataManager.instance.GetExpeditionState(panelIndex);
            Debug.Log("Expedition State : " + DataManager.instance.GetExpeditionState(panelIndex));
            UiManager.instance.ui_Home.ui_Expedition[panelIndex].SetExpeditionPanels();
            Debug.Log("Set timer : " + all_ExpeditionItem[index].time);
            expeditionStateIndex = index;
            ui_Expedition.gameObject.SetActive(true);
            ui_ExpeditionSelection.gameObject.SetActive(false);
            UiManager.instance.ui_tutorial.tutorialState = TutorialState.ExpeditionComplete;

        }
        else if (!DataManager.instance.isTutorialPlaying)
        {
            TimeCalculation.instance.expeditionTimer[panelIndex] = all_ExpeditionItem[index].time;
            TimeCalculation.instance.currentExpeditionTimer[panelIndex] = all_ExpeditionItem[index].time;
            DataManager.instance.SetExpeditionState(panelIndex, ExpeditionState.running);
            UiManager.instance.ui_Home.ui_Expedition[panelIndex].expeditionState = (ExpeditionState)DataManager.instance.GetExpeditionState(panelIndex);
            Debug.Log("Expedition State : " + DataManager.instance.GetExpeditionState(panelIndex));
            UiManager.instance.ui_Home.ui_Expedition[panelIndex].SetExpeditionPanels();
            Debug.Log("Set timer : " + all_ExpeditionItem[index].time);
            expeditionStateIndex = index;
            this.gameObject.SetActive(false);
        }


       
    }

    public void OnClick_Close()
    {
        if (!DataManager.instance.isTutorialPlaying)
        {

        this.gameObject.SetActive(false);
        }
    }
}
