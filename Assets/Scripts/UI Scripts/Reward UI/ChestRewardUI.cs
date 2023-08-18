using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChestRewardUI : MonoBehaviour
{
    [Header("Chest Reward")]
    public bool canShowRewardSummaryUI;
    [SerializeField] private RewardChestCardUI[] all_Cards;
    [SerializeField] private TextMeshProUGUI txt_NumberOfRewards;
    public List<Sprite> list_CardRewardIcon = new List<Sprite>();
    public List<int> list_CardRewardAmount = new List<int>();

    [SerializeField] private Transform rt_Card_AnimationPosition;
    [SerializeField] private Transform rt_CardSpawnPosition;
    [SerializeField] private float flt_AnimationDuration;


    [Header("Coins")]
    [SerializeField] private Transform coin_Card;
    [SerializeField] private Transform panel_Coin;
    [SerializeField] private TextMeshProUGUI txt_TotalCoins;

    [Header("Gems")]
    [SerializeField] private Transform gems_Card;
    [SerializeField] private Transform panel_Gems;
    [SerializeField] private TextMeshProUGUI txt_TotalGems;

    [Header("Energy")]
    [SerializeField] private Transform energy_Card;
    [SerializeField] private Transform panel_Energy;
    [SerializeField] private TextMeshProUGUI txt_TotalEnergy;

    [Header("Card")]
    [SerializeField] private Transform player_Card;
    [SerializeField] private Transform panel_Card;
    [SerializeField] private TextMeshProUGUI txt_PlayerName;
    [SerializeField] private TextMeshProUGUI txt_CardType;
    [SerializeField] private Slider slider_Cards;
    [SerializeField] private TextMeshProUGUI txt_TotalCard;


    [SerializeField] private bool isFirstcard;
    [SerializeField] private int index;
    [SerializeField] private int numberOfRewards;
    private int currentCardsCount;



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
                if (currentCardsCount <= 0)
                {
                    if (canShowRewardSummaryUI)
                    {
                        Debug.Log("Set Summary Data");
                        List<string> strings = new List<string>();

                        for (int i = 0; i < list_CardRewardAmount.Count; i++)
                        {
                            strings.Add(list_CardRewardAmount[i].ToString());
                        }
                        UiManager.instance.ui_Reward.ui_RewardSummary.SetMultiplRewardSummaryData(list_CardRewardIcon, strings);
                        UiManager.instance.ui_Reward.ui_RewardSummary.gameObject.SetActive(true);
                        this.gameObject.SetActive(false);
                    }
                    else
                    {
                        UiManager.instance.ui_Reward.ui_StaterPack.gameObject.SetActive(true);
                        UiManager.instance.ui_Reward.ui_StaterPack.canMoveNextReward = true;
                    }

                    ResetAllObjects();
                    this.gameObject.SetActive(false);
                }

                if (index > 0)
                {
                     StopPreviousAnimations(index - 1);
                }

                if (index < all_Cards.Length)
                {
                    AnimateCardAndDetails(index);
                    index++;
                    currentCardsCount--;
                    txt_NumberOfRewards.text = currentCardsCount.ToString();
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
                energy_Card.DOKill();
                panel_Energy.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Energy.localScale = Vector3.zero;
                break;
            case 3:
                all_Cards[3].transform.DOKill();
                player_Card.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Card.localScale = Vector3.zero;
                break;
            case 4:
                all_Cards[4].transform.DOKill();
                player_Card.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Card.localScale = Vector3.zero;
                break;
            case 5:
                all_Cards[5].transform.DOKill();
                player_Card.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Card.localScale = Vector3.zero;
                break;
            case 6:
                all_Cards[6].transform.DOKill();
                player_Card.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Card.localScale = Vector3.zero;
                break;
            case 7:
                all_Cards[7].transform.DOKill();
                player_Card.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Card.localScale = Vector3.zero;
                break;
            case 8:
                all_Cards[8].transform.DOKill();
                player_Card.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                panel_Card.localScale = Vector3.zero;
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
                    AnimateCard(all_Cards[0].transform);
                    coin_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Coin, txt_TotalCoins));
                    txt_TotalCard.text = list_CardRewardAmount[0].ToString();
                    isFirstcard = false;
                    break;
                case 1:
                    AnimateCard(all_Cards[1].transform);
                    gems_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Gems, txt_TotalGems));
                    txt_TotalGems.text = list_CardRewardAmount[1].ToString();
                    break;
                case 2:
                    AnimateCard(all_Cards[2].transform);
                    energy_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Energy, txt_TotalEnergy));
                    txt_TotalEnergy.text = list_CardRewardAmount[2].ToString();
                    break;
                case 3:
                    AnimateCard(all_Cards[3].transform);
                    player_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Card, txt_TotalCard));
                    SetPlayerDetails(3);
                    break;
                case 4:
                    AnimateCard(all_Cards[4].transform);
                    player_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Card, txt_TotalCard));
                    SetPlayerDetails(4);
                    break;
                case 5:
                    AnimateCard(all_Cards[5].transform);
                    player_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Card, txt_TotalCard));
                    SetPlayerDetails(5);
                    break;
                case 6:
                    AnimateCard(all_Cards[6].transform);
                    player_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Card, txt_TotalCard));
                    break;
                case 7:
                    AnimateCard(all_Cards[7].transform);
                    player_Card.DOMove(rt_Card_AnimationPosition.position, 1f).OnComplete(() => AnimateDetails(panel_Card, txt_TotalCard));
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
        Debug.Log("Animation Details called");
        details.DOScale(Vector3.one, flt_AnimationDuration);
        txt.DOFade(1, flt_AnimationDuration);
    }



    private void SetPlayerDetails(int index)
    {
        int playerIndex = list_CardRewardAmount[index];

        txt_PlayerName.text = HeroesManager.Instance.all_HeroData[playerIndex].name;
       // txt_PlayerLevel.text = (PlayerManager.Instance.all_Players[playerIndex].currentLevel + 1).ToString();
        txt_CardType.text = HeroesManager.Instance.all_HeroData[playerIndex].heroType.ToString();
        StartCoroutine(PlayerSliderAnimation(playerIndex, 5));
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
        txt_TotalCard.text = $"{currentCards} / {HeroesManager.Instance.all_HeroData[_playerIndex].requireCardsToUnlock[currentPlayerLevel]}";

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
        Debug.Log("Cleat List");
        ResetAnimationDetails(panel_Coin, txt_TotalCoins);
        ResetAnimationDetails(panel_Gems, txt_TotalGems);
        ResetAnimationDetails(panel_Energy, txt_TotalEnergy);
        ResetAnimationDetails(panel_Card, txt_TotalCard);

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
        all_Cards[index].SetRewardCardData(list_CardRewardIcon[index] , list_CardRewardAmount[index]);
    }

    public void SetChestRewardData(List<Sprite> _rewardIcon , List<int> _rewardAmount)
    {
        index = 0;
        this.gameObject.SetActive(true);
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
        for(int i  = 0;i < list_CardRewardIcon.Count; i++)
        {
            all_Cards[i].gameObject.SetActive(true);
        }
    }



}
