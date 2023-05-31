using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyRewardUI : MonoBehaviour
{
    [Header ("Daily Reward Data")]
    [SerializeField] private Button[] allDaysRewardButtons;
    [SerializeField] private Sprite rewardClimedSprite;

    private int index = 0;

    private List<Sprite> sprites = new List<Sprite>();
    private List<string> strings = new List<string>();



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < allDaysRewardButtons.Length; i++)
        {
            allDaysRewardButtons[i].interactable = false;
        }
        allDaysRewardButtons[index].interactable = true;
        
    }


    public void ClearList()
    {
        sprites.Clear();
        strings.Clear();
    }

    public void OnClick_ActiveNextRewardButton(int _buttonIndex)
    {
        if (_buttonIndex == index)
        {
            allDaysRewardButtons[index].interactable = false;
            allDaysRewardButtons[index].transform.GetChild(2).gameObject.SetActive(true);
            if (index >= allDaysRewardButtons.Length)
            {
                index = -1;
                print("index is big them length");
            }

            if(index == 15)
            {
                int childCount = allDaysRewardButtons[index].transform.GetChild(0).transform.childCount;
                print(childCount);
               
                for (int i =0; i < childCount; i++)
                {

                    sprites.Add(allDaysRewardButtons[index].transform.GetChild(0).transform.GetChild(i).GetComponent<Image>().sprite);
                    print("sprites :" + sprites.Count);
                    
                    strings.Add(allDaysRewardButtons[index].transform.GetChild(1).transform.GetChild(i).GetComponent<TextMeshProUGUI>().text);
                    print("Strings " + strings.Count);
                }
                UiManager.instance.rewardSummaryUI.SetMultiplRewardSummaryData(sprites, strings);
                UiManager.instance.rewardSummaryUI.gameObject.SetActive(true);
            }
            else
            {
                Image rewardIcon = allDaysRewardButtons[index].transform.GetChild(0).GetComponent<Image>();
                string rewardAmount = allDaysRewardButtons[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
                UiManager.instance.rewardSummaryUI.SetRewardSummaryData(rewardIcon.sprite, rewardAmount);
                UiManager.instance.rewardSummaryUI.gameObject.SetActive(true);
                rewardIcon.sprite = rewardClimedSprite;
                index++;
                allDaysRewardButtons[index].interactable = true;
                print(index);
            }
        }

    }
}
