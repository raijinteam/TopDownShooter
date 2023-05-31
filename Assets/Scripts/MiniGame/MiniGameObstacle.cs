using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniGameObstacle : MonoBehaviour
{

    public float flt_DamageAmount;

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * MiniGameManager.instance.ObstacleMoveSpeed * Time.deltaTime;
    }

}
