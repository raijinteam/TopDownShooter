using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class StaterPackUI : MonoBehaviour
{
    public RewardChestCardUI[] all_Cards;

    [SerializeField] private TextMeshProUGUI txt_NumberOfRewards;
    private int numberOfRewards;
    [SerializeField] private RectTransform rt_Card_AnimationPosition;
    [SerializeField] private RectTransform rt_CardSpawnPosition;
    [SerializeField] private float flt_AnimationDuration;

    public List<Sprite> list_CardRewardIcon = new List<Sprite>();
    public List<int> list_CardRewardAmount = new List<int>();

    [Header("Coins")]
    [SerializeField] private RectTransform panel_Coin;
    [SerializeField] private RectTransform coin_Card;
    [SerializeField] private TextMeshProUGUI txt_CoinAmount;

    [Header("Gems")]
    [SerializeField] private RectTransform panel_Gems;
    [SerializeField] private RectTransform gems_Card;
    [SerializeField] private TextMeshProUGUI txt_GemsAmount;

    [Header("Material")]
    [SerializeField] private RectTransform panel_Material;
    [SerializeField] private RectTransform card_Material;
    [SerializeField] private TextMeshProUGUI txt_MaterialType;
    [SerializeField] private Slider slider_Cards;
    [SerializeField] private TextMeshProUGUI txt_MaterialName;
    [SerializeField] private TextMeshProUGUI txt_Material_Cards;

    public bool canMoveNextReward;
    [SerializeField] private bool isFirstcard;
    [SerializeField] private int index;
    [SerializeField] private int currentCardsCount;


    private void OnEnable()
    {
        if (!canMoveNextReward)
        {
          //  Debug.Log("Not move Next index");
            index++;
           // Debug.Log("Index : " + index);
            StopPreviousAnimations(index);
            currentCardsCount--;

            if (currentCardsCount <= 0)
            {
               // Debug.Log("False this object");
                List<string> strings = new List<string>();
                for(int i = 0; i < list_CardRewardAmount.Count; i++)
                {
                    strings.Add(list_CardRewardAmount[i].ToString());
                }
                UiManager.instance.ui_Reward.ui_RewardSummary.SetMultiplRewardSummaryData(list_CardRewardIcon, strings);
                UiManager.instance.ui_Reward.ui_RewardSummary.gameObject.SetActive(true);
                ResetAllObjects();
                this.gameObject.SetActive(false);
            }

            txt_NumberOfRewards.text = currentCardsCount.ToString();
            canMoveNextReward = true;
        }
    }

    private void Update()
    {
        if (isFirstcard)
        {
            AnimateCardAndDetails(index);
            index++;
            currentCardsCount--;
            txt_NumberOfRewards.text = currentCardsCount.ToString();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
               // Debug.Log("MOve Next Level");
                if (currentCardsCount <= 0)
                {
                  //  Debug.Log("False this object");
                    ResetAllObjects();
                    this.gameObject.SetActive(false);
                    this.gameObject.transform.parent.gameObject.SetActive(false);
                }

                if (index > 0)
                {
                    StopPreviousAnimations(index - 1);
                }

                if (index < all_Cards.Length)
                {
                  //  Debug.Log("Index : " + index);
                    AnimateCardAndDetails(index);
                    if (canMoveNextReward)
                    {
                        index++;
                        currentCardsCount--;
                        txt_NumberOfRewards.text = currentCardsCount.ToString();
                    }
                }
            }
        }
    }


    private void StopPreviousAnimations(int cardIndex)
    {
        switch (cardIndex)
        {
            case 0:
                all_Cards[cardIndex].DOKill();
                coin_Card.transform.DOKill();
                panel_Coin.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Coin.localScale = Vector3.zero;
                break;
            case 1:
                all_Cards[1].transform.DOKill();
                gems_Card.DOKill();
                panel_Gems.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Gems.localScale = Vector3.zero;
                break;
            case 2:
                all_Cards[2].transform.DOKill();
                card_Material.DOKill();
                panel_Material.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Material.localScale = Vector3.zero;
                break;
            case 3:
                all_Cards[3].transform.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                break;
            case 4:
                all_Cards[4].transform.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                break;
        }
    }

    private void AnimateCardAndDetails(int cardIndex)
    {
        if (cardIndex < numberOfRewards)
        {
            switch (cardIndex)
            {
                case 0:
                  //  Debug.Log("First Index");
                    AnimateCard(all_Cards[0].transform);
                    coin_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Coin, txt_CoinAmount));
                    txt_CoinAmount.text = list_CardRewardAmount[0].ToString();
                    isFirstcard = false;
                    break;
                case 1:
                   // Debug.Log("Second Index");
                    AnimateCard(all_Cards[1].transform);
                    gems_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Gems, txt_GemsAmount));
                    txt_GemsAmount.text = list_CardRewardAmount[1].ToString();
                    break;
                case 2:
                  //  Debug.Log("Third Index");
                    AnimateCard(all_Cards[2].transform);
                    card_Material.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Material, txt_MaterialName));
                    SetPlayerDetails();
                    break;
                case 3:
                   // Debug.Log("Forth Index");
                    canMoveNextReward = false;
                  //  Debug.Log("Open Chest Panel");
                    List<Sprite> sprites = new List<Sprite>();
                    List<int> amounts = new List<int>();
                    for (int i = 0; i < RewardManager.Instance.all_ChestData[1].all_ChestData.Length; i++)
                    {
                        sprites.Add(RewardManager.Instance.all_ChestData[1].all_ChestData[i].rewardIcon);
                        amounts.Add(RewardManager.Instance.all_ChestData[1].all_ChestData[i].rewardAmount);
                    }
                    UiManager.instance.ui_Reward.ui_ChestReward.canShowRewardSummaryUI = false;
                    UiManager.instance.ui_Reward.ui_ChestReward.SetChestRewardData(sprites, amounts);
                    this.gameObject.SetActive(false);
                    break;
                case 4:
                 //   Debug.Log("Fifth Idnex");
                    canMoveNextReward = false;
                 //   Debug.Log("Open Chest Panel");
                    List<Sprite> sprites1 = new List<Sprite>();
                    List<int> amounts1 = new List<int>();
                    for (int i = 0; i < RewardManager.Instance.all_ChestData[1].all_ChestData.Length; i++)
                    {
                        sprites1.Add(RewardManager.Instance.all_ChestData[1].all_ChestData[i].rewardIcon);
                        amounts1.Add(RewardManager.Instance.all_ChestData[1].all_ChestData[i].rewardAmount);
                    }
                    UiManager.instance.ui_Reward.ui_ChestReward.canShowRewardSummaryUI = false;
                    UiManager.instance.ui_Reward.ui_ChestReward.SetChestRewardData(sprites1, amounts1);
                    this.gameObject.SetActive(false);
                    break;

            }
        }

    }


    private void AnimateCard(Transform card)
    {
        Sequence seq = DOTween.Sequence();
        card.DOMove(rt_Card_AnimationPosition.position, flt_AnimationDuration);
        seq.Append(card.DORotate(new Vector3(0, 180, 0), flt_AnimationDuration / 2)).Append(card.DORotate(new Vector3(0, 360, 0), flt_AnimationDuration / 2).OnComplete(SetRewardItems));
    }

    private void SetRewardItems()
    {
        all_Cards[index - 1].SetRewardCardData(list_CardRewardIcon[index - 1], list_CardRewardAmount[index - 1]);
    }

    private void AnimateDetails(Transform details, TextMeshProUGUI txt)
    {
      //  Debug.Log("Animation Details called");
        details.DOScale(Vector3.one, flt_AnimationDuration);
        txt.DOFade(1, flt_AnimationDuration);
    }



    private void SetPlayerDetails()
    {
        int materialSlotIndex = Random.Range(0 , 5);
       // Debug.Log("Material Slot Index : " + materialSlotIndex);
        int materialIndex = 0;
        string materialName = "";
        int materialLevel = 0;
        string materialType = "";
        int currentMaterialCards = 0;
        int requireCards = 0;
        switch (materialSlotIndex){
            case 0:
                materialSlotIndex = Random.Range(0, SlotHeadEquipmentManager.instance.all_HeadInventory.Length);
                materialLevel = SlotHeadEquipmentManager.instance.all_HeadInventory[materialIndex].currentLevel;
                materialName = SlotHeadEquipmentManager.instance.all_HeadInventory[materialSlotIndex].name;
                materialType = "Head";
                SlotHeadEquipmentManager.instance.IncreaseMaterial(list_CardRewardAmount[2]);
                currentMaterialCards = SlotHeadEquipmentManager.instance.all_HeadInventory[materialSlotIndex].currentCards;
                requireCards = SlotHeadEquipmentManager.instance.all_HeadInventory[materialIndex].requireMaterialToLevelUp[materialLevel];
                break;
            case 1:
                materialSlotIndex = Random.Range(0, SlotGunsManager.instance.all_GunInventoryItems.Length);
                materialLevel = SlotGunsManager.instance.all_GunInventoryItems[materialIndex].currentLevel;
                materialName = SlotGunsManager.instance.all_GunInventoryItems[materialSlotIndex].name;
                currentMaterialCards = SlotGunsManager.instance.all_GunInventoryItems[materialSlotIndex].currentCards;
                materialType = "Guns";
                SlotGunsManager.instance.IncreaseMaterial(list_CardRewardAmount[2]);
                requireCards = SlotGunsManager.instance.all_GunInventoryItems[materialIndex].requireMaterialToLevelUp[materialLevel];
                break;
            case 2:
                materialSlotIndex = Random.Range(0, SlotArrmorManager.instance.all_ArrmorInventoryItems.Length);
                materialLevel = SlotArrmorManager.instance.all_ArrmorInventoryItems[materialIndex].currentLevel;
                materialName = SlotArrmorManager.instance.all_ArrmorInventoryItems[materialSlotIndex].name;
                currentMaterialCards = SlotArrmorManager.instance.all_ArrmorInventoryItems[materialSlotIndex].currentCards;
                materialType = "Arrmor";
                SlotArrmorManager.instance.IncreaseMaterial(list_CardRewardAmount[2]);
                requireCards = SlotArrmorManager.instance.all_ArrmorInventoryItems[materialIndex].requireMaterialToLevelUp[materialLevel];
                break;
            case 3:
                materialSlotIndex = Random.Range(0, SlotGlovesManager.instance.all_GlovesInventoryItems.Length);
                materialLevel = SlotGlovesManager.instance.all_GlovesInventoryItems[materialIndex].currentLevel;
                materialName = SlotGlovesManager.instance.all_GlovesInventoryItems[materialSlotIndex].name;
                currentMaterialCards = SlotGlovesManager.instance.all_GlovesInventoryItems[materialSlotIndex].currentCards;
                materialType = "Gloves";
                SlotGlovesManager.instance.IncreaseMaterial(list_CardRewardAmount[2]);
                requireCards = SlotGlovesManager.instance.all_GlovesInventoryItems[materialIndex].requireMaterialToLevelUp[materialLevel];
                break;
            case 4:
                materialSlotIndex = Random.Range(0, SlotAnythingManager.instance.all_AnythingInventoryItems.Length);
                materialLevel = SlotAnythingManager.instance.all_AnythingInventoryItems[materialIndex].currentLevel;
                materialName = SlotAnythingManager.instance.all_AnythingInventoryItems[materialSlotIndex].name;
                currentMaterialCards = SlotAnythingManager.instance.all_AnythingInventoryItems[materialSlotIndex].currentCards;
                materialType = "Anything";
                SlotAnythingManager.instance.IncreaseMaterial(list_CardRewardAmount[2]);
                requireCards = SlotAnythingManager.instance.all_AnythingInventoryItems[materialIndex].requireMaterialToLevelUp[materialLevel];
                break;
            case 5:
                materialSlotIndex = Random.Range(0, SlotAblitiesManager.instance.all_AbilitesInventoryItems.Length);
                materialLevel = SlotAblitiesManager.instance.all_AbilitesInventoryItems[materialIndex].currentLevel;
                materialName = SlotAblitiesManager.instance.all_AbilitesInventoryItems[materialSlotIndex].name;
                materialType = "Ablities";
                SlotAblitiesManager.instance.IncreaseMaterial(list_CardRewardAmount[2]);
                currentMaterialCards = SlotAblitiesManager.instance.all_AbilitesInventoryItems[materialSlotIndex].currentCards;
                requireCards = SlotAblitiesManager.instance.all_AbilitesInventoryItems[materialIndex].requireMaterialToLevelUp[materialLevel];
                break;
        }


       txt_MaterialName.text = materialName;
        Debug.Log("Material Name : " + materialName);
       txt_MaterialType.text = materialType;
        txt_Material_Cards.text = $"{currentMaterialCards} / {requireCards}";

       // StartCoroutine(PlayerSliderAnimation(playerIndex, 5));
    }

    private IEnumerator PlayerSliderAnimation(int _playerIndex, int updateCards)
    {
        ResetSlider();

        int currentPlayerLevel = HeroesManager.Instance.all_HeroData[_playerIndex].currentLevel;
        slider_Cards.maxValue = HeroesManager.Instance.all_HeroData[_playerIndex].requireCardsToUnlock[currentPlayerLevel];
        int currentCards = HeroesManager.Instance.all_HeroData[_playerIndex].currentCards;
        slider_Cards.value = currentCards;
        currentCards += updateCards;
        HeroesManager.Instance.all_HeroData[_playerIndex].currentCards = currentCards;
        txt_Material_Cards.text = $"{currentCards} / {HeroesManager.Instance.all_HeroData[_playerIndex].requireCardsToUnlock[currentPlayerLevel]}";

        //DataManager.instance.SetHeroCard(_playerIndex, currentCards);

        float timer = 0;
        yield return new WaitForSeconds(flt_AnimationDuration);
        while (timer < 1)
        {
            timer += Time.deltaTime / 2f;
            slider_Cards.value = Mathf.Lerp(0, currentCards, timer);
            yield return null;
        }
        //HeroesManager.Instance.SetPlayerState(_playerIndex);
    }

    private void ResetSlider()
    {
        slider_Cards.value = 0;
    }



    private void ResetAnimationDetails(Transform details, TextMeshProUGUI txt)
    {
        details.DOScale(Vector3.zero, 0.001f);
        txt.DOFade(0, 0.001f);
    }

    private void ResetAllObjects()
    {

        ResetAnimationDetails(panel_Coin, txt_CoinAmount);
        ResetAnimationDetails(panel_Gems, txt_GemsAmount);
        ResetAnimationDetails(panel_Material, txt_MaterialName);

        list_CardRewardIcon.Clear();
        list_CardRewardAmount.Clear();

        index = 0;
        numberOfRewards = 0;



        for (int i = 0; i < all_Cards.Length; i++)
        {
            all_Cards[i].transform.position = rt_CardSpawnPosition.position;
            StopPreviousAnimations(i);
            all_Cards[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < numberOfRewards; i++)
        {
            all_Cards[i].DisableCards();
        }

    }



    private void SetCardData()
    {
        all_Cards[index].SetRewardCardData(list_CardRewardIcon[index], list_CardRewardAmount[index]);
    }

    public void SetStaterPackData(List<Sprite> _rewardIcon, List<int> _rewardAmount)
    {
        index = 0;
        this.gameObject.SetActive(true);
        canMoveNextReward = true;

        for (int i = 0; i < _rewardIcon.Count; i++)
        {
            list_CardRewardIcon.Add(_rewardIcon[i]);
            list_CardRewardAmount.Add(_rewardAmount[i]);
        }

        numberOfRewards = list_CardRewardIcon.Count;
        currentCardsCount = numberOfRewards;
        SetActiveRewardCards();
        isFirstcard = true;
    }


    public void SetActiveRewardCards()
    {
        for (int i = 0; i < list_CardRewardIcon.Count; i++)
        {
            all_Cards[i].gameObject.SetActive(true);
        }
    }
}
