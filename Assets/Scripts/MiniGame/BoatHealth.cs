using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHealth : MonoBehaviour
{
    [SerializeField] private float flt_BoatHealth = 100f;

    [Header("Only For Debbing")]
    [SerializeField] private float currentHealth;
    private string obstacleTag = "Obstacle";
    private string stoneGateTag = "StoneGate";

    private void Start()
    {
        currentHealth = flt_BoatHealth;
    }

    private void DecreaseHealth(float _damage)
    {
        currentHealth -= _damage;
        if(currentHealth <= 100)
        {
            print("Game Over");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag))
        {
            print("Decrease Health");
            DecreaseHealth(other.GetComponent<MiniGameObstacle>().flt_DamageAmount);
        }

        if (other.CompareTag(stoneGateTag))
        {
            print("Decrease Health");
            DecreaseHealth(other.GetComponent<MiniGameObstacle>().flt_DamageAmount);
        }
    }
}
