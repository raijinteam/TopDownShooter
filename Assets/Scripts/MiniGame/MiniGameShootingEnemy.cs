using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameShootingEnemy : MonoBehaviour
{

    [Header("Required Component")]
    [SerializeField] private Animator enemyAnim = null;

    [Header("Shooting Enemy bullet")]
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private Transform enemyBody;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private bool nextEnemySpawn = false;
    [SerializeField] private float enemyStopingRange = 20f;
    [SerializeField] private float bulletShootTime = 2f;

    private bool canMove = false;
    private bool canShoot = false;
    [SerializeField] private float currentBulletShootTime;
    private float currentEnemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        currentBulletShootTime = bulletShootTime;
        currentEnemyHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            currentBulletShootTime += Time.deltaTime;
            if (currentBulletShootTime >= bulletShootTime)
            {
                GameObject shoot = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

                currentBulletShootTime = 0;
            }
        }

        if (canMove)
        {
            transform.position -= Vector3.forward * MiniGameManager.instance.ObstacleMoveSpeed * Time.deltaTime;

            enemyAnim.SetBool("canShoot", false);

            if (Vector3.Distance(transform.position, GameManager.instance.Player.transform.position) < enemyStopingRange)
            {
                canShoot = true;
                canMove = false;
                enemyAnim.SetBool("canShoot", true);
                enemyBody.transform.LookAt(GameManager.instance.Player.transform.position);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }

}
