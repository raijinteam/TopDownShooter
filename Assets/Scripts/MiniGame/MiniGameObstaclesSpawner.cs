using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameObstaclesSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] allObstacles; // ARRAY OF ALL OBSTACLES IN MINIGAMES
    [SerializeField] private GameObject[] all_StoneGateway;
    [SerializeField] private GameObject shootingEnemyPrefab;
    [SerializeField] private Transform miniGameObstacleParent;

    [Header("Obstacles Properties")]
    public float timeBetweenSpawnObstacle ; // TIME GAP BETWEEN ONE OBSTACLE SPAWN TO SECOND OBSTACL SPWAN
    [SerializeField] private float currentTimeBetweenSpawnObstacle;

    [Header("Enemy Properties")]
    [SerializeField] private bool canShootingEnemySpawn = false;
    public float timeBetweenSpawnEnemy ; // TIME GAP BETWEEN ONE OBSTACLE SPAWN TO SECOND OBSTACL SPWAN
    [SerializeField] private float currentTimeBetweenSpawnEnemy;

    [Space]
    [SerializeField] private float minSpawnPosX , maxSpawnPosX,minSpawnPosZ, maxSpawnPosZ; 
    public float obstacleMoveSpeed;

    [HideInInspector]
    public GameObject shootingEnemy;

    void Start()
    {
        currentTimeBetweenSpawnObstacle = timeBetweenSpawnObstacle;
        currentTimeBetweenSpawnEnemy = timeBetweenSpawnEnemy;
    }

    // Update is called once per frame
    void Update()
    {

        if (MiniGameManager.instance.IsMiniGameStart)
        {
            currentTimeBetweenSpawnObstacle += Time.deltaTime;

            if (currentTimeBetweenSpawnObstacle >= timeBetweenSpawnObstacle)
            {
                SpawnObstacle();
                int perCount = Random.Range(1, 100);
                if(perCount >= 60)
                {
                    SpawnStoneGateway();
                }
                currentTimeBetweenSpawnObstacle = 0;
            }

            currentTimeBetweenSpawnEnemy += Time.deltaTime;

            if (currentTimeBetweenSpawnEnemy >= timeBetweenSpawnEnemy)
            {
                SpawnEnemy();
                currentTimeBetweenSpawnEnemy = 0;
            }
        }
        else
        {
            DestoryAllObstacles();
        }

    }


    private void SpawnObstacle()
    {
        float randomPosX = Random.Range(minSpawnPosX, maxSpawnPosX);
        float randomPosZ = Random.Range(minSpawnPosZ, maxSpawnPosZ);
        
        int randomIndex = Random.Range(0, allObstacles.Length);
        
        GameObject obstacle = Instantiate(allObstacles[randomIndex], transform.position + new Vector3(randomPosX , 0 ,randomPosZ), Quaternion.Euler(0,90,0) , miniGameObstacleParent);
    }

    private void SpawnStoneGateway()
    {
        print("Gateway Spawn");
        int randomIndex = Random.Range(0, all_StoneGateway.Length);
        float randomPosZ = Random.Range(minSpawnPosZ, maxSpawnPosZ);
        Instantiate(all_StoneGateway[randomIndex], transform.position + new Vector3(2.5f, 4, randomPosZ), Quaternion.identity, miniGameObstacleParent);
    }

    private void DestoryAllObstacles()
    {
        currentTimeBetweenSpawnEnemy = 0;
        currentTimeBetweenSpawnObstacle = 0;
        canShootingEnemySpawn = false;
        int countOfObstacle = miniGameObstacleParent.childCount;
        for(int i =0; i < countOfObstacle; i++)
        {
            Destroy(miniGameObstacleParent.GetChild(i).gameObject);
        }
    }

    private void SpawnEnemy()
    {
        float randomPosX = Random.Range(minSpawnPosX, maxSpawnPosX);
        float randomPosZ = Random.Range(minSpawnPosZ, maxSpawnPosZ);

        if (!canShootingEnemySpawn)
        {
            shootingEnemy = Instantiate(shootingEnemyPrefab, transform.position + new Vector3(randomPosX, 0, randomPosZ), Quaternion.Euler(0, -90, 0) , miniGameObstacleParent);
            canShootingEnemySpawn = true;
        }
    }



    public bool CanShootingEnemySpawn
    {
        get
        {
            return canShootingEnemySpawn;
        }
        set
        {
            canShootingEnemySpawn = value;
        }
    }

    public GameObject ShootingEnemy
    {
        get
        {
            return shootingEnemy;
        }
    }
}
