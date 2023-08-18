using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommonPanelUI : MonoBehaviour
{
    [SerializeField] private Image img_UserProgress;
    [SerializeField] private TextMeshProUGUI txt_UserLevel;


    [SerializeField] private TextMeshProUGUI txt_Skipits;
    [SerializeField] private TextMeshProUGUI txt_Energy;
    [SerializeField] private TextMeshProUGUI txt_Gold;
    [SerializeField] private TextMeshProUGUI txt_Gems;
    [SerializeField] private float lerpTimer = 0.5f;

    private int currentCoins;
    private int currentGems;
    private int currentEnergy;
    private int currentSkipits;


    public void SetCommonPanelData()
    {

        txt_UserLevel.text = DataManager.instance.GetUserLevel().ToString();

        currentCoins = DataManager.instance.coins;
        txt_Gold.text = currentCoins.ToString();

        currentGems = DataManager.instance.gems;
        txt_Gems.text = currentGems.ToString();

        currentEnergy = DataManager.instance.energy;
        txt_Energy.text = currentEnergy.ToString();

        currentSkipits = DataManager.instance.skipits;
        txt_Skipits.text = currentSkipits.ToString();
    }

    private void Update()
    {
        if(currentCoins != DataManager.instance.coins)
        {
            StartCoroutine(CurrencyAnimation(currentCoins, DataManager.instance.coins, txt_Gold));
        }
        if (currentGems != DataManager.instance.gems)
        {
            StartCoroutine(CurrencyAnimation(currentGems, DataManager.instance.gems, txt_Gems));
        }
        if (currentEnergy != DataManager.instance.energy)
        {
            StartCoroutine(CurrencyAnimation(currentEnergy, DataManager.instance.energy, txt_Energy));
        }
        if (currentSkipits != DataManager.instance.skipits)
        {
            StartCoroutine(CurrencyAnimation(currentSkipits, DataManager.instance.skipits, txt_Skipits));
        }
    }

    public IEnumerator CurrencyAnimation(float currentCurrency , float updateCurrency , TextMeshProUGUI currencyText)
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / lerpTimer;
            currentCurrency = Mathf.Lerp(currentCurrency, updateCurrency, currentTime);
            currencyText.text = currentCurrency.ToString("F0");
            yield return null;
        }
    }


    public void SetUserCurrentXP()
    {
        float progress = DataManager.instance.userCurrentXP / DataManager.instance.userRequireXP;
        img_UserProgress.fillAmount = progress;
    }

    public void SetUserLevel(int userLevel)
    {
        txt_UserLevel.text = userLevel.ToString();
    }


    public void OnClick_SkipitsShop()
    {
        UiManager.instance.ui_Navbar.OnClick_MenuActivate(0);
        UiManager.instance.ui_Shop.SetScrollContent(3500);
    }
    public void OnClick_EnergysShop()
    {
        Debug.Log("Button Clicked");
        UiManager.instance.ui_EnergyPurchase.gameObject.SetActive(true);
    }
    public void OnClick_CoinsShop()
    {
        UiManager.instance.ui_Navbar.OnClick_MenuActivate(0);
        UiManager.instance.ui_Shop.SetScrollContent(5500);
    }
    public void OnClick_GemsShop()
    {
        UiManager.instance.ui_Navbar.OnClick_MenuActivate(0);
        UiManager.instance.ui_Shop.SetScrollContent(7500);
    }
}
