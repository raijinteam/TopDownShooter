using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;



    [SerializeField] private bool isMiniGameStart = false;
    [SerializeField] private int currentMinigameLevel = 0;
    [SerializeField] private int nextLevelIndex = 20;
    [SerializeField] private float minigameRunningTime = 0;
    [SerializeField] private List<GameObject> list_ActiveEnemy;

    [SerializeField] private float increaseObstacelSpeedPer;
    [SerializeField] private float increaseSpawnTimePer;

    [SerializeField] private MiniGameObstaclesSpawner obstacleSpawner;
    [SerializeField] private BorderParallexEffect border;
    private void Awake()
    {
        if (instance != this || instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMiniGameStart)
        {
            minigameRunningTime += Time.deltaTime;

            if (minigameRunningTime >= nextLevelIndex)
            {
                nextLevelIndex += 20;
                currentMinigameLevel++;
                obstacleSpawner.obstacleMoveSpeed += obstacleSpawner.obstacleMoveSpeed * (increaseObstacelSpeedPer / 100);
                obstacleSpawner.timeBetweenSpawnEnemy -= obstacleSpawner.timeBetweenSpawnEnemy * (increaseSpawnTimePer / 100);
                obstacleSpawner.timeBetweenSpawnObstacle -= obstacleSpawner.timeBetweenSpawnObstacle * (increaseSpawnTimePer / 100);
                border.all_BorderMoveSpeed += border.all_BorderMoveSpeed * (increaseObstacelSpeedPer / 100);
            }
        }
    }
    public void AddEnemyInList(GameObject _Currentenemy)
    {
        list_ActiveEnemy.Add(_Currentenemy);
    }
    public void RemoveEnemyInList(GameObject _Currentenemy)
    {
        list_ActiveEnemy.Remove(_Currentenemy);
    }
    public List<GameObject> GetListOfEnemy()
    {
        return list_ActiveEnemy;
    }

    public bool IsMiniGameStart
    {
        get
        {
            return isMiniGameStart;
        }
        set
        {
            isMiniGameStart = value;
        }
    }

    public int CurrentMinigameLevel
    {
        get
        {
            return currentMinigameLevel;
        }
    }

    public MiniGameObstaclesSpawner MiniGameSpawener
    {
        get
        {
            return obstacleSpawner;
        }
    }

    public float ObstacleMoveSpeed
    {
        get
        {
            return obstacleSpawner.obstacleMoveSpeed;
        }
    }


    public GameObject ActiveShootingEnemy
    {
        get
        {
            return obstacleSpawner.ShootingEnemy;
        }
    }

   
}
