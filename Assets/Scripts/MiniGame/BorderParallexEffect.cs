using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderParallexEffect : MonoBehaviour
{

    [SerializeField] private GameObject[] all_leftborder;
    [SerializeField] private GameObject[] all_Rightborder;
    public float all_BorderMoveSpeed;
    [SerializeField] private Transform leftBorderSpawnPoint;
    [SerializeField] private Transform rightBorderSpawnPoint;

    [SerializeField] private float borderReachPoint;


    // Update is called once per frame
    void Update()
    {
        if (MiniGameManager.instance.IsMiniGameStart)
        {
            for (int i = 0; i < all_leftborder.Length; i++)
            {
                all_leftborder[i].transform.Translate(0, 0, -all_BorderMoveSpeed * Time.deltaTime);
                all_Rightborder[i].transform.Translate(0, 0, -all_BorderMoveSpeed * Time.deltaTime);
                if (all_leftborder[i].transform.position.z <= -borderReachPoint)
                {
                    all_leftborder[i].transform.position = leftBorderSpawnPoint.position;
                }

                if (all_Rightborder[i].transform.position.z <= -borderReachPoint)
                {
                    all_Rightborder[i].transform.position = rightBorderSpawnPoint.position;
                }
            }
        }
    }
}
