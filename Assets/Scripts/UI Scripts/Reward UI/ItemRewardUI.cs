using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemRewardUI : MonoBehaviour
{

    public List<Sprite> list_RewardIcon = new List<Sprite>();
    public List<int> list_RewardAmount = new List<int>();

    [Header("Item Reward")]
    [SerializeField] private Image img_Reward;
    [SerializeField] private TextMeshProUGUI txt_RewardAmount;

    private int index;

    public void SetRewardData(Sprite _rewardSprite, int _rewardAmount)
    {
        this.gameObject.SetActive(true);
        list_RewardIcon.Add(_rewardSprite);
        list_RewardAmount.Add(_rewardAmount);
    }

    private void OnEnable()
    {
        index = 0;
        SetData();
    }

    private void OnDisable()
    {
        list_RewardIcon.Clear();
        list_RewardAmount.Clear();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if (index == list_RewardIcon.Count)
            {
                this.gameObject.SetActive(false);
                UiManager.instance.ui_Reward.gameObject.SetActive(false);
                Debug.Log("Close Reward Screen");
            }
            if (index < list_RewardIcon.Count)
            {
                Debug.Log("Index : " + index);
                SetData();
            }
        }
    }
    private void SetData()
    {
        img_Reward.sprite = list_RewardIcon[index];
        txt_RewardAmount.text = list_RewardAmount[index].ToString();
    }
}
