using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletShootTime = 2f;
    [SerializeField] private GameObject player;

    [SerializeField] private Transform target;

    private GameObject bullet;
    private float currentBulletShootTime;

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        if (!MiniGameManager.instance.IsMiniGameStart)
        {
            DestoryActiveBullet();
        }
    }

    private void FindTarget()
    {
        if(target == null)
        {
            if (MiniGameManager.instance.MiniGameSpawener.CanShootingEnemySpawn && MiniGameManager.instance.IsMiniGameStart)
            {
                target = MiniGameManager.instance.ActiveShootingEnemy.transform;
            }
        }

        if (target != null)
        {
            player.transform.LookAt(target.position);
            FireBullet();
        }
    }

    private void FireBullet()
    {
        currentBulletShootTime += Time.deltaTime;
        if (currentBulletShootTime >= bulletShootTime)
        {
            currentBulletShootTime = 0;
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
       bullet =  Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        Vector3 direction = -player.transform.position + target.position;
        bullet.GetComponent<PlayerBullet>().SetDirection(direction + new Vector3(0,1.5f,0));
    }

    public void DestoryActiveBullet()
    {
        Destroy(bullet);
    }
}
