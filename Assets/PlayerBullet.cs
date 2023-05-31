using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float flt_MovementSpeed;

    private Vector3 direction;

    public void SetDirection(Vector3 _Bulletdirection)
    {
        direction = _Bulletdirection;
    }

    private void Update()
    {
        BulletMotion();
    }

    private void BulletMotion()
    {
        transform.Translate(direction.normalized * flt_MovementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShootingEnemy"))
        {
            Destroy(this.gameObject);
            Destroy(other.GetComponentInParent<MiniGameShootingEnemy>().gameObject);
            MiniGameManager.instance.MiniGameSpawener.CanShootingEnemySpawn = false;
        }
    }
}
