using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TutorialState { 

    Welcome,
    ExpeditionOpen,
    ExpeditionSelect,
    ExpeditionComplete,
    ExpeditionRewardClaim,
    OpenPlayerSelect,
    OpenAllPlayer,
    FirstPlayerDetails,
    UpgradePlayer,
    TutorialComplete
}

public class TutorialUI : MonoBehaviour
{

    public TutorialState tutorialState;


    [SerializeField] private RectTransform panel_Welcome;
    [SerializeField] private RectTransform panel_ExpeditionOpen;
    [SerializeField] private RectTransform panel_ExpeditionSelect;
    [SerializeField] private RectTransform panel_ExpeditionComplete;
    [SerializeField] private RectTransform panel_ExpeditionRewardClaim;
    [SerializeField] private RectTransform panel_OpenPlayerSelect;
    [SerializeField] private RectTransform panel_AllPlayer;
    [SerializeField] private RectTransform panel_OpenFirstPlayerDetails;
    [SerializeField] private RectTransform panel_UpgradePlayer;
    [SerializeField] private RectTransform panel_TutorialComplete;



    private void Update()
    {
        if(tutorialState == TutorialState.Welcome)
        {
            panel_Welcome.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                tutorialState = TutorialState.ExpeditionOpen;

            }


        }
        else  if(tutorialState == TutorialState.ExpeditionOpen)
        {
            panel_Welcome.gameObject.SetActive(false);
            panel_ExpeditionOpen.gameObject.SetActive(true);
        }
        else if(tutorialState == TutorialState.ExpeditionSelect)
        {
            panel_ExpeditionOpen.gameObject.SetActive(false);
            panel_ExpeditionSelect.gameObject.SetActive(true);
        }
        else if (tutorialState == TutorialState.ExpeditionComplete)
        {
            panel_ExpeditionSelect.gameObject.SetActive(false);
            panel_ExpeditionComplete.gameObject.SetActive(true);
        }else if(tutorialState == TutorialState.ExpeditionRewardClaim)
        {
            panel_ExpeditionComplete.gameObject.SetActive(false);
            panel_ExpeditionRewardClaim.gameObject.SetActive(true);
        }
        else if (tutorialState == TutorialState.OpenPlayerSelect)
        {
            panel_ExpeditionRewardClaim.gameObject.SetActive(false);
            panel_OpenPlayerSelect.gameObject.SetActive(true);
        }
        else if (tutorialState == TutorialState.OpenAllPlayer)
        {
            panel_OpenPlayerSelect.gameObject.SetActive(false);
            panel_AllPlayer.gameObject.SetActive(true);
        }
        else if (tutorialState == TutorialState.FirstPlayerDetails)
        {
            panel_AllPlayer.gameObject.SetActive(false);
            panel_OpenFirstPlayerDetails.gameObject.SetActive(true);
        }
        else if (tutorialState == TutorialState.UpgradePlayer)
        {
            panel_OpenFirstPlayerDetails.gameObject.SetActive(false);
            panel_UpgradePlayer.gameObject.SetActive(true);
        }
        else if (tutorialState == TutorialState.TutorialComplete)
        {
            panel_UpgradePlayer.gameObject.SetActive(false);
            panel_TutorialComplete.gameObject.SetActive(true);
        }
    }
}
