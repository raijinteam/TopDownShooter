using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameObstacleDestoryer : MonoBehaviour
{
    private string obstacleTag = "Obstacle";
    private string stoneGameTag = "StoneGate";


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag(stoneGameTag))
        {
            Destroy(other.gameObject);
        }

    }
}
