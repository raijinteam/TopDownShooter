using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ExpeditionState
{
    start,
    running,
    finished
}


public class ExpeditionUI : MonoBehaviour
{
    public int index;
    public ExpeditionState expeditionState;
    [SerializeField] private RectTransform panel_ExpeditionStart;
    [SerializeField] private RectTransform panel_ExpeditionRunning;
    [SerializeField] private RectTransform panel_ExpeditionFinished;

    [SerializeField] private TextMeshProUGUI txt_Timer;


    private void Update()
    {

        if (expeditionState == ExpeditionState.running)
        {
            ExpeditionTimer((int)TimeCalculation.instance.currentExpeditionTimer[index]);
        }

    }

    public void SetExpeditionState()
    {
        expeditionState = (ExpeditionState)DataManager.instance.GetExpeditionState(index);
        SetExpeditionPanels();
    }

    public void ExpeditionTimer(int _timeLeft)
    {
        float hours = _timeLeft / 3600;
        float minutes = (_timeLeft % 3600) / 60;
        float seconds = _timeLeft % 60;


        txt_Timer.text = $"{hours} : {minutes} : {seconds}";
    }


    public void SetExpeditionPanels()
    {
        if (expeditionState == ExpeditionState.start)
        {
            panel_ExpeditionStart.gameObject.SetActive(true);
            panel_ExpeditionRunning.gameObject.SetActive(false);
            panel_ExpeditionFinished.gameObject.SetActive(false);
        }
        else if (expeditionState == ExpeditionState.running)
        {
            panel_ExpeditionStart.gameObject.SetActive(false);
            panel_ExpeditionRunning.gameObject.SetActive(true);
            panel_ExpeditionFinished.gameObject.SetActive(false);
        }
        else if (expeditionState == ExpeditionState.finished)
        {
            panel_ExpeditionStart.gameObject.SetActive(false);
            panel_ExpeditionRunning.gameObject.SetActive(false);
            panel_ExpeditionFinished.gameObject.SetActive(true);
        }
    }


    public void OnClick_StartExpedition()
    {

        if(DataManager.instance.isTutorialPlaying && index == 0)
        {
            UiManager.instance.ui_tutorial.tutorialState = TutorialState.ExpeditionSelect;
            UiManager.instance.ui_ExpeditionPanel[index].gameObject.SetActive(true);
            UiManager.instance.ui_ExpeditionPanel[index].panelIndex = index;
        }
        else if (!DataManager.instance.isTutorialPlaying)
        {
            UiManager.instance.ui_ExpeditionPanel[index].gameObject.SetActive(true);
            UiManager.instance.ui_ExpeditionPanel[index].panelIndex = index;
        }

    }

    public void OnClick_ExpeditionRunning()
    {
       

        UiManager.instance.ui_ExpeditionPanel[index].gameObject.SetActive(true);
    }

    public void OnClick_FinishedExpedition()
    {
        if (DataManager.instance.isTutorialPlaying)
        {
            UiManager.instance.ui_tutorial.tutorialState = TutorialState.OpenPlayerSelect;
        }

        expeditionState = ExpeditionState.start;
        SetExpeditionPanels();
        DataManager.instance.SetExpeditionState(index, expeditionState);
    }


}
