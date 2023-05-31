using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PassiveUpgradeSelectionUI : MonoBehaviour
{

    [SerializeField] private Button btn_Upgrade;
    [SerializeField] private float animationDuration = .2f;
    [SerializeField] private float flt_PassiveUpgradeRewardedAnimationDuration = .2f;
    [SerializeField] private float flt_RandomSelectionEffectTime = 1.5f;

    [SerializeField] private GameObject[] all_PassiveUpgrades;

    [SerializeField] private Image[] all_PassiveSelectionBG;
    [SerializeField] private TextMeshProUGUI[] all_PassiveUpgradeLevel;


    public int selectedPowerupIndex;

    private List<int> list_AvaliablePassiveUpgrade = new List<int>();


    private void OnEnable()
    {
        for (int i = 0; i < all_PassiveSelectionBG.Length; i++)
        {
            all_PassiveSelectionBG[i].gameObject.SetActive(false);
        }
    }


    //RANDOM SELECTION EFFECT 
    private IEnumerator SetRandomPassiveUpgradeSelectionEffect()
    {
        int randomLoopCount = Random.Range(10, 15);

        int previousIndex = 0;

        UiManager.instance.CanChangeMenus = false;

        for (int i = 0; i < randomLoopCount; i++)
        {
            selectedPowerupIndex = Random.Range(0, list_AvaliablePassiveUpgrade.Count);
            all_PassiveSelectionBG[previousIndex].gameObject.SetActive(false);
            all_PassiveSelectionBG[selectedPowerupIndex].gameObject.SetActive(true);
            previousIndex = selectedPowerupIndex;
            yield return new WaitForSeconds(0.1f);
        }

        list_AvaliablePassiveUpgrade.Clear();

        UiManager.instance.ui_PassiveUpgrade.ui_PassiveRewardInfo.SetRewardPanelInfo(selectedPowerupIndex);
        Sequence seq = DOTween.Sequence();
        seq.Append(all_PassiveUpgrades[selectedPowerupIndex].transform.DOScale(1.2f, flt_PassiveUpgradeRewardedAnimationDuration).SetEase(Ease.Linear))
                    .Append(all_PassiveUpgrades[selectedPowerupIndex].transform.DOScale(1, flt_PassiveUpgradeRewardedAnimationDuration).SetEase(Ease.Linear)).SetLoops(5).
                    OnComplete(ShowRewardPanel);
    }

    //ACTIVE REWARDED PANEL
    private void ShowRewardPanel()
    {
        UiManager.instance.CanChangeMenus = true;
        this.gameObject.SetActive(false);
        UiManager.instance.ui_PassiveUpgrade.ui_PassiveRewardInfo.gameObject.SetActive(true);
        btn_Upgrade.interactable = true;
    }

    //CHECK IF PASSIVE UPGRADE IS UP MAX PASSIVE LEVEL THEN SKIP THAT UPGRADE
    private void CheckForUpgradeMaxLevel()
    {
        for (int i = 0; i < all_PassiveSelectionBG.Length; i++)
        {
            if (PassiveUpgradeManager.Instance.GetCurrentPassivesLevel(i) < PassiveUpgradeManager.Instance.maxPassiveLevel)
            {
                list_AvaliablePassiveUpgrade.Add(i);
            }
        }

        // if everything upgraded, code here


    }

    //SET LEVEL TEXT IN PASSIVE UPGRADE MENU 
    public void SetPassiveUpgradeLevelText()
    {
        for (int i = 0; i < all_PassiveUpgradeLevel.Length; i++)
        {
            all_PassiveUpgradeLevel[i].text = PassiveUpgradeManager.Instance.GetCurrentPassivesLevel(i).ToString();
        }
    }

    public int SelectedPowerupIndex
    {
        get
        {
            return selectedPowerupIndex;
        }
    }

    public Image[] AllGlowBG
    {
        get
        {
            return all_PassiveSelectionBG;
        }
    }

    //CALLBACK FUNCTION WHEN CLICK UPGRADE BUTTON IN PASSIVE UPGRADE
    public void OnClick_UpgradeButton()
    {

        CheckForUpgradeMaxLevel();

        btn_Upgrade.interactable = false;

        StartCoroutine(SetRandomPassiveUpgradeSelectionEffect());

        Sequence seq = DOTween.Sequence();
        seq.Append(btn_Upgrade.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), animationDuration).SetEase(Ease.OutSine)).
                    Append(btn_Upgrade.transform.DOScale(new Vector3(1f, 1f, 1f), animationDuration).SetEase(Ease.InSine));
    }

}
