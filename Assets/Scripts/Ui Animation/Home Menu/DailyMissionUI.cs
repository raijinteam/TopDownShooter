
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyMissionUI : MonoBehaviour
{

    [SerializeField] private DailyMissionSO[] all_DailyMissionsSO; // REFERANCE OF ALL ACTIVE DAILY MISSIONS

    [SerializeField] private int numberOfDailyMissions = 4;
    [SerializeField] private GameObject pf_DailyMission;
    [SerializeField] private RectTransform parentOfDailyMission;
    public TextMeshProUGUI txt_DailyMissionTimer;


    [SerializeField] private List<DailyMissionSO> listOfDailyMissionSO = new List<DailyMissionSO>();
    [SerializeField] private List<DailyMissionData> list_DailyMission = new List<DailyMissionData>();
    public bool isOnedayCompelete;



    private void Update()
    {
        int timeInSeconds = (int)TimeCalculation.instance.currentDailyMissionTime;

        int hours = timeInSeconds / 3600;
        int minutes = (timeInSeconds % 3600) / 60;
        int seconds = timeInSeconds % 60;

        txt_DailyMissionTimer.text = $"{hours} : {minutes} : {seconds}";



        if (Input.GetKeyDown(KeyCode.P))
        {
            increasemissinValue(1, 1);
        }
    }


    public void increasemissinValue(int index, int value)
    {
        int currentValue = DataManager.instance.GetAchivementCurrentValue(index) + value;
        DataManager.instance.SetAchivementCurrentValue(index, currentValue);
        SetDailyMissionData(index);
    }

    public void SpawnDailyMissions()
    {
        // Debug.Log("Is One day complete : " + DataManager.Instance.GetDailyMissionOneDayComplete());
        isOnedayCompelete = DataManager.instance.GetDailyMissionOneDayComplete();

        for (int i = 0; i < numberOfDailyMissions; i++)
        {
            GameObject dailyMission = Instantiate(pf_DailyMission, parentOfDailyMission.position, Quaternion.identity, parentOfDailyMission);
            DailyMissionData mission = dailyMission.GetComponent<DailyMissionData>();



            list_DailyMission.Add(mission);
            SetDailyMissionData(i);
        }

    }

    private void SetRandomSO(int index)
    {
        if (isOnedayCompelete)
        {
            int checkRandomIndex = 0;
            int randomIndex = Random.Range(0, all_DailyMissionsSO.Length);

            if (randomIndex == checkRandomIndex)
            {
                randomIndex = Random.Range(0, all_DailyMissionsSO.Length);
            }
            else if (randomIndex == DataManager.instance.GetDailyMisisonSOIndex(index))
            {
                randomIndex = Random.Range(0, all_DailyMissionsSO.Length);
            }



            listOfDailyMissionSO.Add(all_DailyMissionsSO[randomIndex]);
            DataManager.instance.SetDailyMisisonSOIndex(index, randomIndex);
            Debug.Log("in if index : " + index);
            if (index == numberOfDailyMissions - 1)
            {
                // Debug.Log("False daily misison ");
                isOnedayCompelete = false;
                DataManager.instance.SetDailyMissionOnedayComplete(false);
            }
            checkRandomIndex = randomIndex;
        }
        else
        {
            int _index = DataManager.instance.GetDailyMisisonSOIndex(index);
            // Debug.Log("in else index : " + _index);
            listOfDailyMissionSO.Add(all_DailyMissionsSO[_index]);

        }
    }

    public void ResetAllListData()
    {
        for (int i = 0; i < list_DailyMission.Count; i++)
        {
            Destroy(list_DailyMission[i].gameObject);
        }
        list_DailyMission.Clear();
        listOfDailyMissionSO.Clear();
    }

    private void SetDailyMissionData(int _index)
    {

        SetRandomSO(_index);

        list_DailyMission[_index].index = _index;

        if (CheckForDailyMissionComplete(_index))
        {
            list_DailyMission[_index].btn_Claim.gameObject.SetActive(true);
        }

        list_DailyMission[_index].isDailyMissionClaimed = DataManager.instance.GetDailyMissionClaimedState(_index);

        if (list_DailyMission[_index].isDailyMissionClaimed)
        {
            list_DailyMission[_index].CheckForRewardIsClaimed();
        }

        list_DailyMission[_index].img_RewardIcon.sprite = listOfDailyMissionSO[_index].img_RewardIcon;
        list_DailyMission[_index].txt_Description.text = listOfDailyMissionSO[_index].str_Description;
        list_DailyMission[_index].txt_RewardAmount.text = listOfDailyMissionSO[_index].rewardAmount.ToString();

        int currentMissionValue = DataManager.instance.GetAchivementCurrentValue(_index);
        int completeMissionValue = listOfDailyMissionSO[_index].missionCompeleteAmount;

        list_DailyMission[_index].txt_MissionValue.text = $"{currentMissionValue} / {completeMissionValue}";

        list_DailyMission[_index].slider_RewardComplate.maxValue = listOfDailyMissionSO[_index].missionCompeleteAmount;
        list_DailyMission[_index].slider_RewardComplate.value = DataManager.instance.GetAchivementCurrentValue(_index);
        list_DailyMission[_index].rewardType = listOfDailyMissionSO[_index].rewardType;
    }





    private bool CheckForDailyMissionComplete(int index)
    {
        int currentValue = DataManager.instance.GetAchivementCurrentValue(index);
        int maxValue = all_DailyMissionsSO[index].missionCompeleteAmount;

        if (currentValue >= maxValue)
        {
           // UiManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(true);
            return true;
        }
        else
        {
            //UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(false);
        }
        return false;
    }

    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}
