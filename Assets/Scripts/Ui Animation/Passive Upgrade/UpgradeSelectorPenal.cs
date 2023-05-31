using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpgradeSelectorPenal : MonoBehaviour
{

    [Header("Require Scripts")]
    [SerializeField] private PassiveUpgradeUI upgradePenal;

    [SerializeField] private Image img_PassiveUpgradeIcon;
    [SerializeField] private TextMeshProUGUI text_PassiveUpgradeName;
    [SerializeField] private TextMeshProUGUI text_PassiveUpgradeLevel;
    [SerializeField] private TextMeshProUGUI text_PassiveUpgradeDesc;
    [SerializeField] private CanvasGroup headingImage;
    [SerializeField] private CanvasGroup continueButton;

    [SerializeField] private float animationDuration = 0.5f;


    // Start is called before the first frame update
    private void Start()
    {
        ResetPassiveUpgradePanelChildAnimation();
    }


    public void SetPassiveUpdateData()
    {
        PassiveUpgradeManager.Instance.IncreasePassiveUpgradeCurrentLevel(UiManager.instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.SelectedPowerupIndex);
        text_PassiveUpgradeLevel.text = "Level " + PassiveUpgradeManager.Instance.GetCurrentPassivesLevel(UiManager.instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.SelectedPowerupIndex).ToString();
        text_PassiveUpgradeName.text = PassiveUpgradeManager.Instance.GetCurrentPassiveUpgradeName(UiManager.instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.SelectedPowerupIndex);
        text_PassiveUpgradeDesc.text = PassiveUpgradeManager.Instance.GetCurrentPassiveUpgradeDesc(UiManager.instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.SelectedPowerupIndex);

        //PassiveUpgradePanelAnimationStart();
    }

    public void ResetPassiveUpgradePanelChildAnimation()
    {
        img_PassiveUpgradeIcon.DOFade(0, 0.01f);
        text_PassiveUpgradeName.DOFade(0, 0.01f);
        text_PassiveUpgradeLevel.DOFade(0, 0.01f);
        text_PassiveUpgradeDesc.DOFade(0, 0.01f);
        continueButton.DOFade(0, 0.01f);
    }

    


}
