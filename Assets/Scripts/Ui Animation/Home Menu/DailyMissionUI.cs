using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class DailyMissionUI : MonoBehaviour
{

    [SerializeField] private DailyManagerSO[] all_DailyMissionsSO; // REFERANCE OF ALL ACTIVE DAILY MISSIONS
    [SerializeField] private DailyMissionData[] allDailyMisisonData;

    [SerializeField] private GameObject pf_DailyMission;
    [SerializeField] private GameObject parentOfDailyMission;

    [SerializeField] private int numberOfDailyMissions = 4;

    private List<DailyManagerSO> listOfDailyMissionSO = new List<DailyManagerSO>();


    private void Start()
    {
        for (int i = 0; i < all_DailyMissionsSO.Length; i++)
        {
            listOfDailyMissionSO.Add(all_DailyMissionsSO[i]);
        }
        ShuffleList(listOfDailyMissionSO);

        for (int i = 0; i < numberOfDailyMissions; i++)
        {
            print("Instation");
            GameObject dailyMission = Instantiate(pf_DailyMission, transform.position, Quaternion.identity);
            dailyMission.transform.SetParent(parentOfDailyMission.transform);
            dailyMission.GetComponent<DailyMissionData>().img_RewardIcon.sprite = listOfDailyMissionSO[i].img_RewardIcon;
            dailyMission.GetComponent<DailyMissionData>().txt_Description.text = listOfDailyMissionSO[i].str_Description;
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
